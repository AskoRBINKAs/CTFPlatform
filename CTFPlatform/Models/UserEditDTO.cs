namespace CTFPlatform.Models;

public class UserEditDto
{
    public string? Name { get; set; } = null;
    public string? Password { get; set; } = null;
    public bool? IsAdmin { get; set; } = null;
    public bool? IsActive { get; set; } = null;
}