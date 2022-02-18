using System.Linq;
using NUnit.Framework;
using ReversiApi.Model;
using ReversiApi.Repository;

namespace Tests.Repository;

[TestFixture]
public class GamesRepositoryTests
{

    private readonly IGamesRepository _repository = new GamesRepository();

    [Test]
    public void AllGames()
    {
        var games = this._repository.All();
        
        Assert.AreEqual(3, games.Count);
        Assert.AreEqual("abcdef", games.First().TokenPlayerOne);
    }
    
    [Test]
    public void AllGamesInQueue()
    {
        var games = this._repository.AllInQueue().ToList();
        
        Assert.AreEqual(2, games.Count);
        Assert.AreEqual("abcdef", games.First().TokenPlayerOne);
    }
    
    [Test]
    public void GetOneGame()
    {
        var token = this._repository.All().First().Token;
        var game = this._repository.Get(token);
        
        Assert.IsNotNull(game);
        Assert.AreEqual(token, game.Token);
        Assert.AreEqual("Potje snel reveri, dus niet lang nadenken", game.Description);
        Assert.AreEqual("abcdef", game.TokenPlayerOne);
        Assert.IsNull( game.TokenPlayerTwo);
    }
    
    [Test]
    public void CannotGetGameForNonExistingToken()
    {
        Assert.IsNull(this._repository.Get(null));
        Assert.IsNull(this._repository.Get("test"));
        Assert.IsNull(this._repository.Get("1"));
    }

    [Test]
    public void AddGame()
    {
        var repository = new GamesRepository();
        var game1 = new Game();
        var game2 = new Game();
        
        game1.TokenPlayerOne = "fdask";
        game1.Description = "Potje snel reveri, dus niet lang nadenken";
        game2.TokenPlayerOne = "qwert";
        game2.TokenPlayerTwo = "fdask";
        game2.Description = "Ik zoek een gevorderde tegenspeler!";
        
        repository.Add(game1);
        repository.Add(game2);
        
        Assert.Contains(game1, repository.All());
        Assert.Contains(game2, repository.All());
        Assert.AreEqual(game1, repository.Get(game1.Token));
        Assert.AreEqual(game2, repository.Get(game2.Token));
    }

}