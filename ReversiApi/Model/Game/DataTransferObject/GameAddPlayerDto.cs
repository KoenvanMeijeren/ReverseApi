namespace ReversiApi.Model.Game.DataTransferObject;

public class GameAddPlayerDto
{
    public string? Token { get; init; }
    public string? PlayerToken { get; init; }
    public string? Name { get; init; }

    /// <summary>
    /// Determines if this DTO is valid configured.
    /// </summary>
    /// <returns>True if the data is valid.</returns>
    public bool ValidData()
    {
        return this.Token != null && this.PlayerToken != null;
    }

}