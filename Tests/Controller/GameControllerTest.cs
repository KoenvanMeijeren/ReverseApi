#nullable enable
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
    public void NotEmpty_GetGamesInQueue()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetGamesInQueue();
        var json = response.ToJson();
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
        Assert.IsTrue(json.Contains("Result"));
        Assert.IsTrue(json.Contains("Status"));
        Assert.IsTrue(json.Contains("PlayerOne"));
        Assert.IsTrue(json.Contains("PlayerTwo"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
    }
    
    [Test]
    public void Empty_GetOfGamesInQueue()
    {
        // Arrange
        var repository = new GamesRepositoryEmptyTest();
        var controller = new GameController(repository);

        // Act
        var response = controller.GetGamesInQueue();
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
    }

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
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
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
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
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
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
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
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<NotFoundResult>(response1.Result);
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
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
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
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<NotFoundResult>(response1.Result);
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
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
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
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<NotFoundResult>(response1.Result);
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
        Assert.IsInstanceOf<CreatedAtRouteResult>(response);
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
        Assert.IsInstanceOf<BadRequestResult>(response);
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
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
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
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<NotFoundResult>(response1.Result);
    }
    
    [Test]
    public void AddPlayerOneToGame_Valid()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);
        var game = new Game();
        var dto = new GameAddPlayerDto()
        {
            Token = game.Token,
            PlayerToken = "abcdef"
        };

        // Act
        repository.Add(game);
        var response = controller.AddPlayerOneToGame(dto);
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
    }
    
    [Test]
    public void AddPlayerOneToGame_Invalid()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);
        var dto = new GameAddPlayerDto()
        {
            Token = "testfda",
            PlayerToken = "abcdeffdasf"
        };
        
        // Act
        var response = controller.AddPlayerOneToGame(null);
        var response1 = controller.AddPlayerOneToGame(dto);
        
        // Assert
        Assert.IsInstanceOf<BadRequestResult>(response.Result);
        Assert.IsInstanceOf<NotFoundResult>(response1.Result);
    }
    
    [Test]
    public void AddPlayerTwoToGame_Valid()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);
        var game = new Game();
        var dto = new GameAddPlayerDto()
        {
            Token = game.Token,
            PlayerToken = "abcdef"
        };

        // Act
        repository.Add(game);
        var response = controller.AddPlayerTwoToGame(dto);
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
    }
    
    [Test]
    public void AddPlayerTwoToGame_Invalid()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);
        var dto = new GameAddPlayerDto()
        {
            Token = "testfda",
            PlayerToken = "abcdeffdasf"
        };
        
        // Act
        var response = controller.AddPlayerTwoToGame(null);
        var response1 = controller.AddPlayerTwoToGame(dto);
        
        // Assert
        Assert.IsInstanceOf<BadRequestResult>(response.Result);
        Assert.IsInstanceOf<NotFoundResult>(response1.Result);
    }
    
    [Test]
    public void Can_StartGame()
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
        
        var response = controller.StartGame(game.Token);
        var json = response.ToJson();
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
        Assert.IsTrue(json.Contains(Status.Playing.ToString()));
    }
    
    [Test]
    public void Cannot_StartGame()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act 
        var response = controller.StartGame("test");
        var response1 = controller.StartGame(null);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<NotFoundResult>(response1.Result);
    }

    [Test]
    public void Can_DoMoveInGame()
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

        var response = controller.DoMoveGame(new GameDoMoveDto()
        {
            Token = game.Token,
            PlayerToken = "",
            Row = 3, 
            Column = 5
        });
        var json = response.ToJson();
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
        Assert.IsTrue(json.Contains(Status.Playing.ToString()));
    }
    
    [Test]
    public void CannotWithInvalidPlayer_DoMoveInGame()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);
        var game = new Game
        {
            PlayerOne = new PlayerOne("qweruty"),
            PlayerTwo = new PlayerTwo()
        };

        // Act 
        repository.Add(game);
        game.Start();

        var response = controller.DoMoveGame(new GameDoMoveDto()
        {
            Token = game.Token,
            PlayerToken = "test",
            Row = 3, 
            Column = 5
        });
        
        // Assert
        Assert.IsInstanceOf<BadRequestResult>(response.Result);
    }
    
    [Test]
    public void Cannot_DoMoveInGame()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act 
        var response = controller.DoMoveGame(new GameDoMoveDto()
        {
            Token = "test"
        });
        var response1 = controller.DoMoveGame(null);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<BadRequestResult>(response1.Result);
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
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
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
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<NotFoundResult>(response1.Result);
    }
    
    [Test]
    public void Can_CheckForFinishedGame()
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
        
        var response = controller.IsFinishedGame(game.Token);
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
    }
    
    [Test]
    public void Cannot_CheckForFinishedGame()
    {
        // Arrange
        var repository = new GamesRepositoryTest();
        var controller = new GameController(repository);

        // Act 
        var response = controller.IsFinishedGame("test");
        var response1 = controller.IsFinishedGame(null);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<NotFoundResult>(response1.Result);
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
        return this.All().Where(game => game.IsQueued());
    }
    
    /// <inheritdoc />
    public bool Exists(string? token)
    {
        return token != null && this.Get(token) != null;
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
    public bool Exists(string? token)
    {
        return token != null && this.Get(token) != null;
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
