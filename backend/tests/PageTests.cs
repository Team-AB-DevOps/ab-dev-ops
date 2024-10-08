﻿using System.Net;
using System.Net.Http.Json;
using api.Models.DTOs;
using api.Models.Entities;
using DotNetEnv;

namespace tests;

public class PageTests : IClassFixture<TestDatabaseFactory>
{
	private readonly TestDatabaseFactory _factory;

	public PageTests(TestDatabaseFactory factory)
	{
		_factory = factory;
	}

	[Fact]
	public async Task Search_Endpoint_Without_language_Should_Return_OK()
	{
		//Arrange
		var client = _factory.CreateClient();

		// Act
		var response = await client.GetAsync("/api/search?q=JavaScript");

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}

	[Fact]
	public async Task Search_Endpoint_Should_Return_OK()
	{
		//Arrange
		var client = _factory.CreateClient();

		// Act
		var response = await client.GetAsync("/api/search?q=JavaScript&language=en");

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}

	[Fact]
	public async Task Search_Endpoint_Should_Return_Two_Results_On_Script_Search()
	{
		//Arrange
		var client = _factory.CreateClient();

		// Act
		var response = await client.GetAsync("/api/search?q=script");
		var pages = await response.Content.ReadFromJsonAsync<List<Page>>();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.NotNull(pages);
		Assert.Equal(2, pages.Count);
		foreach (var page in pages)
		{
			Assert.Contains("script", page.Content);
		}
	}

	[Fact]
	public async Task Search_Endpoint_Should_Return_OK_On_No_Search_Match()
	{
		//Arrange
		var client = _factory.CreateClient();

		// Act
		var response = await client.GetAsync("/api/search?q=leverpostej");
		var pages = await response.Content.ReadFromJsonAsync<List<Page>>();

		// Assert
		Assert.Equal([], pages);
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}
}
