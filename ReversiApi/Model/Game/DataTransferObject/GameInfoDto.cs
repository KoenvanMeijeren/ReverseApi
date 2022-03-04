using Newtonsoft.Json;

#nullable enable
namespace ReversiApi.Model.Game.DataTransferObject;

public class GameInfoDto
{

    private readonly GameEntity? _entity;
    
    public int? Id => this._entity?.Id;
    public string? Description => this._entity?.Description;
    public string? Token => this._entity?.Token;
    public PlayerInfoDto PlayerOne => new PlayerInfoDto(this._entity?.Game.PlayerOne);
    public PlayerInfoDto PlayerTwo => new PlayerInfoDto(this._entity?.Game.PlayerTwo);
    public string Board => JsonConvert.SerializeObject(this._entity?.Board);
    public PlayerInfoDto CurrentPlayer => new PlayerInfoDto(this._entity?.Game.CurrentPlayer);
    public string? Status => this._entity?.Status.ToString();
    
    public GameInfoDto(GameEntity? entity)
    {
        this._entity = entity;

        var playerOne = this.PlayerOne;
        var playerTwo = this.PlayerTwo;
    }
    
}