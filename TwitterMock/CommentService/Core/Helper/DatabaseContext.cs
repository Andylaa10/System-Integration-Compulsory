using CommentService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Core.Helper;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("CommentDb");
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
    }

    private DbSet<Comment> Comments { get; set; }
}