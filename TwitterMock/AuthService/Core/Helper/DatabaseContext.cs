﻿using AuthService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Core.Helper;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB
        //Auto generate id
        modelBuilder.Entity<Auth>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Auth>().HasIndex(u => u.Email).IsUnique();
        #endregion
    }

    public DbSet<Auth> Auths { get; set; }
}
