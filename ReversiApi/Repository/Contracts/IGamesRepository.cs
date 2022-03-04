namespace ReversiApi.Repository.Contracts;

/// <summary>
/// Provides an interface for games repository.
/// </summary>
public interface IGamesRepository : IRepository<GameEntity>
{

    /// <summary>
    /// Returns all games of the queue.
    /// </summary>
    /// <returns></returns>
    IEnumerable<GameEntity> AllInQueue();

    /// <summary>
    /// Determines if this player does not player another game.
    /// </summary>
    /// <param name="playerEntity">The player.</param>
    /// <returns>True if the player does not play another game.</returns>
    bool DoesNotPlayAGame(PlayerEntity playerEntity);
    
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
    GameEntity? Get(string? token);
    
    /// <summary>
    /// Gets the game by the token of player one.
    /// </summary>
    /// <param name="token">The unique token of the game.</param>
    /// <returns>The game.</returns>
    GameEntity? GetByPlayerOne(string? token);
    
    /// <summary>
    /// Gets the game by the token of player two.
    /// </summary>
    /// <param name="token">The unique token of the game.</param>
    /// <returns>The game.</returns>
    GameEntity? GetByPlayerTwo(string? token);
}