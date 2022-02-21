using Newtonsoft.Json;

#nullable enable
namespace ReversiApi.Model.Game.DataTransferObject;

public class GameStatusDto
{

    private readonly IGame? _game;
    
    public string? Board => JsonConvert.SerializeObject(this._game?.Board);
    public string? CurrentPlayerColor => this._game?.CurrentPlayer.Color.ToString();
    public string? Status => this._game?.Status.ToString();
    
    public GameStatusDto(IGame? game)
    {
        this._game = game;
    }
    
}