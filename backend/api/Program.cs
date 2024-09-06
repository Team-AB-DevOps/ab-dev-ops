using api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB CONNECTION
var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception(
        "Connection string not found. Ensure the .env file is correctly configured and placed in the root directory.");
}

var serverVersion = ServerVersion.AutoDetect(connectionString);


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// Apply migrations
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
await dbContext.Database.EnsureDeletedAsync();
await dbContext.Database.MigrateAsync();

app.Run();
