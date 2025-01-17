﻿namespace ReversiApi.Repository.Contracts;

/// <summary>
/// Provides an interface for games repository.
/// </summary>
public interface IPlayersRepository : IRepository<PlayerEntity>
{

    /// <summary>
    /// Returns the first player or creates one.
    /// </summary>
    /// <param name="playerEntity">The player.</param>
    /// <returns>The first or created player.</returns>
    PlayerEntity FirstOrCreate(PlayerEntity playerEntity);

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
    PlayerEntity? Get(string? token);

}
