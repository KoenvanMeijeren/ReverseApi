using NUnit.Framework;
using ReversiApi.Model;
using ReversiApi.Model.Game;
using ReversiApi.Model.Player;

namespace Tests.Model.GameTests;

[TestFixture]
public class GameEntityTests
{
    [Test]
    public void CreateGameEntity_Empty()
    {
        // Arrange
        var entity = new GameEntity();
        
        // Act

        // Assert
        Assert.AreEqual(Status.Created, entity.Status);
        Assert.AreEqual("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", entity.Board);
        Assert.IsInstanceOf<IGame>(entity.Game);
        Assert.IsNotEmpty(entity.Token);
        Assert.AreEqual(IEntity.IdUndefined, entity.Id);
        Assert.IsNull(entity.Description);
        Assert.IsNull(entity.PlayerOne);
        Assert.IsNull(entity.PlayerOneId);
        Assert.IsNull(entity.PlayerTwo);
        Assert.IsNull(entity.PlayerTwoId);
        Assert.IsNull(entity.CurrentPlayer);
    }
    
    [Test]
    public void CreateGameEntity_NotEmpty_WithoutUpdatingToGameModel()
    {
        // Arrange
        var entity = new GameEntity
        {
            Id = 12,
            Description = "test",
            PlayerOneId = 13,
            PlayerTwoId = 15,
            PlayerOne = new PlayerEntity(new PlayerOne("abcdef")),
            PlayerTwo = new PlayerEntity(new PlayerTwo("qwerty"))
        };

        // Act

        // Assert
        Assert.AreEqual(12, entity.Id);
        Assert.AreEqual(13, entity.PlayerOneId);
        Assert.AreEqual(15, entity.PlayerTwoId);
        Assert.AreEqual("abcdef", entity.PlayerOne.Token);
        Assert.AreEqual(Color.White, entity.PlayerOne.Color);
        Assert.AreEqual("qwerty", entity.PlayerTwo.Token);
        Assert.AreEqual(Color.Black, entity.PlayerTwo.Color);
        Assert.AreEqual(Status.Created, entity.Status);
        Assert.AreEqual("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", entity.Board);
        Assert.IsInstanceOf<IGame>(entity.Game);
        Assert.IsNotEmpty(entity.Token);
        Assert.IsNull(entity.Game.PlayerOne);
        Assert.IsNull(entity.Game.PlayerTwo);
        Assert.IsInstanceOf<PlayerUndefined>(entity.Game.CurrentPlayer);
    }
    
    [Test]
    public void CreateGameEntity_NotEmpty_WithUpdatingToGameModel()
    {
        // Arrange
        var entity = new GameEntity
        {
            Id = 12,
            Description = "test",
            PlayerOneId = 13,
            PlayerTwoId = 15,
            PlayerOne = new PlayerEntity(new PlayerOne("abcdef")),
            PlayerTwo = new PlayerEntity(new PlayerTwo("qwerty"))
        };
        entity.UpdateGame();

        // Act

        // Assert
        Assert.AreEqual(12, entity.Id);
        Assert.AreEqual(13, entity.PlayerOneId);
        Assert.AreEqual(15, entity.PlayerTwoId);
        Assert.AreEqual("abcdef", entity.PlayerOne.Token);
        Assert.AreEqual("abcdef", entity.Game.PlayerOne.Token);
        Assert.AreEqual(Color.White, entity.PlayerOne.Color);
        Assert.AreEqual(Color.White, entity.Game.PlayerOne.Color);
        Assert.AreEqual("qwerty", entity.PlayerTwo.Token);
        Assert.AreEqual("qwerty", entity.Game.PlayerTwo.Token);
        Assert.AreEqual(Color.Black, entity.PlayerTwo.Color);
        Assert.AreEqual(Color.Black, entity.Game.PlayerTwo.Color);
        Assert.AreEqual(Status.Created, entity.Status);
        Assert.AreEqual("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", entity.Board);
        Assert.IsInstanceOf<IGame>(entity.Game);
        Assert.IsNotEmpty(entity.Token);
        Assert.IsInstanceOf<PlayerUndefined>(entity.Game.CurrentPlayer);
    }
    
    [Test]
    public void CreateGameEntity_NotEmpty_WithoutUpdatingToEntity()
    {
        // Arrange
        var entity = new GameEntity
        {
            Id = 12,
            Description = "test",
            PlayerOneId = 13,
            PlayerTwoId = 15,
            PlayerOne = new PlayerEntity(new PlayerOne("abcdef")),
            PlayerTwo = new PlayerEntity(new PlayerTwo("qwerty"))
        };
        entity.UpdateGame();

        // Act

        // Assert
        Assert.AreEqual("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", entity.Board);

        var previousCurrentPlayer = entity.CurrentPlayer;
        entity.Game.Start();
        entity.Game.DoMove(3, 5);
        
        Assert.AreEqual(Status.Created, entity.Status);
        Assert.AreEqual("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", entity.Board);
        Assert.AreEqual(previousCurrentPlayer, entity.CurrentPlayer);
    }
    
    [Test]
    public void CreateGameEntity_NotEmpty_WithUpdatingToEntity()
    {
        // Arrange
        var entity = new GameEntity
        {
            Id = 12,
            Description = "test",
            PlayerOneId = 13,
            PlayerTwoId = 15,
            PlayerOne = new PlayerEntity(new PlayerOne("abcdef")),
            PlayerTwo = new PlayerEntity(new PlayerTwo("qwerty"))
        };
        entity.UpdateGame();

        // Act

        // Assert
        Assert.AreEqual("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", entity.Board);

        var previousCurrentPlayer = entity.CurrentPlayer;
        entity.Game.Start();
        entity.Game.DoMove(3, 5);
        entity.UpdateEntity();
        
        Assert.AreEqual(Status.Playing, entity.Status);
        Assert.AreEqual("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,1,1,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", entity.Board);
        Assert.AreNotEqual(previousCurrentPlayer, entity.CurrentPlayer);
    }

    [Test]
    public void CreateGameEntity_NotEmpty_StatusChangesToPending()
    {
        // Arrange
        var entity = new GameEntity
        {
            Id = 12,
            Description = "test",
            PlayerOneId = 13,
            PlayerTwoId = 15,
            PlayerOne = new PlayerEntity(new PlayerOne("abcdef")),
            PlayerTwo = new PlayerEntity(new PlayerTwo("qwerty"))
        };
        entity.UpdateGame();
        
        // Act
        entity.UpdateEntity();
        
        // Assert
        Assert.AreEqual(Status.Pending, entity.Status);
    }
    
}