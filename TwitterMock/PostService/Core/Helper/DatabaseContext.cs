using Microsoft.EntityFrameworkCore;
using PostService.Core.Entities;

namespace PostService.Core.Helper;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB
        //Auto generate id
        modelBuilder.Entity<Post>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        #endregion
        
    }

    public DbSet<Post> Posts { get; set; }
}