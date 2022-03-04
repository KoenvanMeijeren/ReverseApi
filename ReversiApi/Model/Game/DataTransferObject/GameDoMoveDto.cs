namespace ReversiApi.Model.Game.DataTransferObject;

public class GameDoMoveDto
{
    public string? Token { get; init; }
    public string? PlayerToken { get; init; }
    public int Row { get; init; }
    public int Column { get; init; }

    /// <summary>
    /// Determines if this DTO is valid configured.
    /// </summary>
    /// <returns>True if the data is valid.</returns>
    public bool ValidData()
    {
        return this.Token != null
               && this.PlayerToken != null
               && this.Row > 0
               && this.Column > 0;
    }
    
}