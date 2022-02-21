using Newtonsoft.Json;
using ReversiApi.Model.Player;

#nullable enable
namespace ReversiApi.Model.Game.DataTransferObject;

public class GameStatusDto
{

    private readonly IGame? _game;
    public string? Board => JsonConvert.SerializeObject(this._game?.Board);
    public IPlayer? CurrentPlayer => this._game?.CurrentPlayer;
    public Status? Status => this._game?.Status;
    
    public GameStatusDto(IGame? game)
    {
        this._game = game;
    }
    
}