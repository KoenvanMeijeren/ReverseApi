using ReversiApi.Model.Player;

namespace ReversiApi.Model.Game;

public enum Color
{
    None, 
    White, 
    Black
}

public enum Status
{
    Created,
    Queued,
    Pending,
    Playing,
    Finished,
    Quit
}

public interface IGame
{
    int Id { get; }
    string? Description { get; set; }
    // The unique token of the game.
    string Token { get; }
    PlayerOne? PlayerOne { get; set; }
    PlayerTwo? PlayerTwo { get; set; }
    IPlayer CurrentPlayer { get; set; }
    Color[,] Board { get; set; }
    Status Status { get; }
        
    /// <summary>
    /// Allows the player to skip his turn, if possible. Otherwise throws an exception if there are moves possible. 
    /// </summary>
    void SkipTurn();

    /// <summary>
    /// Whether the game has been created or not.
    /// </summary>
    /// <returns>True if the game is created.</returns>
    bool IsCreated();
    
    /// <summary>
    /// Changes the status to queued if one of the players is not set.
    /// </summary>
    void Queue();
    
    /// <summary>
    /// Whether the game has been queued or not.
    /// </summary>
    /// <returns>True if the game is queued.</returns>
    bool IsQueued();

    /// <summary>
    /// Whether the game is waiting for changing the status or not.
    /// </summary>
    /// <returns>True if the game is pending.</returns>
    bool IsPending();
    
    /// <summary>
    /// Whether the game is played by the players or not.
    /// </summary>
    /// <returns>True if the game is playing.</returns>
    bool IsPlaying();

    /// <summary>
    /// Starts the game if both players are initialized.
    /// </summary>
    void Start();
    
    /// <summary>
    /// The current player quits the game.
    /// </summary>
    void Quit();
    
    /// <summary>
    /// Whether the game has been quit or not.
    /// </summary>
    /// <returns>True if the game is quit.</returns>
    bool IsQuit();

    /// <summary>
    /// Whether the game has been finished or not.
    /// </summary>
    /// <returns>True if the game is finished.</returns>
    bool IsFinished();
    
    /// <summary>
    /// Which color occurs the most on the game board.
    /// </summary>
    /// <returns></returns>
    Color PredominantColor();

    /// <summary>
    /// Determines if the move is possible.
    /// </summary>
    /// <param name="row">The row to move to.</param>
    /// <param name="column">The column of the row.</param>
    /// <returns></returns>
    bool IsMovePossible(int row, int column);
        
    /// <summary>
    /// Moves the player to a new position.
    /// </summary>
    /// <param name="row">The row to move to.</param>
    /// <param name="column">The column of the row.</param>
    void DoMove(int row, int column);
        
}