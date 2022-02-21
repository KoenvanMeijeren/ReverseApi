namespace ReversiApi.Model.Game.DataTransferObject;

public class GameDoMoveDto
{
    public string? Token { get; set; }
    public string? PlayerToken { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }

}