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
    }

    public DbSet<Comment> Comments { get; set; }
}