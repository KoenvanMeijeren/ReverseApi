namespace ReversiApi.Model.Player.DataTransferObject;

public class PlayerInfoDto
{
    private readonly IPlayer? _player;

    public string? Token => this._player?.Token;
    public string? Color => this._player?.Color.ToString();
    
    public PlayerInfoDto(IPlayer? player)
    {
        this._player = player;
    }
    
}