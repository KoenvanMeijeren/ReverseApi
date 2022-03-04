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

    [Test]
    public void Player_IsEqual()
    {
        // Arrange
        var player = new PlayerEntity(new PlayerOne("abcdef"));
        var player2 = new PlayerEntity(new PlayerTwo("qwerty"));
        var player3 = new PlayerEntity(new PlayerUndefined());

        // Act


        // Assert
        Assert.IsTrue(player.Equals(new PlayerEntity(new PlayerOne("abcdef"))));
        Assert.IsTrue(player.Equals(new PlayerEntity(1, "abcdef", Color.White)));
        
        Assert.IsTrue(player2.Equals(new PlayerEntity(new PlayerTwo("qwerty"))));
        Assert.IsTrue(player2.Equals(new PlayerEntity(2, "qwerty", Color.Black)));
        
        Assert.IsTrue(player3.Equals(new PlayerEntity(new PlayerUndefined())));
        Assert.IsTrue(player3.Equals(new PlayerEntity(3)));
    }
    
    [Test]
    public void Player_IsNotEqual()
    {
        // Arrange
        var player = new PlayerEntity(new PlayerOne("abcdef"));
        var player2 = new PlayerEntity(new PlayerTwo("qwerty"));
        var player3 = new PlayerEntity(new PlayerUndefined());

        // Act


        // Assert
        Assert.IsFalse(player.Equals(new PlayerEntity(new PlayerOne("adfdas"))));
        Assert.IsFalse(player.Equals(new PlayerEntity(1, "fdasfas", Color.White)));
        Assert.IsFalse(player.Equals(new PlayerEntity(1, "abcdef", Color.Black)));
        
        Assert.IsFalse(player2.Equals(new PlayerEntity(new PlayerTwo("fdafderqw"))));
        Assert.IsFalse(player2.Equals(new PlayerEntity(2, "qwerty", Color.White)));
        Assert.IsFalse(player2.Equals(new PlayerEntity(2, "vczafda", Color.Black)));
        
        Assert.IsFalse(player3.Equals(new PlayerEntity(new PlayerOne())));
        Assert.IsFalse(player3.Equals(new PlayerEntity(3, "test", Color.Black)));
        
        Assert.IsFalse(player.Equals(null));
        Assert.IsFalse(player.Equals("test"));
    }
    
}