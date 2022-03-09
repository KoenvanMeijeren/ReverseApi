using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReversiApi.DataAccess;

[ExcludeFromCodeCoverage]
public class GameEntityConfiguration : IEntityTypeConfiguration<GameEntity>
{
    public void Configure(EntityTypeBuilder<GameEntity> builder)
    {
        builder.Property(game => game.Id).Metadata.IsPrimaryKey();

        builder.HasOne(game => game.PlayerOne)
            .WithMany(player => player.GamesPlayerOne)
            .HasForeignKey(game => game.PlayerOneId);

        builder.HasOne(game => game.PlayerTwo)
            .WithMany(player => player.GamesPlayerTwo)
            .HasForeignKey(game => game.PlayerTwoId);
    }
}