namespace ReversiApi.Model.Player.DataTransferObject;

public class PlayerInfoDto
{
    private readonly IPlayer? _player;
    private readonly PlayerEntity? _entity;

    public string? Token => this._entity?.Token;
    public string? Color => this._player?.Color.ToString();
    public string? Name => this._entity?.Name;

    public PlayerInfoDto(IPlayer? player, PlayerEntity? entity)
    {
        this._player = player;
        this._entity = entity;
    }

}
