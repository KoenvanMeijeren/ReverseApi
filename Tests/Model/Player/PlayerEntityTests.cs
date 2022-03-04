using NUnit.Framework;
using ReversiApi.Model;
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
        Assert.AreEqual("", player.Token);
        Assert.AreEqual("", player.Name);
        Assert.IsEmpty(player.GamesPlayerOne);
        Assert.IsEmpty(player.GamesPlayerTwo);
        Assert.IsInstanceOf<int>(player.GetHashCode());
    }
    
    [Test]
    public void CreatePlayerEntity_NotEmpty()
    {
        // Arrange
        var player = new PlayerEntity(11, "test", "Teddy");

        // Act

        // Assert
        Assert.AreEqual(11, player.Id);
        Assert.AreEqual("test", player.Token);
        Assert.AreEqual("Teddy", player.Name);
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
        Assert.IsTrue(player.Equals(new PlayerEntity(1, "abcdef", "Teddy")));
        Assert.IsTrue(player.Equals(new PlayerEntity(21, "abcdef", "Teddy")));
        
        Assert.IsTrue(player2.Equals(new PlayerEntity(new PlayerTwo("qwerty"))));
        Assert.IsTrue(player2.Equals(new PlayerEntity(2, "qwerty", "Hein")));
        Assert.IsTrue(player2.Equals(new PlayerEntity(2, "qwerty", "Jessica")));
        
        Assert.IsTrue(player3.Equals(new PlayerEntity(new PlayerUndefined())));
        Assert.IsTrue(player3.Equals(new PlayerEntity(3)));
        Assert.IsTrue(player3.Equals(new PlayerEntity(new PlayerOne())));
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
        Assert.IsFalse(player.Equals(new PlayerEntity(1, "fdasfas", "John")));
        
        Assert.IsFalse(player2.Equals(new PlayerEntity(new PlayerTwo("fdafderqw"))));
        Assert.IsFalse(player2.Equals(new PlayerEntity(2, "vczafda", "Jessica")));
        
        Assert.IsFalse(player3.Equals(new PlayerEntity(3, "test")));
        
        Assert.IsFalse(player.Equals(null));
        Assert.IsFalse(player.Equals("test"));
    }
    
}