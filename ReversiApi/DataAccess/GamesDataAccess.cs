using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ReversiApi.DataAccess;

[ExcludeFromCodeCoverage]
public class GamesDataAccess : DbContext
{

    public GamesDataAccess(DbContextOptions<GamesDataAccess> options) : base(options)
    {

    }

    public DbSet<GameEntity> Games { get; set; }
    public DbSet<PlayerEntity> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GameEntityConfiguration());
    }
}