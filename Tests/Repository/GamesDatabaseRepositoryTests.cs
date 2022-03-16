using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReversiApi.DataAccess;
using ReversiApi.Model.Game;
using ReversiApi.Model.Player;
using ReversiApi.Repository;
using ReversiApi.Repository.Contracts;

namespace Tests.Repository;

[TestFixture]
public class GamesDatabaseRepositoryTests
{
    private readonly IDatabaseGamesRepository _repository;

    public GamesDatabaseRepositoryTests()
    {
        // Create and open a connection. This creates the SQLite in-memory database, which will persist until the
        // connection is closed at the end of the test (see Dispose below).
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        // These options will be used by the context instances in this test suite, including the connection opened
        // above.
        var contextOptions = new DbContextOptionsBuilder<GamesDataAccess>()
            .UseSqlite(connection)
            .Options;

        // Create the schema and seed some data
        var context = new GamesDataAccess(contextOptions);

        context.Database.EnsureCreated();

        context.Games.AddRange((new GamesRepository()).All());
        context.SaveChanges();

        this._repository = new GamesDatabaseRepository(context);
    }

    [Test]
    public void AllGames()
    {
        var games = this._repository.All();

        Assert.AreEqual(10, games.Count());
        Assert.AreEqual("abcdef", games.First().PlayerOne.Token);
    }

    [Test]
    public void AllGamesInQueue()
    {
        var games = this._repository.AllByStatus("queued");

        Assert.AreEqual(8, games.Count());
        Assert.AreEqual("abcdef", games.First().PlayerOne.Token);
    }

    [Test]
    public void AllGamesByStatus()
    {
        Assert.AreEqual(10, this._repository.AllByStatus(null).Count());
        Assert.AreEqual(10, this._repository.AllByStatus("test").Count());
        Assert.AreEqual(10, this._repository.AllByStatus("all").Count());
        Assert.AreEqual(6, this._repository.AllByStatus("created").Count());
        Assert.AreEqual(8, this._repository.AllByStatus("queued").Count());
        Assert.AreEqual(1, this._repository.AllByStatus("pending").Count());
        Assert.AreEqual(1, this._repository.AllByStatus("playing").Count());
        Assert.AreEqual(9, this._repository.AllByStatus("active").Count());
        Assert.AreEqual(1, this._repository.AllByStatus("quit").Count());
        Assert.AreEqual(0, this._repository.AllByStatus("finished").Count());
    }

    [Test]
    public void GetOneGame_ByToken()
    {
        var token = this._repository.All().First().Token;
        var game = this._repository.Get(token);

        Assert.IsNotNull(game);
        Assert.AreEqual(token, game.Token);
        Assert.AreEqual("Potje snel reveri, dus niet lang nadenken", game.Description);
        Assert.AreEqual("abcdef", game.PlayerOne.Token);
        Assert.IsNull(game.PlayerTwo);
    }

    [Test]
    public void GetOneGame_ById()
    {
        var entity = this._repository.Get(2);

        Assert.IsInstanceOf<GameEntity>(entity);
        Assert.AreEqual("Ik zoek een gevorderde tegenspeler!", entity.Description);
        Assert.AreEqual("ghijkl", entity.PlayerOne.Token);
        Assert.AreEqual("mnopqr", entity.PlayerTwo.Token);
    }

    [Test]
    public void CannotGetGameForNonExistingToken()
    {
        Assert.IsNull(this._repository.Get(null));
        Assert.IsNull(this._repository.Get("test"));
        Assert.IsNull(this._repository.Get("1"));
    }

    [Test]
    public void GetOneGameByPlayerOne()
    {
        var token = this._repository.All().First().PlayerOne.Token;
        var game = this._repository.GetByPlayerOne(token);

        Assert.IsNotNull(game);
        Assert.AreEqual("Potje snel reveri, dus niet lang nadenken", game.Description);
        Assert.AreEqual(token, game.PlayerOne.Token);
        Assert.IsNull(game.PlayerTwo?.Token);
    }

    [Test]
    public void CannotGetGameForNonExistingPlayerOneToken()
    {
        Assert.IsNull(this._repository.GetByPlayerOne(null));
        Assert.IsNull(this._repository.GetByPlayerOne("test"));
        Assert.IsNull(this._repository.GetByPlayerOne("1"));
    }

    [Test]
    public void GetOneGameByPlayerTwo()
    {
        var game = this._repository.GetByPlayerTwo("mnopqr");

        Assert.IsNotNull(game);
        Assert.AreEqual("Ik zoek een gevorderde tegenspeler!", game.Description);
        Assert.AreEqual("ghijkl", game.PlayerOne.Token);
        Assert.AreEqual("mnopqr", game.PlayerTwo.Token);
    }

    [Test]
    public void CannotGetGameForNonExistingPlayerTwoToken()
    {
        Assert.IsNull(this._repository.GetByPlayerTwo(null));
        Assert.IsNull(this._repository.GetByPlayerTwo("test"));
        Assert.IsNull(this._repository.GetByPlayerTwo("1"));
    }

    [Test]
    public void ExistsGame()
    {
        var token = this._repository.All().First().Token;

        Assert.IsTrue(this._repository.Exists(token));
        Assert.IsTrue(this._repository.Exists(1));
        Assert.IsFalse(this._repository.Exists(null));
        Assert.IsFalse(this._repository.Exists("abcdef"));
    }

    [Test]
    public void AddGame()
    {
        var game1 = new GameEntity();
        var game2 = new GameEntity();

        game1.PlayerOne = new PlayerEntity(token: "fdask");
        game1.Description = "Potje snel reveri, dus niet lang nadenken";
        game2.PlayerOne = new PlayerEntity(token: "qwert");
        game2.PlayerTwo = new PlayerEntity(token: "fdask");
        game2.Description = "Ik zoek een gevorderde tegenspeler!";

        this._repository.Add(game1);
        this._repository.Add(game2);

        Assert.Contains(game1, this._repository.All().ToList());
        Assert.Contains(game2, this._repository.All().ToList());
        Assert.AreEqual(game1, this._repository.Get(game1.Token));
        Assert.AreEqual(game2, this._repository.Get(game2.Token));
    }

    [Test]
    public void UpdateGame()
    {
        // Arrange
        var entity = this._repository.All().First();

        // Act
        entity.Game.Status = Status.Finished;
        var successful = this._repository.Update(entity);

        // Assert
        Assert.IsTrue(successful);
        Assert.AreEqual(Status.Finished, this._repository.All().First().Status);
    }

    [Test]
    public void DeleteGame()
    {
        // Arrange
        var entity = this._repository.All().First();
        entity.Id = 34;

        this._repository.Add(entity);
        this._repository.Add(new GameEntity());

        // Act
        entity.Game.Status = Status.Finished;
        this._repository.Update(entity);
        var successful = this._repository.Delete(entity);

        // Assert
        Assert.IsTrue(successful);
        Assert.AreEqual(Status.Created, this._repository.All().First().Status);
    }

    [Test]
    public void Game_PlayerDoesPlayAGame()
    {
        Assert.IsFalse(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "abcdef")));
        Assert.IsFalse(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "ghijkl")));
        Assert.IsFalse(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "mnopqr")));
        Assert.IsFalse(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "stuvwx")));
        Assert.IsFalse(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "abcdef")));
        Assert.IsFalse(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "ghijkl")));
        Assert.IsFalse(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "mnopqr")));
        Assert.IsFalse(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "stuvwx")));
    }

    [Test]
    public void Game_PlayerDoesNotPlayAGame()
    {
        Assert.IsTrue(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "fdadfa")));
        Assert.IsTrue(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "czcz")));
        Assert.IsTrue(this._repository.DoesNotPlayAGame(new PlayerEntity(token: "fda")));
        Assert.IsTrue(this._repository.DoesNotPlayAGame(new PlayerEntity()));

        Assert.IsTrue(this._repository.DoesNotPlayAGame(null));
    }

}
