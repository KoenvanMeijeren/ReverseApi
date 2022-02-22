using Newtonsoft.Json;

#nullable enable
namespace ReversiApi.Model.Game.DataTransferObject;

public class GameInfoDto
{

    private readonly IGame? _game;
    
    public int? Id => this._game?.Id;
    public string? Description => this._game?.Description;
    public string? Token => this._game?.Token;
    public PlayerInfoDto PlayerOne => new PlayerInfoDto(this._game?.PlayerOne);
    public PlayerInfoDto PlayerTwo => new PlayerInfoDto(this._game?.PlayerTwo);
    public string Board => JsonConvert.SerializeObject(this._game?.Board);
    public PlayerInfoDto CurrentPlayer => new PlayerInfoDto(this._game?.CurrentPlayer);
    public string? Status => this._game?.Status.ToString();
    
    public GameInfoDto(IGame? game)
    {
        this._game = game;
    }
    
}