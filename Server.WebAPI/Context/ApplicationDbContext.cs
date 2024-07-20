using Microsoft.EntityFrameworkCore;
using Server.WebAPI.Models;

namespace Server.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(p => p.FirstName).HasColumnType("varchar(50)");
        modelBuilder.Entity<User>().Property(p => p.LastName).HasColumnType("varchar(50)");
        modelBuilder.Entity<User>().Property(p => p.Salary).HasColumnType("money");
    }
}
