using Microsoft.EntityFrameworkCore;
using TimeLineService.Core.Entities;

namespace TimeLineService.Core.Helper;

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
        modelBuilder.Entity<TimeLine>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        #endregion
    }

    public DbSet<TimeLine> TimeLines { get; set; }
}