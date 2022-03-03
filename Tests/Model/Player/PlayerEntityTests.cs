using NUnit.Framework;
using ReversiApi.Model;
using ReversiApi.Model.Game;
using ReversiApi.Model.Player;

namespace Tests.Model.Player;

[TestFixture]
public class PlayerEntityTests
{

    [Test]
    public void CreatePlayerEntity_Empty()
    {
        // Arrange
        var player = new PlayerEntity();

        // Act

        // Assert
        Assert.AreEqual(IEntity.IdUndefined, player.Id);
        Assert.AreEqual(Color.None, player.Color);
        Assert.AreEqual("", player.Token);
        Assert.IsEmpty(player.GamesPlayerOne);
        Assert.IsEmpty(player.GamesPlayerTwo);
        Assert.IsInstanceOf<int>(player.GetHashCode());
    }
    
    [Test]
    public void CreatePlayerEntity_NotEmpty()
    {
        // Arrange
        var player = new PlayerEntity(11, "test", Color.White);

        // Act

        // Assert
        Assert.AreEqual(11, player.Id);
        Assert.AreEqual(Color.White, player.Color);
        Assert.AreEqual("test", player.Token);
        Assert.IsInstanceOf<int>(player.GetHashCode());
    }
    
    [Test]
    public void CreatePlayerEntity_FromPlayerOne()
    {
        // Arrange
        var player = new PlayerEntity(new PlayerOne("abcdef"), 5);

        // Act

        // Assert
        Assert.AreEqual(5, player.Id);
        Assert.AreEqual(Color.White, player.Color);
        Assert.AreEqual("abcdef", player.Token);
        Assert.IsInstanceOf<int>(player.GetHashCode());
    }
    
    [Test]
    public void CreatePlayerEntity_FromPlayerTwo()
    {
        // Arrange
        var player = new PlayerEntity(new PlayerTwo("qwerty"), 5);

        // Act

        // Assert
        Assert.AreEqual(5, player.Id);
        Assert.AreEqual(Color.Black, player.Color);
        Assert.AreEqual("qwerty", player.Token);
        Assert.IsInstanceOf<int>(player.GetHashCode());
    }

    [Test]
    public void CreatePlayerEntity_FromPlayerUndefined()
    {
        // Arrange
        var player = new PlayerEntity(new PlayerUndefined(), 5);

        // Act

        // Assert
        Assert.AreEqual(5, player.Id);
        Assert.AreEqual(Color.None, player.Color);
        Assert.AreEqual("", player.Token);
        Assert.IsInstanceOf<int>(player.GetHashCode());
    }
    
}