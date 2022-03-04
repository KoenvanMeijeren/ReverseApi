using System.Linq;
using NUnit.Framework;
using ReversiApi.Model.Game;
using ReversiApi.Model.Player;
using ReversiApi.Repository;
using ReversiApi.Repository.Contracts;

namespace Tests.Repository;

[TestFixture]
public class PlayersRepositoryTests
{
    private readonly IPlayersRepository _repository = new PlayersRepository();

    [Test]
    public void All()
    {
        var players = this._repository.All();
        
        Assert.AreEqual(4, players.Count());
        Assert.AreEqual(Color.White, players.First().Color);
        Assert.AreEqual("abcdef", players.First().Token);
    }
    
    [Test]
    public void Add()
    {
        // Arrange
        var repository = new PlayersRepository();
        
        // Act
        repository.Add(new PlayerEntity(new PlayerOne("qwerty")));
        var players = repository.All();
        
        // Assert
        Assert.AreEqual(5, players.Count());
        Assert.AreEqual(Color.White, players.Last().Color);
        Assert.AreEqual("qwerty", players.Last().Token);
    }
    
    [Test]
    public void FirstOrCreate()
    {
        // Arrange
        
        // Act
        var player = this._repository.FirstOrCreate(new PlayerEntity(new PlayerOne("hjikl")));
        player = this._repository.FirstOrCreate(new PlayerEntity(new PlayerOne("hjikl")));
        var players = this._repository.All();
        
        // Assert
        Assert.AreEqual(5, players.Count());
        Assert.AreEqual(Color.White, player.Color);
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