using ReversiApi.Helpers.Validators;
using ReversiApi.Repository.Contracts;

namespace ReversiApi.Repository;

/// <summary>
/// Provides a repository for the game.
/// </summary>
public class GamesRepository : RepositoryBase<GameEntity>, IGamesRepository
{
    public GamesRepository()
    {
        var entity1 = new GameEntity(1);
        var entity2 = new GameEntity(2);
        var entity3 = new GameEntity(3);

        entity1.Description = "Potje snel reveri, dus niet lang nadenken";
        entity1.PlayerOne = new PlayerEntity(token: "abcdef");

        entity2.Description = "Ik zoek een gevorderde tegenspeler!";
        entity2.PlayerOne = new PlayerEntity(token: "ghijkl");
        entity2.PlayerTwo = new PlayerEntity(token: "mnopqr");

        entity3.PlayerOne = new PlayerEntity(token: "stuvwx");

        this.Add(entity1);
        this.Add(entity2);
        this.Add(entity3);

        // Add some entities for testing the status.
        this.Add(new GameEntity(11) { Status = Status.Created });
        this.Add(new GameEntity(12) { Status = Status.Queued });
        this.Add(new GameEntity(13) { Status = Status.Pending });
        this.Add(new GameEntity(14) { Status = Status.Playing });
        this.Add(new GameEntity(15) { Status = Status.Quit });
    }

    /// <inheritdoc />
    public override void Add(GameEntity entity)
    {
        entity.UpdateGame();

        base.Add(entity);
    }

    /// <inheritdoc />
    public IEnumerable<GameEntity> AllByStatus(string? status)
    {
        return status switch
        {
            "created" => this.All().Where(entity => entity.Game.IsCreated()),
            "queued" => this.All().Where(entity => entity.Game.IsQueued()),
            "pending" => this.All().Where(entity => entity.Game.IsPending()),
            "playing" => this.All().Where(entity => entity.Game.IsPlaying()),
            "active" => this.All().Where(entity => !entity.Game.IsQuit() && !entity.Game.IsFinished()),
            "quit" => this.All().Where(entity => entity.Game.IsQuit()),
            "finished" => this.All().Where(entity => entity.Game.IsFinished()),
            _ => this.All()
        };
    }

    /// <inheritdoc />
    public bool DoesNotPlayAGame(PlayerEntity? playerEntity)
    {
        return GameValidator.PlayerDoesNotPlayAGame(this.All(), playerEntity);
    }

    /// <inheritdoc />
    public bool Exists(string? token)
    {
        return token != null && this.Get(token) != null;
    }

    /// <inheritdoc />
    public GameEntity? Get(string? token)
    {
        return this.Items.FirstOrDefault(entity => entity.Token.Equals(token));
    }

    /// <inheritdoc />
    public GameEntity? GetByPlayerOne(string? token, string? status = null)
    {
        return this.AllByStatus(status)
            .FirstOrDefault(entity => entity.PlayerOne != null && entity.PlayerOne.Token.Equals(token));
    }

    /// <inheritdoc />
    public GameEntity? GetByPlayerTwo(string? token, string? status = null)
    {
        return this.AllByStatus(status)
            .FirstOrDefault(entity => entity.PlayerTwo != null && entity.PlayerTwo.Token.Equals(token));
    }

    /// <inheritdoc />
    public override bool Update(GameEntity entity)
    {
        entity.UpdateEntity();
        entity.UpdateGame();

        return base.Update(entity);
    }
}
