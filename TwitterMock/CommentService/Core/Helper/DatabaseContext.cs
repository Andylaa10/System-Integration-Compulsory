using CommentService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Core.Helper;

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
        modelBuilder.Entity<Comment>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        #endregion

        #region Seeding Data
        //Seeding data
        //Creating 5 comment objects
        var comment1 = new Comment
        {
            Id = 1,
            UserId = 1,
            PostId = 1,
            Content = "This is the first comment on post 1",
            CreatedAt = DateTime.Now,
        };

        var comment2 = new Comment
        {
            Id = 2,
            UserId = 2,
            PostId = 1,
            Content = "Great post! Thanks for sharing.",
            CreatedAt = DateTime.Now,
        };

        var comment3 = new Comment
        {
            Id = 3,
            UserId = 3,
            PostId = 1,
            Content = "I have a question regarding this post.",
            CreatedAt = DateTime.Now,
        };

        var comment4 = new Comment
        {
            Id = 4,
            UserId = 4,
            PostId = 1,
            Content = "Interesting topic! Looking forward to more.",
            CreatedAt = DateTime.Now,
        };

        var comment5 = new Comment
        {
            Id = 5,
            UserId = 5,
            PostId = 1,
            Content = "Thanks for the detailed explanation.",
            CreatedAt = DateTime.Now,
        };

        modelBuilder.Entity<Comment>().HasData(comment1, comment2, comment3, comment4,comment5);

        #endregion
    }

    public DbSet<Comment> Comments { get; set; }
}