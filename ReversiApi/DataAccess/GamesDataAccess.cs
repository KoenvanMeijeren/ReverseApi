using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ReversiApi.DataAccess;

public class GamesDataAccess : DbContext
{
    public DbSet<GameEntity> Games { get; set; }
    public DbSet<PlayerEntity> Players { get; set; }

    public GamesDataAccess(DbContextOptions<GamesDataAccess> options) : base(options)
    {

    }

    [ExcludeFromCodeCoverage]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GameEntityConfiguration());
    }
}
