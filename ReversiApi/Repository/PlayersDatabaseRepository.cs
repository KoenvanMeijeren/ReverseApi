using Microsoft.EntityFrameworkCore;
using ReversiApi.DataAccess;
using ReversiApi.Repository.Contracts;

namespace ReversiApi.Repository;

/// <summary>
/// Provides a repository for the game.
/// </summary>
public class PlayersDatabaseRepository : RepositoryDatabaseBase<PlayerEntity>, IPlayersDatabaseRepository
{
    
    public PlayersDatabaseRepository(GamesDataAccess context) : base(context)
    {
        
    }

    public PlayerEntity FirstOrCreate(PlayerEntity playerEntity)
    {
        var dbPlayer = this.Get(playerEntity.Token);
        if (dbPlayer != null)
        {
            return dbPlayer;
        }
        
        this.Add(playerEntity);

        return playerEntity;
    }

    /// <inheritdoc />
    public bool Exists(string? token)
    {
        return this.Context.Players.SingleOrDefault(player => player.Token.Equals(token)) != null;
    }
    
    /// <inheritdoc />
    public PlayerEntity? Get(string? token)
    {
        return this.Context.Players.SingleOrDefault(player => player.Token.Equals(token));
    }

    /// <inheritdoc />
    protected override DbSet<PlayerEntity> GetDbSet()
    {
        return this.Context.Players;
    }
}