using NUnit.Framework;
using ReverseApi.Model;
using ReverseApi.Model.DataTransferObject.Game;

namespace Tests.Model.DataTransferObject;

[TestFixture]
public class GameDtoTests
{

    [Test]
    public void CanCreate_GameCreateDto()
    {
        var dto = new GameCreateDto
        {
            Description = "test",
            TokenPlayerOne = "abcdef"
        };
        
        Assert.AreEqual("test", dto.Description);
        Assert.AreEqual("abcdef", dto.TokenPlayerOne);
    }
    
    [Test]
    public void CanCreateEmpty_GameCreateDto()
    {
        var dto = new GameCreateDto();
        
        Assert.IsNull(dto.Description);
        Assert.IsNull(dto.TokenPlayerOne);
    }

    [Test]
    public void CanCreate_GameInfoDto()
    {
        IGame game = new Game();
        game.TokenPlayerOne = "abcdef";
        game.Description = "Potje snel reveri, dus niet lang nadenken";
        
        var dto = new GameInfoDto(game);
        
        Assert.IsNotNull(dto.Token);
        Assert.AreEqual("abcdef", dto.TokenPlayerOne);
        Assert.AreEqual("Potje snel reveri, dus niet lang nadenken", dto.Description);
    }
    
    [Test]
    public void CanCreateEmpty_GameInfoDto()
    {
        var dto = new GameInfoDto(null);
        
        Assert.IsNull(dto.Id);
        Assert.IsNull(dto.Description);
        Assert.IsNull(dto.Token);
        Assert.IsNull(dto.TokenPlayerOne);
        Assert.IsNull(dto.TokenPlayerTwo);
    }
    
}