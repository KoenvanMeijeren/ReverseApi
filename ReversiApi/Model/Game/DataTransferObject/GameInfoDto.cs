using Newtonsoft.Json;

#nullable enable
namespace ReversiApi.Model.Game.DataTransferObject;

public class GameInfoDto
{

    private readonly IGame? _game;
    
    public int? Id => this._game?.Id;
    public string? Description => this._game?.Description;
    public string? Token => this._game?.Token;
    public string? TokenPlayerOne => this._game?.PlayerOne?.Token;
    public string? TokenPlayerTwo => this._game?.PlayerTwo?.Token;
    public string? Board => JsonConvert.SerializeObject(this._game?.Board);
    public string? CurrentPlayer => this._game?.CurrentPlayer.Color.ToString();
    public string? Status => this._game?.Status.ToString();
    
    public GameInfoDto(IGame? game)
    {
        this._game = game;
    }
    
}