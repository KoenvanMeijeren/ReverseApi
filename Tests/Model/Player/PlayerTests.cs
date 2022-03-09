using NUnit.Framework;
using ReversiApi.Model.Game;
using ReversiApi.Model.Player;

namespace Tests.Model.Player;

[TestFixture]
public class PlayerTests
{

    [Test]
    public void CreatePlayerOne()
    {
        // Arrange
        IPlayer player = new PlayerOne();
        IPlayer player1 = new PlayerOne();

        // Act

        // Assert
        Assert.AreEqual(Color.White, player.Color);
        Assert.AreEqual("", player.Token);
        Assert.IsInstanceOf<int>(player.GetHashCode());
        Assert.IsTrue(player.Equals(player1));
        Assert.IsFalse(player.Equals(player1.Color));
    }

    [Test]
    public void CreatePlayerTwo()
    {
        // Arrange
        IPlayer player = new PlayerTwo();
        IPlayer player1 = new PlayerTwo();

        // Act

        // Assert
        Assert.AreEqual(Color.Black, player.Color);
        Assert.AreEqual("", player.Token);
        Assert.IsInstanceOf<int>(player.GetHashCode());
        Assert.IsTrue(player.Equals(player1));
        Assert.IsFalse(player.Equals(player1.Color));
    }

    [Test]
    public void CreatePlayerUndefined()
    {
        // Arrange
        IPlayer player = new PlayerUndefined();
        IPlayer player1 = new PlayerUndefined();

        // Act

        // Assert
        Assert.AreEqual(Color.None, player.Color);
        Assert.AreEqual("", player.Token);
        Assert.IsInstanceOf<int>(player.GetHashCode());
        Assert.IsTrue(player.Equals(player1));
        Assert.IsFalse(player.Equals(player1.Color));
    }

}
