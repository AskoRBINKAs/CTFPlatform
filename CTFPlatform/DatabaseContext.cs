using CTFPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace CTFPlatform;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public bool UserExists(string login)
    {
        foreach(var user in Users)
        {
            if (user.Login == login)
            {
                return true;
            }
        }
        return false;
    }
}