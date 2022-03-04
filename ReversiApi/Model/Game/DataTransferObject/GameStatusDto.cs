using Newtonsoft.Json;

#nullable enable
namespace ReversiApi.Model.Game.DataTransferObject;

public class GameStatusDto
{

    private readonly GameEntity? _entity;
    public string Board => JsonConvert.SerializeObject(this._entity?.Game.Board);
    public PlayerInfoDto CurrentPlayer => new PlayerInfoDto(this._entity?.Game.CurrentPlayer, this._entity?.CurrentPlayer);
    public string? Status => this._entity?.Status.ToString();
    
    public GameStatusDto(GameEntity? entity)
    {
        this._entity = entity;
    }
    
}