#nullable enable
namespace ReverseApi.Model.DataTransferObject.Game;

public class GameInfoDto
{

    private readonly IGame? _game;
    
    public int? Id => this._game?.Id;
    public string? Description => this._game?.Description;
    public string? Token => this._game?.Token;
    public string? TokenPlayerOne => this._game?.TokenPlayerOne;
    public string? TokenPlayerTwo => this._game?.TokenPlayerTwo;
    
    public GameInfoDto(IGame? game)
    {
        this._game = game;
    }
    
}