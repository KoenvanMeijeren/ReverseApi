using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseApi.Model
{
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
        
        private Color[,] _bord;

        #endregion

        #region Properties

        public int Id { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public string TokenPlayerOne { get; set; }
        public string TokenPlayerTwo { get; set; }

        public Color[,] Board
        {
            get => this._bord;
            set => this._bord = value;
        }

        public Color HasTurn { get; set; }

        #endregion
        
        public Game()
        {
            this.Token = Game.GenerateToken();

            this.Board = new Color[BoardSize, BoardSize];
            this.Board[3, 3] = Color.White;
            this.Board[4, 4] = Color.White;
            this.Board[3, 4] = Color.Black;
            this.Board[4, 3] = Color.Black;

            this.HasTurn = Color.None;
        }

        private static string GenerateToken()
        {
            // Avoid using the '/' and '+' symbols, because the requests are done via API calls with the token as ID.
            return Convert
                .ToBase64String(Guid.NewGuid().ToByteArray())
                .Replace("/", "q")
                .Replace("+", "r");
        }

        /// <inheritdoc/>
        public void SkipTurn()
        {
            if (this.AreMovesPossible(this.HasTurn))
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
                    if (this.MovePossible(row, column, Color.Black) || this.MovePossible(row, column, Color.White))
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
                    switch (this._bord[row, column])
                    {
                        case Color.White:
                            whiteCount++;
                            continue;
                        case Color.Black:
                            blackCount++;
                            continue;
                        case Color.None:
                            continue;
                        default:
                            throw new ArgumentOutOfRangeException();
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
            
            return this.MovePossible(row, column, this.HasTurn);
        }

        /// <inheritdoc/>
        public void DoMove(int row, int column)
        {
            // todo: maak hierbij gebruik van de reeds in deze klassen opgenomen methoden!
            
            throw new NotImplementedException();
        }

        private static Color GetColorOpponent(Color color)
        {
            return color switch
            {
                Color.White => Color.Black,
                Color.Black => Color.White,
                _ => Color.None
            };
        }

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
                    if (this.MovePossible(row, column, color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool MovePossible(int row, int column, Color color)
        {
            for (int delta = 0; delta < BoardSize; delta++)
            {
                {
                    if (this.CanFlipOpponentStones(
                            row, column, color, 
                            this._direction[delta, 0], this._direction[delta, 1]))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        private void ChangeTurn()
        {
            this.HasTurn = this.HasTurn == Color.White ? Color.Black : Color.White;
        }

        private static bool PositionInsideBoardBoundaries(int row, int column)
        {
            return row is >= 0 and < BoardSize && column is >= 0 and < BoardSize;
        }
        
        private bool PositionIsNotFilled(int row, int column)
        {
            return this.Board[row, column] == Color.None;
        }

        private bool MoveInsideBoardAndFree(int row, int column)
        {
            return Game.PositionInsideBoardBoundaries(row, column) && this.PositionIsNotFilled(row, column);
        }

        private bool CanFlipOpponentStones(int row, int column, Color colorPlayer, int rowDirection, int columnDirection)
        {
            if (!this.MoveInsideBoardAndFree(row, column))
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

        private bool FlipOpponentStones(int row, int column, Color colorPlayer, int rowDirection, int columnDirection)
        {
            if (!this.CanFlipOpponentStones(row, column, colorPlayer, rowDirection, columnDirection))
            {
                return false;
            }
         
            Color colorOpponent = Game.GetColorOpponent(colorPlayer);
            var currentRow = row + rowDirection;
            var currentColumn = column + columnDirection;

            // N.b.: je weet zeker dat je niet buiten het bord belandt,
            // omdat de stenen van de tegenstander ingesloten zijn door
            // een steen van degene die de zet doet.
            while (this.Board[currentRow, currentColumn] == colorOpponent)
            {
                this.Board[currentRow, currentColumn] = colorPlayer;
                currentRow += rowDirection;
                currentColumn += columnDirection;
            }
            
            return true;
        }
    }
}
