using api.Abstractions;
using api.Controllers;
using api.Models.DTOs;
using api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace tests;

public class UserTests
{
	private readonly IJwtGenerator _jwtGeneratorMock;
	private readonly IPasswordHasher _passwordHasherMock;
	private readonly UserController _userController;
	private readonly IUserRepository _userRepositoryMock;

	public UserTests()
	{
		// Mocks:
		_passwordHasherMock = Substitute.For<IPasswordHasher>();
		_userRepositoryMock = Substitute.For<IUserRepository>();
		_jwtGeneratorMock = Substitute.For<IJwtGenerator>();

		_userController = new UserController(_userRepositoryMock, _passwordHasherMock, _jwtGeneratorMock);
	}

	[Fact]
	public async Task Register_Existing_Username_Should_Return_Bad_Request()
	{
		// Arrange
		var userInDb = new User { Email = "Test2@gmail.com", Username = "Test Name" };
		var requestDto = new RegisterRequestDto("Test Name", "TestPassword");
		_userRepositoryMock.GetByUsername(requestDto.Username).Returns(userInDb);

		// Act
		var result = await _userController.Register(requestDto);

		// Assert
		Assert.IsType<BadRequestObjectResult>(result.Result);
	}

	[Fact]
	public async Task Register_New_User_Should_Success()
	{
		// Arrange
		var requestDto = new RegisterRequestDto("Julie", "TestPassword");
		var hashedPassword = "hashed";
		_userRepositoryMock.GetByUsername(requestDto.Username).Returns(Task.FromResult<User?>(null));
		_passwordHasherMock.Hash(requestDto.Password).Returns(hashedPassword);

		var newUser = new User
		{
			Username = "Julie",
			Password = hashedPassword,
		};
		_userRepositoryMock.CreateUser(newUser).Returns(Task.FromResult(newUser));

		// Act
		var result = await _userController.Register(requestDto);

		// Assert
		var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
		var response = Assert.IsType<UserResponseDto>(createdResult.Value);
		Assert.Equal(newUser.Username, response.Username);

	}

	[Fact]
	public async Task Login_Wrong_Password_Should_Bad_Request()
	{
		// Arrange
		var userInDb = new User
		{
			Email = "James@gmail.com",
			Username = "James",
			Password = "Hashed",
		};
		var requestDto = new LoginRequestDto("James", "WrongPw");

		_userRepositoryMock.GetByUsername(requestDto.Username).Returns(userInDb);
		_passwordHasherMock.Verify(userInDb.Password, requestDto.Password).Returns(false);

		// Act
		var result = await _userController.Login(requestDto);

		// Assert
		Assert.IsType<BadRequestObjectResult>(result.Result);
	}

	[Fact]
	public async Task Login_Non_Existing_Username_Should_Bad_Request()
	{
		// Arrange
		var requestDto = new LoginRequestDto("Shabob", "CorrectPw");

		_userRepositoryMock.GetByUsername(requestDto.Username).Returns(Task.FromResult<User?>(null));

		// Act
		var result = await _userController.Login(requestDto);

		// Assert
		Assert.IsType<BadRequestObjectResult>(result.Result);
	}

	[Fact]
	public async Task Login_Existing_Username_And_Correct_Password_Should_Success()
	{
		// Arrange
		var userInDb = new User
		{
			Username = "James",
			Password = "CorrectPw",
		};
		var requestDto = new LoginRequestDto("James", "CorrectPw");
		var jwtKey = "superSecretLongJwtKey";

		_userRepositoryMock.GetByUsername(requestDto.Username).Returns(userInDb);
		_passwordHasherMock.Verify(userInDb.Password, requestDto.Password).Returns(true);
		_jwtGeneratorMock.GenerateToken(userInDb).Returns(jwtKey);

		// Act
		var result = await _userController.Login(requestDto);

		// Assert
		var createdResult = Assert.IsType<OkObjectResult>(result.Result);
		var response = Assert.IsType<TokenUserResponseDto>(createdResult.Value);
		Assert.Equal(requestDto.Username, response.User.Username);
		Assert.NotNull(response.Token);
	}
}
