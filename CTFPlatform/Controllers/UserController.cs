using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CTFPlatform.Models;

namespace CTFPlatform.Controllers;

[ApiController]
[Route("v1/users")]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;


    public UserController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public IActionResult GetAllUsers()
    {
        List<User> users = _databaseContext.Users.ToList();
        foreach (var user in users)
        {
            user.Password = "HIDDEN";
            user.Login = "HIDDEN";
        }

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetUser(int id)
    {
        var user = (from b in _databaseContext.Users where b.Id == id select b).FirstOrDefault();
        if (user == null)
        {
            return NotFound("not found");
        }

        return Ok(user);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteUser(int id)
    {
        var user = (from b in _databaseContext.Users where b.Id == id select b).FirstOrDefault();
        if (user == null)
        {
            return NotFound("not found");
        }

        _databaseContext.Users.Remove(user);
        _databaseContext.SaveChanges();
        return Ok("deleted");
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateUser(int id, [FromBody] UserEditDto userData)
    {
        var user = (from b in _databaseContext.Users where b.Id == id select b).FirstOrDefault();
        if (user == null)
        {
            return NotFound("not found");
        }

        if (userData.IsAdmin != null)
        {
            user.IsAdmin = userData.IsAdmin.Value;
        }

        if (userData.Name != null)
        {
            user.Name = userData.Name;
        }

        if (userData.IsActive != null)
        {
            user.IsActive = userData.IsActive.Value;
        }

        if (userData.Password != null)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(userData.Password);
        }

        _databaseContext.Users.Update(user);
        _databaseContext.SaveChanges();
        return Ok(user);
    }
}