using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Abstractions;
using api.Models.DTOs;
using api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers;
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;
    
    public UserController(IUserRepository userRepository, IPasswordHasher passwordHasher, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
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

        await _userRepository.CreateUser(newUser);
        var userDto = new UserResponseDto(newUser.Username, newUser.Email);
        
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
        
        // Jwt claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("userId", userInDb.Id.ToString()),
            new Claim("Username", userInDb.Username),
            new Claim("Email", userInDb.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signIn
            );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        var userDto = new UserResponseDto(userInDb.Username, userInDb.Email);
        
        return Ok(new { Token = tokenValue, User = userDto});
        
        // TODO: Change userDto?

        return Ok(userDto);
    }
}