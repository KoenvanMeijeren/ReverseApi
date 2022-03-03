using Microsoft.EntityFrameworkCore;
using ReversiApi.DataAccess;
using ReversiApi.Repository.Contracts;

namespace ReversiApi.Repository;

/// <summary>
/// Provides a repository for the game.
/// </summary>
public class GamesDatabaseRepository : RepositoryDatabaseBase<GameEntity>, IDatabaseGamesRepository
{
    
    public GamesDatabaseRepository(GamesDataAccess context) : base(context)
    {
        
    }
    
    /// <inheritdoc />
    public override IEnumerable<GameEntity> All()
    {
        var entities = this.GetDbSet()
            .Include(entity => entity.PlayerOne)
            .Include(entity => entity.PlayerTwo)
            .ToList();

        return PrepareForReturn(entities);
    }

    /// <inheritdoc />
    public IEnumerable<GameEntity> AllInQueue()
    {
        var entities = this.All();

        var filtered = entities.Where(entity => entity.Game.IsQueued() || entity.Game.IsPending()).ToList();

        return PrepareForReturn(filtered);
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
            .SingleOrDefault(entity => entity.Token.Equals(token));

        return PrepareForReturn(entity);
    }

    /// <inheritdoc />
    public GameEntity? GetByPlayerOne(string? token)
    {
        var entity = this.Context.Games
            .Include(entity => entity.PlayerOne)
            .Include(entity => entity.PlayerTwo)
            .SingleOrDefault(entity => entity.PlayerOne != null && entity.PlayerOne.Token.Equals(token));

        return PrepareForReturn(entity);
    }
    
    /// <inheritdoc />
    public GameEntity? GetByPlayerTwo(string? token)
    {
        var entity = this.Context.Games
            .Include(entity => entity.PlayerOne)
            .Include(entity => entity.PlayerTwo)
            .SingleOrDefault(entity => entity.PlayerTwo != null && entity.PlayerTwo.Token.Equals(token));

        return PrepareForReturn(entity);
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

    /// <inheritdoc />
    protected override DbSet<GameEntity> GetDbSet()
    {
        return this.Context.Games;
    }
}