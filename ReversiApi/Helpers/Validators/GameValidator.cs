namespace ReversiApi.Helpers.Validators;

public static class GameValidator
{
    /// <summary>
    /// Determines if this player does not player another game.
    /// </summary>
    /// <param name="games">The games.</param>
    /// <param name="playerEntity">The player.</param>
    /// <returns>True if the player does not play another game.</returns>
    public static bool PlayerDoesNotPlayAGame(IEnumerable<GameEntity> games, PlayerEntity? playerEntity)
    {
        if (playerEntity == null)
            
        {
            return true;
        }

        var playsAGame = false;
        foreach (var entity in games)
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
}
