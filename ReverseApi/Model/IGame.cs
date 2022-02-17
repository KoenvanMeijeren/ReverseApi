namespace ReverseApi.Model;

public enum Color
{
    None, 
    White, 
    Black
};

public interface IGame
{
    int Id { get; set; }
    string Description { get; set; }
    // The unique token of the game.
    string Token { get; set; }
    string TokenPlayerOne { get; set; }
    string TokenPlayerTwo { get; set; }
    Color[,] Board { get; set; }
    Color CurrentPlayer { get; set; }
        
    /// <summary>
    /// Allows the player to skip his turn, if possible. Otherwise throws an exception if there are moves possible. 
    /// </summary>
    void SkipTurn();
        
    /// <summary>
    /// Whether the game has been finished or not.
    /// </summary>
    /// <returns></returns>
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