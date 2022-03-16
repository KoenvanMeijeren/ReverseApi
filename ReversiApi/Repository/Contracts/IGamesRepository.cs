namespace ReversiApi.Repository.Contracts;

/// <summary>
/// Provides an interface for games repository.
/// </summary>
public interface IGamesRepository : IRepository<GameEntity>
{

    /// <summary>
    /// Returns all games of the queue.
    /// </summary>
    /// <param name="status">The status to filter on.</param>
    /// <returns>The found games.</returns>
    IEnumerable<GameEntity> AllByStatus(string? status);

    /// <summary>
    /// Determines if this player does not player another game.
    /// </summary>
    /// <param name="playerEntity">The player.</param>
    /// <returns>True if the player does not play another game.</returns>
    bool DoesNotPlayAGame(PlayerEntity? playerEntity);

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
    /// <param name="status">The status to filter on.</param>
    /// <returns>The game.</returns>
    GameEntity? GetByPlayerOne(string? token, string? status = null);

    /// <summary>
    /// Gets the game by the token of player two.
    /// </summary>
    /// <param name="token">The unique token of the game.</param>
    /// <param name="status">The status to filter on.</param>
    /// <returns>The game.</returns>
    GameEntity? GetByPlayerTwo(string? token, string? status = null);
}
