using api.Abstractions;
using api.Mappers;
using api.Models.DTOs;
using api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    
    public UserController(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    [Route("/api/register")]
    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> Register([FromBody] RegisterRequestDto registerRequest)
    {
        var existingMail = await _userRepository.GetByEmail(registerRequest.Email);
        var existingUsername = await _userRepository.GetByUsername(registerRequest.Username);

        if (existingUsername != null)
            return BadRequest("Username is already taken");

        if (existingMail != null)
            return BadRequest("Email already taken");
        
        
        var hashedPassword = _passwordHasher.Hash(registerRequest.Password);

        var newUser = new User()
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            Password = hashedPassword
        };

        var savedUser = await _userRepository.CreateUser(newUser);
        var userDto = savedUser.ToDto();
        
        return CreatedAtAction(nameof(Login), userDto);
    }

    [Route("/api/login")]
    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
    {
        const string errorMsg = "Username and/or password is wrong";
        var userInDb = await _userRepository.GetByUsername(loginRequest.Username);

        if (userInDb == null)
            return BadRequest(errorMsg);

        bool matchingPassword = _passwordHasher.Verify(userInDb.Password, loginRequest.Password);

        if (!matchingPassword)
            return BadRequest(errorMsg);

        return Ok(userInDb.ToDto());
    }
}