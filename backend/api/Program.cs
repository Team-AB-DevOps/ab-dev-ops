using System.Text;
using api.Abstractions;
using api.Data;
using api.Repositories;
using api.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);



// Configure Serilog
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.WriteTo.Console()
	.CreateLogger();

builder.Host.UseSerilog(); // Use Serilog instead of the default .NET logger


// Try to load .env file if it exists for local development
var envFile = ".env";
if (File.Exists(envFile))
{
	Env.Load(envFile);
}

builder.Configuration.AddEnvironmentVariables();

var jwtKey = builder.Configuration["JWT_KEY"] ?? "fallback_test_jwt_key";
if (string.IsNullOrEmpty(jwtKey))
{
	throw new InvalidOperationException("JWT_KEY is not set in the configuration.");
}

// CORS Settings
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy(
		myAllowSpecificOrigins,
		policy =>
		{
			policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
		}
	);
});

// Add services to the container.
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddHttpClient<IWeatherApi, WeatherApi>();
builder.Services.AddResponseCaching();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
	.Services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
		};
	});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Hvis "Test"-Enviornment, så andvend in memory sqlite db
if (builder.Environment.EnvironmentName == "Test")
{
	builder.Services.AddDbContext<DataContext>(options => options.UseSqlite("DataSource=:memory:"));
}
else
{
	// MySQL for dev og prod
	var connectionString = builder.Configuration.GetValue<string>("ConnectionString");
	if (string.IsNullOrEmpty(connectionString))
	{
		throw new Exception("Connection string not found. Ensure the .env file is correctly configured and placed in the root directory.");
	}

	var serverVersion = ServerVersion.AutoDetect(connectionString);

	builder.Services.AddDbContext<DataContext>(options =>
	{
		options.UseMySql(connectionString, serverVersion).LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging().EnableDetailedErrors();
	});
}


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Apply CORS settings
app.UseCors(myAllowSpecificOrigins);

// Apply use of caching
app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Apply migrations only if not in the "Test" environment
if (!app.Environment.IsEnvironment("Test"))
{
	using var scope = app.Services.CreateScope();
	var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
	await dbContext.Database.EnsureDeletedAsync();
	await dbContext.Database.MigrateAsync();

	// Call the database initializer at startup
	var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
	var sqlFilePath = Path.Combine(app.Environment.ContentRootPath, "Sql", "data.sql");
	initializer.InitializeDatabase(sqlFilePath);
}

app.Run();

public partial class Program { }
