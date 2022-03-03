namespace ReversiApi.Repository.Contracts;

/// <summary>
/// Provides an interface for games repository.
/// </summary>
public interface IDatabaseGamesRepository<out T> : IGamesRepository<GameEntity>, IDatabaseRepository<GameEntity>
{
    
}