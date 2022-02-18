using System.Collections.Generic;
using System.Linq;
using NuGet.Protocol;
using NUnit.Framework;
using ReverseApi.Controllers;
using ReverseApi.Model;
using ReverseApi.Model.DataTransferObject.Game;
using ReverseApi.Repository;

namespace Tests.Controller;

[TestFixture]
public class GameControllerTest
{

    [Test]
    public void NotEmpty_GetDescriptionsOfGameInQueue()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetDescriptionsOfGameInQueue();
        var json = response.ToJson();
        
        // Assert
        Assert.IsNotEmpty(json);
        Assert.IsTrue(json.Contains("200"));
        Assert.IsTrue(json.Contains("Result"));
        Assert.IsTrue(json.Contains("Value"));
        Assert.IsTrue(json.Contains("Potje snel reveri"));
    }
    
    [Test]
    public void Empty_GetDescriptionsOfGameInQueue()
    {
        // Arrange
        var repository = new GamesRepositoryEmptyTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetDescriptionsOfGameInQueue();
        var json = response.ToJson();
        
        // Assert
        Assert.IsNotEmpty(json);
        Assert.IsTrue(json.Contains("404"));
        Assert.IsTrue(json.Contains("Result"));
        Assert.IsFalse(json.Contains("Value"));
        Assert.IsFalse(json.Contains("Potje snel reveri"));
    }
    
    [Test]
    public void NotEmpty_GetGameByToken()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetByToken(repository.All().First().Token);
        var json = response.ToJson();
        
        // Assert
        Assert.IsNotEmpty(json);
        Assert.IsTrue(json.Contains("200"));
        Assert.IsTrue(json.Contains("Result"));
        Assert.IsTrue(json.Contains("Value"));
        Assert.IsTrue(json.Contains("Potje snel reveri"));
    }
    
    [Test]
    public void Empty_GetGameByToken()
    {
        // Arrange
        var repository = new GamesRepositoryEmptyTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetByToken("test");
        var json = response.ToJson();
        
        // Assert
        Assert.IsNotEmpty(json);
        Assert.IsTrue(json.Contains("404"));
        Assert.IsTrue(json.Contains("Result"));
        Assert.IsFalse(json.Contains("Value"));
        Assert.IsFalse(json.Contains("Potje snel reveri"));
    }

    [Test]
    public void PostGame_Valid()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);
        var dto = new GameCreateDto
        {
            Description = "test",
            TokenPlayerOne = "abcdef"
        };

        // Act
        var response = controller.PostGame(dto);
        var json = response.ToJson();
        
        // Assert
        Assert.IsNotEmpty(json);
        Assert.IsTrue(json.Contains("201"));
        Assert.IsTrue(json.Contains("Value"));
        Assert.IsTrue(json.Contains("RouteName"));
        Assert.IsTrue(json.Contains("getGameByTokenRoute"));
        Assert.IsTrue(json.Contains("RouteValues"));
        Assert.IsTrue(json.Contains("token"));
        Assert.IsTrue(json.Contains("Id"));
        Assert.IsTrue(json.Contains("Description"));
        Assert.IsTrue(json.Contains("Token"));
        Assert.IsTrue(json.Contains("TokenPlayerOne"));
    }
    
    [Test]
    public void PostGame_Invalid()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.PostGame(null);
        var json = response.ToJson();
        
        // Assert
        Assert.IsNotEmpty(json);
        Assert.IsTrue(json.Contains("400"));
        Assert.IsFalse(json.Contains("Value"));
        Assert.IsFalse(json.Contains("RouteName"));
        Assert.IsFalse(json.Contains("getGameByTokenRoute"));
        Assert.IsFalse(json.Contains("RouteValues"));
        Assert.IsFalse(json.Contains("token"));
        Assert.IsFalse(json.Contains("Id"));
        Assert.IsFalse(json.Contains("Description"));
        Assert.IsFalse(json.Contains("Token"));
        Assert.IsFalse(json.Contains("TokenPlayerOne"));
    }
    
}

class GamesRepositoryTest : IGamesRepository
{
    private List<IGame> _games;

    public GamesRepositoryTest()
    {
        IGame game1 = new Game();
        IGame game2 = new Game();
        IGame game3 = new Game();

        game1.TokenPlayerOne = "abcdef";
        game1.Description = "Potje snel reveri, dus niet lang nadenken";
        game2.TokenPlayerOne = "ghijkl";
        game2.TokenPlayerTwo = "mnopqr";
        game2.Description = "Ik zoek een gevorderde tegenspeler!";
        game3.TokenPlayerOne = "stuvwx";
        game3.Description = "Na dit spel wil ik er nog een paar spelen tegen zelfde tegenstander";

        this._games = new List<IGame> {game1, game2, game3};
    }
    
    /// <inheritdoc />
    public void Add(IGame game)
    {
        this._games.Add(game);
    }

    /// <inheritdoc />
    public List<IGame> All()
    {
        return this._games;
    }

    /// <inheritdoc />
    public IEnumerable<IGame> AllInQueue()
    {
        return this.All().Where(game => game.TokenPlayerTwo == null);
    }

    /// <inheritdoc />
    public IGame Get(string token)
    {
        return this._games.Find(game => game.Token.Equals(token));
    }
}

class GamesRepositoryEmptyTest : IGamesRepository
{
    private List<IGame> _games = new List<IGame>();

    /// <inheritdoc />
    public void Add(IGame game)
    {
        this._games.Add(game);
    }

    /// <inheritdoc />
    public List<IGame> All()
    {
        return this._games;
    }

    /// <inheritdoc />
    public IEnumerable<IGame> AllInQueue()
    {
        return this.All().Where(game => game.TokenPlayerTwo == null);
    }

    /// <inheritdoc />
    public IGame Get(string token)
    {
        return this._games.Find(game => game.Token.Equals(token));
    }
}
