using Microsoft.EntityFrameworkCore;
using UserService.Core.Entities;

namespace UserService.Core.Helper;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB
        //Comment model builder
        //Auto generate id
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        //Ensures that the email is unique
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        
        //Ensures that the username is unique
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        #endregion
        
    }

    public DbSet<User> Users { get; set; }
}