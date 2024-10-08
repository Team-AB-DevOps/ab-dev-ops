﻿using api.Data;
using api.Models.Entities;
using DotNetEnv;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace tests;

public class TestDatabaseFactory : WebApplicationFactory<Program>
{
	private readonly SqliteConnection _sqliteConnection;

	public TestDatabaseFactory()
	{
		_sqliteConnection = new SqliteConnection("DataSource=:memory:");
		_sqliteConnection.Open();
	}

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.UseEnvironment("Test"); // Set environment to "Test"

		builder.ConfigureAppConfiguration(
			(context, config) =>
			{
				// Try to load .env file if it exists
				var envFile = "../api/.env";
				if (File.Exists(envFile))
				{
					DotNetEnv.Env.Load(envFile);
				}

				// Add environment variables to configuration
				config.AddEnvironmentVariables();

				// Get JWT settings from environment variables or use fallback values
				var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? "fallback_test_jwt_key";
				var jwtIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer") ?? "test_issuer";
				var jwtAudience = Environment.GetEnvironmentVariable("Jwt__Audience") ?? "test_audience";

				// Add in-memory configuration
				config.AddInMemoryCollection(
					new Dictionary<string, string?>
					{
						{ "JWT_KEY", jwtKey },
						{ "Jwt:Issuer", jwtIssuer },
						{ "Jwt:Audience", jwtAudience },
					}
				);
			}
		);

		builder.ConfigureServices(services =>
		{
			// Remove the existing DbContext registration
			var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));
			if (descriptor != null)
			{
				services.Remove(descriptor);
			}

			// Add DbContext using SQLite in-memory database
			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlite(_sqliteConnection);
			});

			// Build the service provider
			var sp = services.BuildServiceProvider();

			// Ensure the database schema is created
			using (var scope = sp.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
				dbContext.Database.EnsureCreated(); // Ensure the in-memory database is created

				SeedDatabase(dbContext); // Seed db
			}
		});
	}

	private static void SeedDatabase(DataContext dbContext)
	{
		// Check if there are any pages already in the database
		if (!dbContext.Pages.Any())
		{
			dbContext.Pages.AddRange(
				new Page
				{
					Title = "JavaScript",
					Url = "https://example.com/page1",
					Language = "en",
					Content = "This is the content for Javascript",
					LastUpdated = DateTime.UtcNow,
				},
				new Page
				{
					Title = "TypeScript",
					Url = "https://example.com/page2",
					Language = "en",
					Content = "This is the content for Typescript",
					LastUpdated = DateTime.UtcNow,
				},
				new Page
				{
					Title = "Go",
					Url = "https://example.com/page3",
					Language = "en",
					Content = "This is the content for Go",
					LastUpdated = DateTime.UtcNow,
				}
			// Add more pages as needed
			);

			// Save changes to the database
			dbContext.SaveChanges();
		}
	}

	public void Dispose()
	{
		_sqliteConnection.Close();
		_sqliteConnection.Dispose();
		base.Dispose();
	}
}
