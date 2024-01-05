using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CTFPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CTFPlatform.Controllers;

[ApiController]
[Route("v1/auth")]
public class AuthController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    private readonly IConfiguration _configuration;

    public AuthController(DatabaseContext databaseContext,IConfiguration configuration)
    {
        _databaseContext = databaseContext;
        _configuration = configuration;
    }
    
    [HttpPost("login")]
    public IActionResult LoginUser(UserLoginDto userLoginDto)
    {
        var userCandidate = (from u in _databaseContext.Users where u.Login == userLoginDto.Login select u).FirstOrDefault();
        if (userCandidate == null) return NotFound(Results.Json(new { message = "not found" }));
        if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, userCandidate.Password))
            return Unauthorized(Results.Json(new { message = "wrong password" }));
        if (userCandidate.IsActive)
        {
            return Ok(Results.Json(new {message="success",id = userCandidate.Id ,token=GenerateToken(userCandidate),isAdmin = userCandidate.IsAdmin,username = userCandidate.Name, solved= userCandidate.SolvedTasks}));
        }
        return BadRequest(Results.Json(new { message = "you was blocked" }));

    }
    
    [HttpPost("register")]
    public IActionResult RegisterUser(UserRegisterDto userRegisterDto)
    {
        if (!ContestSettings.UserConfiguration.RegistrationAllowed) return BadRequest(Results.Json(new { message = "registration closed" },statusCode: StatusCodes.Status400BadRequest));
        if (_databaseContext.UserExists(userRegisterDto.Login)) return BadRequest(Results.Json(new { message = "user already exists" },statusCode: StatusCodes.Status400BadRequest));
        _databaseContext.Users.Add(new User(
            login: userRegisterDto.Login,
            password: BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password),
            name: userRegisterDto.Name
        ));
        _databaseContext.SaveChanges();
        return Ok(Results.Json(new { message = "success" },statusCode: StatusCodes.Status201Created));
    }

    [HttpGet("validate_token")]
    [Authorize(Roles = "Admin,User")]
    public IActionResult ValidateToken()
    {
        return Ok(true);
    }
    
    private string GenerateToken(User user)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, user.Login));
        claims.Add(new Claim("username", user.Login));
        claims.Add(new Claim("id", user.Id.ToString()));
        claims.Add(user.IsAdmin ? new Claim(ClaimTypes.Role, "Admin") : new Claim(ClaimTypes.Role, "User"));
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}