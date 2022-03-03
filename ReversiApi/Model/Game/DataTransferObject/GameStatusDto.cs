using Newtonsoft.Json;

#nullable enable
namespace ReversiApi.Model.Game.DataTransferObject;

public class GameStatusDto
{

    private readonly GameEntity? _game;
    public string Board => JsonConvert.SerializeObject(this._game?.Game.Board);
    public PlayerInfoDto CurrentPlayer => new PlayerInfoDto(this._game?.Game.CurrentPlayer);
    public string? Status => this._game?.Status.ToString();
    
    public GameStatusDto(GameEntity? game)
    {
        this._game = game;
    }
    
}