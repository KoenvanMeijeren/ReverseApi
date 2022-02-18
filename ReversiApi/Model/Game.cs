namespace ReversiApi.Model;

/// <summary>
/// Provides the Game class for the Reversi game.
/// </summary>
public class Game : IGame
{
    #region Fields

    private const int BoardSize = 8;
    
    private readonly int[,] _direction = new int[8, 2] {
        {  0,  1 },         // rightwards
        {  0, -1 },         // leftwards
        {  1,  0 },         // downwards
        { -1,  0 },         // towards
        {  1,  1 },         // downwards to right
        {  1, -1 },         // downwards to left
        { -1,  1 },         // towards to right
        { -1, -1 }          // towards to left
    };

    #endregion

    #region Properties

    public int Id { get; set; }
    public string Description { get; set; }
    public string Token { get; set; }
    public string TokenPlayerOne { get; set; }
    public string TokenPlayerTwo { get; set; }
    public Color[,] Board { get; set; }
    public Color CurrentPlayer { get; set; }

    #endregion
    
    public Game()
    {
        this.Token = Game.GenerateToken();

        this.Board = new Color[BoardSize, BoardSize];
        this.Board[3, 3] = Color.White;
        this.Board[4, 4] = Color.White;
        this.Board[3, 4] = Color.Black;
        this.Board[4, 3] = Color.Black;

        this.CurrentPlayer = Color.None;
    }

    /// <summary>
    /// Generates the token for the game.
    ///
    /// Avoids using the '/' and '+' symbols, because the requests are done via API calls with the token as ID.
    /// </summary>
    /// <returns>The generated token.</returns>
    private static string GenerateToken()
    {
        return Convert
            .ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("/", "q")
            .Replace("+", "r");
    }

    /// <inheritdoc/>
    public void SkipTurn()
    {
        if (this.AreMovesPossible(this.CurrentPlayer))
        {
            throw new Exception("Passen mag niet, er is nog een zet mogelijk");
        }

        this.ChangeTurn();
    }

    /// <inheritdoc/>
    public bool IsFinished()
    {
        for (int row = 0; row < BoardSize; row++)
        {
            for (int column = 0; column < BoardSize; column++)
            {
                if (this.IsMovePossible(row, column, Color.Black) 
                    || this.IsMovePossible(row, column, Color.White))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <inheritdoc/>
    public Color PredominantColor()
    {
        int whiteCount = 0, blackCount = 0;
        for (int row = 0; row < BoardSize; row++)
        {
            for (int column = 0; column < BoardSize; column++)
            {
                switch (this.Board[row, column])
                {
                    case Color.White:
                        whiteCount++;
                        continue;
                    case Color.Black:
                        blackCount++;
                        continue;
                    default:
                        continue;
                }
            }
        }

        if (whiteCount > blackCount)
        {
            return Color.White;
        }

        if (blackCount > whiteCount)
        {
            return Color.Black;
        }
        
        return Color.None;
    }

    /// <inheritdoc/>
    public bool IsMovePossible(int row, int column)
    {
        if (!Game.PositionInsideBoardBoundaries(row, column))
        {
            throw new Exception($"Zet ({row},{column}) ligt buiten het bord!");
        }
        
        return this.IsMovePossible(row, column, this.CurrentPlayer);
    }

    /// <inheritdoc/>
    public void DoMove(int row, int column)
    {
        if (!this.IsMovePossible(row, column))
        {
            throw new Exception($"Zet ({row},{column}) is niet mogelijk!");
        }

        for (int delta = 0; delta < BoardSize; delta++)
        {
            var rowDirection = this._direction[delta, 0];
            var columnDirection = this._direction[delta, 1];

            this.FlipOpponentStones(row, column, this.CurrentPlayer, rowDirection, columnDirection);
        }
        
        this.Board[row, column] = this.CurrentPlayer;
        this.ChangeTurn();
    }

    /// <summary>
    /// Gets the color of the opponent.
    /// </summary>
    /// <param name="color">The color of the current player.</param>
    /// <returns>The color of the opponent.</returns>
    private static Color GetColorOpponent(Color color)
    {
        return color switch
        {
            Color.White => Color.Black,
            Color.Black => Color.White,
            _ => Color.None
        };
    }

    /// <summary>
    /// Determines if there are any moves possible.
    /// </summary>
    /// <param name="color">The color of the player.</param>
    /// <returns>True if there are available moves.</returns>
    /// <exception cref="Exception">If an invalid color has been given.</exception>
    private bool AreMovesPossible(Color color)
    {
        if (color == Color.None)
        {
            throw new Exception("Kleur mag niet gelijk aan Geen zijn!");
        }
        
        // Checks if there is a move possible for a color.
        for (int row = 0; row < BoardSize; row++)
        {
            for (int column = 0; column < BoardSize; column++)
            {
                if (this.IsMovePossible(row, column, color))
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Determines if the move is possible.
    /// </summary>
    /// <param name="row">The row to move to.</param>
    /// <param name="column">The column of the row.</param>
    /// <param name="color">The color of the player.</param>
    /// <returns>True if the move is possible.</returns>
    private bool IsMovePossible(int row, int column, Color color)
    {
        for (int delta = 0; delta < BoardSize; delta++)
        {
            if (this.CanMakeMoveAndFlipOpponentStones(row, column, color, this._direction[delta, 0], this._direction[delta, 1]))
            {
                return true;
            }
        }
        
        return false;
    }

    /// <summary>
    /// Changes the turn to the opposite player.
    /// </summary>
    private void ChangeTurn()
    {
        this.CurrentPlayer = this.CurrentPlayer == Color.White ? Color.Black : Color.White;
    }

    /// <summary>
    /// Whether the position inside the boundaries of the board or not.
    /// </summary>
    /// <param name="row">The row to move to.</param>
    /// <param name="column">The column of the row.</param>
    /// <returns>True when the position is inside the boundaries.</returns>
    private static bool PositionInsideBoardBoundaries(int row, int column)
    {
        return row is >= 0 and < BoardSize && column is >= 0 and < BoardSize;
    }
    
    /// <summary>
    /// Determines if the position is not filled.
    /// </summary>
    /// <param name="row">The row to move to.</param>
    /// <param name="column">The column of the row.</param>
    /// <returns>True when the position is not filled.</returns>
    private bool PositionIsNotFilled(int row, int column)
    {
        return this.Board[row, column] == Color.None;
    }

    /// <summary>
    /// Checks if the move is inside the board and not already filled.
    /// </summary>
    /// <param name="row">The row to move to.</param>
    /// <param name="column">The column of the row.</param>
    /// <returns>True when the move is possible.</returns>
    private bool IsMoveInsideBoardAndFree(int row, int column)
    {
        return Game.PositionInsideBoardBoundaries(row, column) && this.PositionIsNotFilled(row, column);
    }

    /// <summary>
    /// Whether the move can be made and the stones of the opponent can be flipped.
    /// </summary>
    /// <param name="row">The current row.</param>
    /// <param name="column">The column of the row.</param>
    /// <param name="colorPlayer">The color of the player.</param>
    /// <param name="rowDirection">The row to move to.</param>
    /// <param name="columnDirection">The column of the row.</param>
    /// <returns>True if the stones can be flipped.</returns>
    private bool CanMakeMoveAndFlipOpponentStones(int row, int column, Color colorPlayer, int rowDirection, int columnDirection)
    {
        if (!this.IsMoveInsideBoardAndFree(row, column))
        {
            return false;
        }
        
        // Initializes the row and column on the index before the first box next to the move.
        var currentRow = row + rowDirection;
        var currentColumn = column + columnDirection;
        Color colorOpponent = Game.GetColorOpponent(colorPlayer);

        int adjacentOpponentStones = 0;
        // Zolang Bord[rij,kolom] niet buiten de bordgrenzen ligt, en je in het volgende vakje 
        // steeds de kleur van de tegenstander treft, ga je nog een vakje verder kijken.
        // Bord[rij, kolom] ligt uiteindelijk buiten de bordgrenzen, of heeft niet meer de
        // de kleur van de tegenstander.
        // N.b.: deel achter && wordt alleen uitgevoerd als conditie daarvoor true is.
        while (Game.PositionInsideBoardBoundaries(currentRow, currentColumn) && this.Board[currentRow, currentColumn] == colorOpponent)
        {
            currentRow += rowDirection;
            currentColumn += columnDirection;
            adjacentOpponentStones++;
        }

        // Nu kijk je hoe je geeindigt bent met bovenstaande loop. Alleen
        // als alle drie onderstaande condities waar zijn, zijn er in de
        // opgegeven richting stenen in te sluiten.
        return Game.PositionInsideBoardBoundaries(currentRow, currentColumn) 
               && this.Board[currentRow, currentColumn] == colorPlayer 
               && adjacentOpponentStones > 0;
    }

    /// <summary>
    /// Flips the stones of the opponent.
    /// </summary>
    /// <param name="row">The current row.</param>
    /// <param name="column">The column of the row.</param>
    /// <param name="colorPlayer">The color of the player.</param>
    /// <param name="rowDirection">The row to move to.</param>
    /// <param name="columnDirection">The column of the row.</param>
    /// <returns>True if the stones where flipped.</returns>
    private bool FlipOpponentStones(int row, int column, Color colorPlayer, int rowDirection, int columnDirection)
    {
        if (!this.CanMakeMoveAndFlipOpponentStones(row, column, colorPlayer, rowDirection, columnDirection))
        {
            return false;
        }
     
        Color colorOpponent = Game.GetColorOpponent(colorPlayer);
        var currentRow = row + rowDirection;
        var currentColumn = column + columnDirection;

        // We are sure that we cannot reach a position outside the board boundaries, because the stones of the 
        // opponent are surrounded by the stones of the current player.
        while (this.Board[currentRow, currentColumn] == colorOpponent)
        {
            this.Board[currentRow, currentColumn] = colorPlayer;
            currentRow += rowDirection;
            currentColumn += columnDirection;
        }
        
        return true;
    }
}
