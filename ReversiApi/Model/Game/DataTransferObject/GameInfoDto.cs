using Newtonsoft.Json;
using ReversiApi.Model.Player;

#nullable enable
namespace ReversiApi.Model.Game.DataTransferObject;

public class GameInfoDto
{

    private readonly IGame? _game;
    
    public int? Id => this._game?.Id;
    public string? Description => this._game?.Description;
    public string? Token => this._game?.Token;
    public PlayerOne? PlayerOne => this._game?.PlayerOne;
    public PlayerTwo? PlayerTwo => this._game?.PlayerTwo;
    public string? Board => JsonConvert.SerializeObject(this._game?.Board);
    public string? CurrentPlayer => this._game?.CurrentPlayer.Color.ToString();
    public string? Status => this._game?.Status.ToString();
    
    public GameInfoDto(IGame? game)
    {
        this._game = game;
    }
    
}