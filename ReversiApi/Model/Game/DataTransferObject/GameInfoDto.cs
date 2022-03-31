using Newtonsoft.Json;

#nullable enable
namespace ReversiApi.Model.Game.DataTransferObject;

public class GameInfoDto
{

    private readonly GameEntity? _entity;

    public int? Id => this._entity?.Id;
    public string? Description => this._entity?.Description;
    public string? Token => this._entity?.Token;
    public PlayerInfoDto PlayerOne => new PlayerInfoDto(this._entity?.Game.PlayerOne, this._entity?.PlayerOne);
    public PlayerInfoDto PlayerTwo => new PlayerInfoDto(this._entity?.Game.PlayerTwo, this._entity?.PlayerTwo);
    public PlayerInfoDto CurrentPlayer => new PlayerInfoDto(this._entity?.Game.CurrentPlayer, this._entity?.CurrentPlayer);
    public string Board => JsonConvert.SerializeObject(this._entity?.Board);
    public string PossibleMoves => JsonConvert.SerializeObject(this._entity?.Game.GetPossibleMoves());
    public string? PredominantColor => this._entity?.Game.PredominantColor().ToString();
    public string? Status => this._entity?.Status.ToString();
    public int ConqueredWhiteFiches => this._entity?.ConqueredWhiteFiches ?? 0;
    public int ConqueredBlackFiches => this._entity?.ConqueredBlackFiches ?? 0;

    public GameInfoDto(GameEntity? entity)
    {
        this._entity = entity;
    }

}
