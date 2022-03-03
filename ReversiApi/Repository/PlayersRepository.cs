using ReversiApi.Repository.Contracts;

namespace ReversiApi.Repository;

/// <summary>
/// Provides a repository for the game.
/// </summary>
public class PlayersRepository : RepositoryBase<PlayerEntity>, IPlayersRepository
{
    
    public PlayersRepository()
    {
        this.Add(new PlayerEntity(new PlayerOne("abcdef")));
        this.Add(new PlayerEntity(new PlayerOne("ghijkl")));
        this.Add(new PlayerEntity(new PlayerTwo("mnopqr")));
        this.Add(new PlayerEntity(new PlayerOne("stuvwx")));
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
        return this.Items.SingleOrDefault(player => player.Token.Equals(token)) != null;
    }
    
    /// <inheritdoc />
    public PlayerEntity? Get(string? token)
    {
        return this.Items.SingleOrDefault(player => player.Token.Equals(token));
    }
}