﻿using System.Linq;
using NUnit.Framework;
using ReversiApi.Model.Game;
using ReversiApi.Model.Player;
using ReversiApi.Repository;
using ReversiApi.Repository.Contracts;

namespace Tests.Repository;

[TestFixture]
public class GamesRepositoryTests
{

    private readonly IGamesRepository _repository = new GamesRepository();

    [Test]
    public void AllGames()
    {
        var games = this._repository.All();
        
        Assert.AreEqual(3, games.Count());
        Assert.AreEqual("abcdef", games.First().PlayerOne.Token);
    }
    
    [Test]
    public void AllGamesInQueue()
    {
        var games = this._repository.AllInQueue().ToList();
        
        Assert.AreEqual(2, games.Count);
        Assert.AreEqual("abcdef", games.First().PlayerOne.Token);
    }
    
    [Test]
    public void GetOneGame()
    {
        var token = this._repository.All().First().Token;
        var game = this._repository.Get(token);
        
        Assert.IsNotNull(game);
        Assert.AreEqual(token, game.Token);
        Assert.AreEqual("Potje snel reveri, dus niet lang nadenken", game.Description);
        Assert.AreEqual("abcdef", game.PlayerOne.Token);
        Assert.IsNull( game.PlayerTwo);
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
        Assert.IsNull( game.PlayerTwo?.Token);
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
        Assert.IsFalse(this._repository.Exists(null));
        Assert.IsFalse(this._repository.Exists("abcdef"));
    }
    
    [Test]
    public void AddGame()
    {
        var repository = new GamesRepository();
        var game1 = new GameEntity();
        var game2 = new GameEntity();
        
        game1.PlayerOne = new PlayerEntity(new PlayerOne("fdask"));
        game1.Description = "Potje snel reveri, dus niet lang nadenken";
        game2.PlayerOne =  new PlayerEntity(new PlayerOne("qwert"));
        game2.PlayerTwo = new PlayerEntity(new PlayerTwo("fdask"));
        game2.Description = "Ik zoek een gevorderde tegenspeler!";
        
        repository.Add(game1);
        repository.Add(game2);
        
        Assert.Contains(game1, repository.All().ToList());
        Assert.Contains(game2, repository.All().ToList());
        Assert.AreEqual(game1, repository.Get(game1.Token));
        Assert.AreEqual(game2, repository.Get(game2.Token));
    }
    
    [Test]
    public void UpdateGame()
    {
        // Arrange
        var entity = this._repository.All().First();
        var game1 = new GameEntity();
        
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
        var repository = new GamesRepository();
        var entity = this._repository.All().First();
        repository.Add(entity);
        repository.Add(new GameEntity());
        
        // Act
        entity.Game.Status = Status.Finished;
        repository.Update(entity);
        var successful = repository.Delete(entity);

        // Assert
        Assert.IsTrue(successful);
        Assert.AreEqual(Status.Created, repository.All().First().Status);
    }

}