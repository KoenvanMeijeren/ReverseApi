using System;
using NUnit.Framework;
using ReversiApi.Model.Game;
using ReversiApi.Model.Player;

namespace Tests.Model.GameTests;

[TestFixture]
public class GameStatusTests
{

    [Test]
    public void CreatedGame()
    {
        // Arrange
        IGame game = new Game();

        // Act

        // Assert
        Assert.AreEqual(Status.Created, game.Status);
        Assert.IsTrue(game.IsCreated());
        Assert.IsTrue(game.IsQueued());
        Assert.AreEqual(Status.Created, game.Status);
    }

    [Test]
    public void QueuedGame()
    {
        // Arrange
        IGame game = new Game();

        // Act
        game.Queue();

        // Assert
        Assert.AreEqual(Status.Queued, game.Status);
        Assert.IsTrue(game.IsQueued());
    }

    [Test]
    public void QueuedGame_CannotIfPlayersArePresent()
    {
        // Arrange
        IGame game = new Game();

        // Act
        game.PlayerOne = new PlayerOne();
        game.PlayerTwo = new PlayerTwo();
        game.Queue();

        // Assert
        Assert.AreEqual(Status.Pending, game.Status);
        Assert.IsTrue(game.IsPending());
        Assert.IsFalse(game.IsQueued());
    }

    [Test]
    public void StartGame()
    {
        // Arrange
        IGame game = new Game();

        // Act
        game.PlayerOne = new PlayerOne();
        game.PlayerTwo = new PlayerTwo();
        game.Start();

        // Assert
        Assert.AreEqual(Status.Playing, game.Status);
        Assert.IsTrue(game.IsPlaying());
    }

    [Test]
    public void StartGame_CannotWithEmptyPlayers()
    {
        // Arrange
        IGame game = new Game();

        // Act
        var ex = Assert.Throws<Exception>(delegate { game.Start(); });
        Assert.That(ex.Message, Is.EqualTo("Game kan niet gestart worden omdat de spelers nog niet gekoppeld zijn."));

        // Assert
        Assert.AreNotEqual(Status.Playing, game.Status);
        Assert.IsFalse(game.IsPlaying());
    }

    [Test]
    public void StartGame_CannotWithEmptyPlayerOne()
    {
        // Arrange
        IGame game = new Game();

        // Act
        game.PlayerTwo = new PlayerTwo();
        var ex = Assert.Throws<Exception>(delegate { game.Start(); });
        Assert.That(ex.Message, Is.EqualTo("Game kan niet gestart worden omdat speler 1 niet gekoppeld is."));

        // Assert
        Assert.AreNotEqual(Status.Playing, game.Status);
        Assert.IsFalse(game.IsPlaying());
    }

    [Test]
    public void StartGame_CannotWithEmptyPlayerTwo()
    {
        // Arrange
        IGame game = new Game();

        // Act
        game.PlayerOne = new PlayerOne();
        var ex = Assert.Throws<Exception>(delegate { game.Start(); });
        Assert.That(ex.Message, Is.EqualTo("Game kan niet gestart worden omdat speler 2 niet gekoppeld is."));

        // Assert
        Assert.AreNotEqual(Status.Playing, game.Status);
        Assert.IsFalse(game.IsPlaying());
    }

    [Test]
    public void StartGame_CannotDoMove()
    {
        // Arrange
        IGame game = new Game();

        // Act
        var ex = Assert.Throws<Exception>(delegate { game.DoMove(2, 3); });
        Assert.That(ex.Message, Is.EqualTo("Game is nog niet gestart!"));

        // Assert
        Assert.AreNotEqual(Status.Playing, game.Status);
        Assert.IsFalse(game.IsPlaying());
    }

    [Test]
    public void StartGame_CannotCheckIfMoveIsPossible()
    {
        // Arrange
        IGame game = new Game();

        // Act
        var ex = Assert.Throws<Exception>(delegate { game.IsMovePossible(2, 3); });
        Assert.That(ex.Message, Is.EqualTo("Game is nog niet gestart!"));

        // Assert
        Assert.AreNotEqual(Status.Playing, game.Status);
        Assert.IsFalse(game.IsPlaying());
    }

    [Test]
    public void QuitGame()
    {
        // Arrange
        IGame game = new Game();

        // Act
        game.PlayerOne = new PlayerOne();
        game.PlayerTwo = new PlayerTwo();
        game.Start();
        game.Quit();

        // Assert
        Assert.AreEqual(Status.Quit, game.Status);
        Assert.IsTrue(game.IsQuit());
    }

    [Test]
    public void QuitGame_CannotIfNotStarted()
    {
        // Arrange
        IGame game = new Game();

        // Act
        game.PlayerOne = new PlayerOne();
        game.PlayerTwo = new PlayerTwo();
        var ex = Assert.Throws<Exception>(delegate { game.Quit(); });
        Assert.That(ex.Message, Is.EqualTo("De game is nog niet gestart!"));

        // Assert
        Assert.AreEqual(Status.Created, game.Status);
        Assert.IsFalse(game.IsQuit());
    }

    [Test]
    public void QuitGame_CannotRestartGame()
    {
        // Arrange
        IGame game = new Game();

        // Act
        game.PlayerOne = new PlayerOne();
        game.PlayerTwo = new PlayerTwo();
        game.Start();
        game.Quit();
        var ex = Assert.Throws<Exception>(delegate { game.Start(); });
        Assert.That(ex.Message, Is.EqualTo("Game is al een keer gestart!"));

        // Assert
        Assert.AreNotEqual(Status.Playing, game.Status);
        Assert.IsFalse(game.IsPlaying());
    }

}
