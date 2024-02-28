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

        #region Seeding Data
        //Seeding data
        //Creating 5 user objects
        var user1 = new User
        {
            Id = 1,
            Username = "user1",
            Email = "user1@example.com",
            Password = "password1",
            FirstName = "John",
            LastName = "Doe",
            CreatedAt = DateTime.Now,
            DateOfBirth = new DateOnly(1990, 5, 15)
        };

        var user2 = new User
        {
            Id = 2,
            Username = "user2",
            Email = "user2@example.com",
            Password = "password2",
            FirstName = "Jane",
            LastName = "Doe",
            CreatedAt = DateTime.Now,
            DateOfBirth = new DateOnly(1988, 9, 20)
        };

        var user3 = new User
        {
            Id = 3,
            Username = "user3",
            Email = "user3@example.com",
            Password = "password3",
            FirstName = "Alice",
            LastName = "Smith",
            CreatedAt = DateTime.Now,
            DateOfBirth = new DateOnly(1995, 3, 10)
        };

        var user4 = new User
        {
            Id = 4,
            Username = "user4",
            Email = "user4@example.com",
            Password = "password4",
            FirstName = "Bob",
            LastName = "Johnson",
            CreatedAt = DateTime.Now,
            DateOfBirth = new DateOnly(1985, 11, 25)
        };

        var user5 = new User
        {
            Id = 5,
            Username = "user5",
            Email = "user5@example.com",
            Password = "password5",
            FirstName = "Emily",
            LastName = "Wilson",
            CreatedAt = DateTime.Now,
            DateOfBirth = new DateOnly(1998, 7, 3)
        };
        
        modelBuilder.Entity<User>().HasData(user1, user2, user3, user4, user5);
        #endregion
    }

    public DbSet<User> Users { get; set; }
}