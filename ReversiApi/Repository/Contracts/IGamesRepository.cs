namespace ReversiApi.Repository.Contracts;

/// <summary>
/// Provides an interface for games repository.
/// </summary>
public interface IGamesRepository<out T> : IRepository<GameEntity>
{

    /// <summary>
    /// Returns all games of the queue.
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> AllInQueue();

    /// <summary>
    /// Determines if the game exists.
    /// </summary>
    /// <param name="token">The unique token of the game.</param>
    /// <returns>Whether the game exists or not.</returns>
    bool Exists(string? token);
    
    /// <summary>
    /// Gets the game by the token.
    /// </summary>
    /// <param name="token">The unique token of the game.</param>
    /// <returns>The game.</returns>
    T? Get(string? token);
    
    /// <summary>
    /// Gets the game by the token of player one.
    /// </summary>
    /// <param name="token">The unique token of the game.</param>
    /// <returns>The game.</returns>
    T? GetByPlayerOne(string? token);
    
    /// <summary>
    /// Gets the game by the token of player two.
    /// </summary>
    /// <param name="token">The unique token of the game.</param>
    /// <returns>The game.</returns>
    T? GetByPlayerTwo(string? token);
}