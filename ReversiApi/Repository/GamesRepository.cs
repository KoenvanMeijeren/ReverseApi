using ReversiApi.Repository.Contracts;

namespace ReversiApi.Repository;

/// <summary>
/// Provides a repository for the game.
/// </summary>
public class GamesRepository : RepositoryBase<GameEntity>, IGamesRepository
{
    public GamesRepository()
    {
        GameEntity entity1 = new GameEntity();
        GameEntity entity2 = new GameEntity();
        GameEntity entity3 = new GameEntity();

        entity1.Description = "Potje snel reveri, dus niet lang nadenken";
        entity1.PlayerOne = new PlayerEntity(new PlayerOne("abcdef"));

        entity2.Description = "Ik zoek een gevorderde tegenspeler!";
        entity2.PlayerOne = new PlayerEntity(new PlayerOne("ghijkl"));
        entity2.PlayerTwo = new PlayerEntity(new PlayerTwo("mnopqr"));
        
        entity3.PlayerOne = new PlayerEntity(new PlayerOne("stuvwx"));

        this.Add(entity1);
        this.Add(entity2);
        this.Add(entity3);
    }

    /// <inheritdoc />
    public override void Add(GameEntity entity)
    {
        entity.UpdateGame();
        
        base.Add(entity);
    }

    /// <inheritdoc />
    public IEnumerable<GameEntity> AllInQueue()
    {
        return this.All().Where(entity => entity.Game.IsQueued());
    }
    
    /// <inheritdoc />
    public bool DoesNotPlayAGame(PlayerEntity playerEntity)
    {
        bool playsAGame = false;
        foreach (var entity in this.All())
        {
            if (entity.PlayerOne != null && DoesPlayerPlayAGame(playerEntity, entity))
            {
                playsAGame = true;
            }
            
            if (entity.PlayerTwo != null && DoesPlayerPlayAGame(playerEntity, entity))
            {
                playsAGame = true;
            }
        }

        return !playsAGame;
    }

    /// <summary>
    /// Determines if the player plays the current game.
    /// </summary>
    /// <param name="playerEntity">The player.</param>
    /// <param name="gameEntity">The game.</param>
    /// <returns>True if the player plays the game.</returns>
    private static bool DoesPlayerPlayAGame(PlayerEntity playerEntity, GameEntity gameEntity)
    {
        if (gameEntity.PlayerOne != null && gameEntity.PlayerOne.Equals(playerEntity))
        {
            return !gameEntity.Game.IsQuit() && !gameEntity.Game.IsFinished();
        }
        
        if (gameEntity.PlayerTwo != null && gameEntity.PlayerTwo.Equals(playerEntity))
        {
            return !gameEntity.Game.IsQuit() && !gameEntity.Game.IsFinished();
        }

        return false;
    }

    /// <inheritdoc />
    public bool Exists(string? token)
    {
        return token != null && this.Get(token) != null;
    }

    /// <inheritdoc />
    public GameEntity? Get(string? token)
    {
        return this.Items.SingleOrDefault(entity => entity.Token.Equals(token));
    }
    
    /// <inheritdoc />
    public GameEntity? GetByPlayerOne(string? token)
    {
        return this.Items.SingleOrDefault(entity => entity.PlayerOne != null && entity.PlayerOne.Token.Equals(token));
    }
    
    /// <inheritdoc />
    public GameEntity? GetByPlayerTwo(string? token)
    {
        return this.Items.SingleOrDefault(entity => entity.PlayerTwo != null && entity.PlayerTwo.Token.Equals(token));
    }
    
    /// <inheritdoc />
    public override bool Update(GameEntity entity)
    {
        entity.UpdateEntity();
        entity.UpdateGame();

        return base.Update(entity);
    }
}