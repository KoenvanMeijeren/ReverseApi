#nullable enable
using System.Collections.Generic;
using System.Linq;
using NuGet.Protocol;
using NUnit.Framework;
using ReversiApi.Controllers;
using ReversiApi.Model.Game;
using ReversiApi.Model.Game.DataTransferObject;
using ReversiApi.Model.Player;
using ReversiApi.Repository;

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
        var response1 = controller.GetByToken(null);
        var json = response.ToJson();
        var json1 = response1.ToJson();
        
        // Assert
        Assert.IsTrue(json.Contains("404"));
        Assert.IsTrue(json1.Contains("404"));
        Assert.IsTrue(json.Contains("Result"));
        Assert.IsFalse(json.Contains("Value"));
        Assert.IsFalse(json.Contains("Potje snel reveri"));
    }

    [Test]
    public void NotEmpty_GetGameByPlayerOneToken()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetByPlayerOneToken("abcdef");
        var json = response.ToJson();
        
        // Assert
        Assert.IsNotEmpty(json);
        Assert.IsTrue(json.Contains("200"));
        Assert.IsTrue(json.Contains("Result"));
        Assert.IsTrue(json.Contains("Value"));
        Assert.IsTrue(json.Contains("Potje snel reveri"));
    }
    
    [Test]
    public void Empty_GetGameByPlayerOneToken()
    {
        // Arrange
        var repository = new GamesRepositoryEmptyTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetByPlayerOneToken("test");
        var response1 = controller.GetByPlayerOneToken(null);
        var json = response.ToJson();
        var json1 = response1.ToJson();
        
        // Assert
        Assert.IsTrue(json.Contains("404"));
        Assert.IsTrue(json1.Contains("404"));
    }
    
    [Test]
    public void NotEmpty_GetGameByPlayerTwoToken()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetByPlayerTwoToken("mnopqr");
        var json = response.ToJson();
        
        // Assert
        Assert.IsNotEmpty(json);
        Assert.IsTrue(json.Contains("200"));
        Assert.IsTrue(json.Contains("Result"));
        Assert.IsTrue(json.Contains("Value"));
        Assert.IsTrue(json.Contains("Ik zoek een"));
    }
    
    [Test]
    public void Empty_GetGameByPlayerTwoToken()
    {
        // Arrange
        var repository = new GamesRepositoryEmptyTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetByPlayerTwoToken("test");
        var response1 = controller.GetByPlayerTwoToken(null);
        var json = response.ToJson();
        var json1 = response1.ToJson();
        
        // Assert
        Assert.IsTrue(json.Contains("404"));
        Assert.IsTrue(json1.Contains("404"));
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
        var response = controller.CreateGame(dto);
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
        Assert.IsTrue(json.Contains("PlayerOne"));
    }
    
    [Test]
    public void PostGame_Invalid()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.CreateGame(null);
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
    
    [Test]
    public void NotEmpty_GetGameStatus()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetGameStatus(repository.All().First().Token);
        var json = response.ToJson();
        
        // Assert
        Assert.IsNotEmpty(json);
        Assert.IsTrue(json.Contains("200"));
        Assert.IsTrue(json.Contains("Result"));
        Assert.IsTrue(json.Contains("Value"));
        Assert.IsTrue(json.Contains("Board"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Status"));
    }
    
    [Test]
    public void Empty_GetGameStatus() 
    {
        // Arrange
        var repository = new GamesRepositoryEmptyTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetGameStatus("test");
        var response1 = controller.GetGameStatus(null);
        var json = response.ToJson();
        var json1 = response1.ToJson();
        
        // Assert
        Assert.IsTrue(json.Contains("404"));
        Assert.IsTrue(json1.Contains("404"));
    }
    
    [Test]
    public void Can_QuitGame()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);
        var game = new Game
        {
            PlayerOne = new PlayerOne(),
            PlayerTwo = new PlayerTwo()
        };

        // Act 
        repository.Add(game);
        game.Start();
        
        var response = controller.QuitGame(game.Token);
        var json = response.ToJson();
        
        // Assert
        Assert.IsTrue(json.Contains("200"));
        Assert.IsTrue(json.Contains(Status.Quit.ToString()));
    }
    
    [Test]
    public void Cannot_QuitGame()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act 
        var response = controller.QuitGame("test");
        var response1 = controller.QuitGame(null);
        var json = response.ToJson();
        var json1 = response1.ToJson();
        
        // Assert
        Assert.IsTrue(json.Contains("404"));
        Assert.IsTrue(json1.Contains("404"));
        Assert.IsFalse(json.Contains(Status.Quit.ToString()));
        Assert.IsFalse(json1.Contains(Status.Quit.ToString()));
    }
}

internal class GamesRepositoryTest : IGamesRepository
{
    private readonly List<IGame> _games;

    public GamesRepositoryTest()
    {
        IGame game1 = new Game();
        IGame game2 = new Game();
        IGame game3 = new Game();

        game1.PlayerOne = new PlayerOne("abcdef");
        game1.Description = "Potje snel reveri, dus niet lang nadenken";
        game2.PlayerOne = new PlayerOne("ghijkl");
        game2.PlayerTwo = new PlayerTwo("mnopqr");
        game2.Description = "Ik zoek een gevorderde tegenspeler!";
        game3.PlayerOne = new PlayerOne("stuvwx");
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
        return this.All().Where(game => game.PlayerTwo?.Token == null);
    }

    /// <inheritdoc />
    public IGame? Get(string? token)
    {
        return this._games.Find(game => game.IsQueued());
    }
    
    /// <inheritdoc />
    public IGame? GetByPlayerOne(string? token)
    {
        return this._games.Find(game => game.PlayerOne != null && game.PlayerOne.Token.Equals(token));
    }
    
    /// <inheritdoc />
    public IGame? GetByPlayerTwo(string? token)
    {
        return this._games.Find(game => game.PlayerTwo != null && game.PlayerTwo.Token.Equals(token));
    }
}

internal class GamesRepositoryEmptyTest : IGamesRepository
{
    private readonly List<IGame> _games = new List<IGame>();

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
        return this.All().Where(game => game.IsQueued());
    }

    /// <inheritdoc />
    public IGame? Get(string? token)
    {
        return this._games.Find(game => game.Token.Equals(token));
    }
    
    /// <inheritdoc />
    public IGame? GetByPlayerOne(string? token)
    {
        return this._games.Find(game => game.PlayerOne != null && game.PlayerOne.Token.Equals(token));
    }
    
    /// <inheritdoc />
    public IGame? GetByPlayerTwo(string? token)
    {
        return this._games.Find(game => game.PlayerTwo != null && game.PlayerTwo.Token.Equals(token));
    }
    
}
