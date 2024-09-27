using api.Abstractions;
using api.Models.DTOs;
using api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/api")]
public class UserController : ControllerBase
{
	private readonly IJwtGenerator _jwtGenerator;
	private readonly IPasswordHasher _passwordHasher;
	private readonly IUserRepository _userRepository;

	public UserController(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtGenerator jwtGenerator)
	{
		_userRepository = userRepository;
		_passwordHasher = passwordHasher;
		_jwtGenerator = jwtGenerator;
	}

	[Route("register")]
	[HttpPost]
	public async Task<ActionResult<UserResponseDto>> Register([FromBody] RegisterRequestDto registerRequest)
	{
		if (!registerRequest.Password.Equals(registerRequest.Password2))
		{
			return BadRequest("Passwords are not matching");
		}

		var existingUsername = await _userRepository.GetByUsername(registerRequest.Username);
		var existingMail = await _userRepository.GetByEmail(registerRequest.Email);

		if (existingUsername != null)
		{
			return BadRequest("Username is already taken");
		}

		if (existingMail != null)
		{
			return BadRequest("Email already taken");
		}

		var hashedPassword = _passwordHasher.Hash(registerRequest.Password);

		var newUser = new User
		{
			Username = registerRequest.Username,
			Email = registerRequest.Email,
			Password = hashedPassword,
		};

		await _userRepository.CreateUser(newUser);
		var userDto = new UserResponseDto(newUser.Username, newUser.Email);

		return CreatedAtAction(nameof(Login), userDto);
	}

	[Route("login")]
	[HttpPost]
	public async Task<ActionResult<TokenUserResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
	{
		const string errorMsg = "Username and/or password is wrong";
		var userInDb = await _userRepository.GetByUsername(loginRequest.Username);

		if (userInDb == null)
		{
			return BadRequest(errorMsg);
		}

		var matchingPassword = _passwordHasher.Verify(userInDb.Password, loginRequest.Password);

		if (!matchingPassword)
		{
			return BadRequest(errorMsg);
		}

		var token = _jwtGenerator.GenerateToken(userInDb);

		var userDto = new UserResponseDto(userInDb.Username, userInDb.Email);

		var response = new TokenUserResponseDto(token, userDto);

		return Ok(response);
	}
}
