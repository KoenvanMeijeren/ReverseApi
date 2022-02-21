using Newtonsoft.Json;
using ReversiApi.Model.Player.DataTransferObject;

#nullable enable
namespace ReversiApi.Model.Game.DataTransferObject;

public class GameStatusDto
{

    private readonly IGame? _game;
    public string? Board => JsonConvert.SerializeObject(this._game?.Board);
    public PlayerInfoDto CurrentPlayer => new PlayerInfoDto(this._game?.CurrentPlayer);
    public string? Status => this._game?.Status.ToString();
    
    public GameStatusDto(IGame? game)
    {
        this._game = game;
    }
    
}