using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTFPlatform.Models;

[Table("users")]
public class User
{
    [Key]
    public int Id { get; set; }
        
    [Required]
    [StringLength(100)]
    [Column("login")]
    public string Login { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    [Column("password")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Column("score")]
    public int Score { get; set; } = 0;

    [Required]
    [Column("isActive")]
    public bool IsActive { get; set; } = true;

    [Required]
    [Column("isAdmin")]
    public bool IsAdmin { get; set; } = false;

    [Required]
    [Column("solvedTasks")]
    public string SolvedTasks { get; set; } = string.Empty;
    
    public User()
    {

    }

    public User(string login, string name, string password)
    {
        Login = login;
        Name = name;
        Password = password;
    }
}