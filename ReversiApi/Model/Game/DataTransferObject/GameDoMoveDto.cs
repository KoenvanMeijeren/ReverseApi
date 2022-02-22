namespace ReversiApi.Model.Game.DataTransferObject;

public class GameDoMoveDto
{
    public string? Token { get; init; }
    public string? PlayerToken { get; init; }
    public int Row { get; init; }
    public int Column { get; init; }

}