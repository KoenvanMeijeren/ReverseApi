using Microsoft.EntityFrameworkCore;
using ReversiApi.DataAccess;
using ReversiApi.Helpers.Validators;
using ReversiApi.Repository.Contracts;

namespace ReversiApi.Repository;

/// <summary>
/// Provides a repository for the game.
/// </summary>
public class GamesDatabaseRepository : RepositoryDatabaseBase<GameEntity>, IDatabaseGamesRepository
{

    public GamesDatabaseRepository(GamesDataAccess context) : base(context, context.Games)
    {

    }

    /// <inheritdoc />
    public override void Add(GameEntity entity)
    {
        entity.UpdateGame();

        base.Add(entity);
    }

    /// <inheritdoc />
    public override IEnumerable<GameEntity> All()
    {
        var entities = this.DbSet
            .Include(entity => entity.PlayerOne)
            .Include(entity => entity.PlayerTwo)
            .ToList();

        return PrepareForReturn(entities);
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
        var entity = this.Context.Games
            .Include(entity => entity.PlayerOne)
            .Include(entity => entity.PlayerTwo)
            .FirstOrDefault(entity => entity.Token.Equals(token));

        return PrepareForReturn(entity);
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

    private static GameEntity? PrepareForReturn(GameEntity? entity)
    {
        if (entity == null)
        {
            return null;
        }

        entity.UpdateGame();

        return entity;
    }

    private static IEnumerable<GameEntity> PrepareForReturn(IList<GameEntity> entities)
    {
        // Todo figure out how to avoid this.
        foreach (var entity in entities)
        {
            entity.UpdateGame();
        }

        return entities;
    }

    /// <inheritdoc />
    public override bool Update(GameEntity entity)
    {
        entity.UpdateEntity();
        entity.UpdateGame();

        return base.Update(entity);
    }
}
