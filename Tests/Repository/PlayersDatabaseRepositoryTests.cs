using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReversiApi.DataAccess;
using ReversiApi.Model.Player;
using ReversiApi.Repository;
using ReversiApi.Repository.Contracts;

namespace Tests.Repository;

[TestFixture]
public class PlayersDatabaseRepositoryTests
{
    private readonly IPlayersDatabaseRepository _repository;

    public PlayersDatabaseRepositoryTests()
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

        context.Players.AddRange((new PlayersRepository()).All());
        context.SaveChanges();
            
        this._repository = new PlayersDatabaseRepository(context);
    }
    
    [Test]
    public void All()
    {
        var players = this._repository.All();
        
        Assert.AreEqual(5, players.Count());
        Assert.AreEqual("abcdef", players.First().Token);
    }
    
    [Test]
    public void Add()
    {
        // Arrange
        
        // Act
        this._repository.Add(new PlayerEntity(token: "qwerty"));
        var players = this._repository.All();
        
        // Assert
        Assert.AreEqual(5, players.Count());
        Assert.AreEqual("qwerty", players.Last().Token);
    }
    
    [Test]
    public void FirstOrCreate()
    {
        // Arrange
        Assert.AreEqual(5, this._repository.All().Count());
        
        // Act
        var player = this._repository.FirstOrCreate(new PlayerEntity(token: "hjikl"));
        player = this._repository.FirstOrCreate(new PlayerEntity(token: "hjikl"));
        var players = this._repository.All();
        
        // Assert
        Assert.AreEqual(6, players.Count());
        Assert.AreEqual("hjikl", player.Token);
    }
    
    [Test]
    public void Exists_True()
    {
        // Arrange
        
        // Act

        // Assert
        Assert.IsTrue(this._repository.Exists("abcdef"));
        Assert.IsTrue(this._repository.Exists("ghijkl"));
    }
    
    [Test]
    public void Exists_False()
    {
        // Arrange
        
        // Act

        // Assert
        Assert.IsFalse(this._repository.Exists("fdafd"));
        Assert.IsFalse(this._repository.Exists(null));
    }
}