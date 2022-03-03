using NUnit.Framework;
using ReversiApi.Model.Game;
using ReversiApi.Model.Game.DataTransferObject;
using ReversiApi.Model.Player;

namespace Tests.Model.GameTests.DataTransferObject;

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
        GameEntity entity = new GameEntity
        {
            PlayerOne = new PlayerEntity(new PlayerOne("abcdef")),
            Description = "Potje snel reveri, dus niet lang nadenken"
        };
        entity.UpdateGame();
        
        var dto = new GameInfoDto(entity);
        
        Assert.IsNotNull(dto.Token);
        Assert.AreEqual(new PlayerUndefined().Color.ToString(), dto.CurrentPlayer.Color);
        Assert.IsTrue(dto.Board?.Contains("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0"));
        Assert.AreEqual("abcdef", dto.PlayerOne.Token);
        Assert.AreEqual("Potje snel reveri, dus niet lang nadenken", dto.Description);
        Assert.AreEqual(Status.Created.ToString(), dto.Status);
    }
    
    [Test]
    public void CanCreateEmpty_GameInfoDto()
    {
        var dto = new GameInfoDto(null);
        
        Assert.IsNull(dto.Id);
        Assert.IsNull(dto.CurrentPlayer.Color);
        Assert.IsNull(dto.CurrentPlayer.Token);
        Assert.AreEqual("null", dto.Board);
        Assert.IsNull(dto.Description);
        Assert.IsNull(dto.Token);
        Assert.IsNull(dto.Status);
        Assert.IsNull(dto.PlayerOne.Color);
        Assert.IsNull(dto.PlayerOne.Token);
        Assert.IsNull(dto.PlayerTwo.Color);
        Assert.IsNull(dto.PlayerTwo.Token);
    }
    
    [Test]
    public void CanCreate_GameStatusDto()
    {
        GameEntity entity = new GameEntity
        {
            PlayerOne = new PlayerEntity(new PlayerOne("abcdef"))
        };
        entity.CurrentPlayer = entity.PlayerOne;
        entity.Description = "Potje snel reveri, dus niet lang nadenken";
        entity.UpdateGame();
        
        var dto = new GameStatusDto(entity);
        
        Assert.AreEqual(Color.None.ToString(), dto.CurrentPlayer.Color);
        Assert.IsTrue(dto.Board?.Contains("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0"));
        Assert.AreEqual(Status.Created.ToString(), dto.Status);
    }
    
    [Test]
    public void CanCreateEmpty_GameStatusDto()
    {
        var dto = new GameStatusDto(null);
        
        Assert.IsNull(dto.Status);
        Assert.IsNull(dto.CurrentPlayer.Color);
        Assert.IsNull(dto.CurrentPlayer.Token);
        Assert.AreEqual("null", dto.Board);
    }

    [Test]
    public void CanCreate_GameAddPlayerDto()
    {
        var dto = new GameAddPlayerDto()
        {
            Token = "test",
            PlayerToken = "qwert"
        };

        Assert.AreEqual("test", dto.Token);
        Assert.AreEqual("qwert", dto.PlayerToken);
    }
    
    [Test]
    public void CanCreateEmpty_GameAddPlayerDto()
    {
        var dto = new GameAddPlayerDto();
        
        Assert.IsNull(dto.Token);
        Assert.IsNull(dto.PlayerToken);
    }
    
    [Test]
    public void CanCreate_GameDoMoveDto()
    {
        var dto = new GameDoMoveDto
        {
            Token = "test",
            PlayerToken = "qwert",
            Row = 3,
            Column = 34
        };

        Assert.AreEqual("test", dto.Token);
        Assert.AreEqual("qwert", dto.PlayerToken);
        Assert.AreEqual(3, dto.Row);
        Assert.AreEqual(34, dto.Column);
    }
    
    [Test]
    public void CanCreateEmpty_GameDoMoveDto()
    {
        var dto = new GameDoMoveDto();
        
        Assert.IsNull(dto.Token);
        Assert.IsNull(dto.PlayerToken);
        Assert.AreEqual(0, dto.Row);
        Assert.AreEqual(0, dto.Column);
    }
    
}