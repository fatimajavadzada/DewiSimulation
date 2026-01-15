using DewiSimulationMPA201.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DewiSimulationMPA201.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<Position> Positions { get; set; }
}
