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
using ReversiApi.Repository.Contracts;

namespace Tests.Controller;

[TestFixture]
public class GameControllerTest
{
    
    [Test]
    public void NotEmpty_GetGamesInQueue()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

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
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

        // Act
        var response = controller.GetGamesInQueue();
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
    }

    [Test]
    public void NotEmpty_GetGameByToken()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

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
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

        // Act
        var response = controller.GetByToken("test");
        var response1 = controller.GetByToken(null);
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<BadRequestResult>(response1.Result);
    }

    [Test]
    public void NotEmpty_GetGameByPlayerOneToken()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

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
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

        // Act
        var response = controller.GetByPlayerOneToken("test");
        var response1 = controller.GetByPlayerOneToken(null);
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<BadRequestResult>(response1.Result);
    }
    
    [Test]
    public void NotEmpty_GetGameByPlayerTwoToken()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

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
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

        // Act
        var response = controller.GetByPlayerTwoToken("test");
        var response1 = controller.GetByPlayerTwoToken(null);
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<BadRequestResult>(response1.Result);
    }
    
    [Test]
    public void PostGame_Valid()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);
        
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
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

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
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

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
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

        // Act
        var response = controller.GetGameStatus("test");
        var response1 = controller.GetGameStatus(null);
        
        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<BadRequestResult>(response1.Result);
    }
    
    [Test]
    public void AddPlayerOneToGame_Valid()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);
        
        var entity = new GameEntity();
        var dto = new GameAddPlayerDto()
        {
            Token = entity.Token,
            PlayerToken = "abcdef"
        };

        // Act
        repository.Add(entity);
        var response = controller.AddPlayerOneToGame(dto);
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
    }
    
    [Test]
    public void AddPlayerOneToGame_Invalid()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);
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
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);
        var entity = new GameEntity();
        var dto = new GameAddPlayerDto()
        {
            Token = entity.Token,
            PlayerToken = "abcdef"
        };

        // Act
        repository.Add(entity);
        var response = controller.AddPlayerTwoToGame(dto);
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
    }
    
    [Test]
    public void AddPlayerTwoToGame_Invalid()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);
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
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);
        var entity = new GameEntity()
        {
            PlayerOne = new PlayerEntity(new PlayerOne()),
            PlayerTwo = new PlayerEntity(new PlayerTwo())
        };

        // Act 
        repository.Add(entity);
        
        var response = controller.StartGame(entity.Token);
        var json = response.ToJson();
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
        Assert.IsTrue(json.Contains(Status.Playing.ToString()));
    }
    
    [Test]
    public void Cannot_StartGame()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

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
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);
        var entity = new GameEntity()
        {
            PlayerOne = new PlayerEntity(new PlayerOne()),
            PlayerTwo = new PlayerEntity(new PlayerTwo())
        };

        // Act 
        repository.Add(entity);
        entity.Game.Start();
        entity.UpdateEntity();

        var response = controller.DoMoveGame(new GameDoMoveDto()
        {
            Token = entity.Token,
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
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);
        var entity = new GameEntity()
        {
            PlayerOne = new PlayerEntity(new PlayerOne("qweruty")),
            PlayerTwo = new PlayerEntity(new PlayerTwo())
        };

        // Act 
        repository.Add(entity);
        entity.Game.Start();
        entity.UpdateEntity();
        
        var response = controller.DoMoveGame(new GameDoMoveDto()
        {
            Token = entity.Token,
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
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

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
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);
        var entity = new GameEntity()
        {
            PlayerOne = new PlayerEntity(new PlayerOne()),
            PlayerTwo = new PlayerEntity(new PlayerTwo())
        };

        // Act 
        repository.Add(entity);
        entity.Game.Start();
        entity.UpdateEntity();
        
        var response = controller.QuitGame(entity.Token);
        var json = response.ToJson();
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
        Assert.IsTrue(json.Contains(Status.Quit.ToString()));
    }
    
    [Test]
    public void Cannot_QuitGame()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

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
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);
        var entity = new GameEntity()
        {
            PlayerOne = new PlayerEntity(new PlayerOne()),
            PlayerTwo = new PlayerEntity(new PlayerTwo())
        };

        // Act 
        repository.Add(entity);
        entity.Game.Start();
        entity.UpdateEntity();
        
        var response = controller.IsFinishedGame(entity.Token);
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
    }
    
    [Test]
    public void Cannot_CheckForFinishedGame()
    {
        // Arrange
        var repository = new GamesRepository();
        var playerRepository = new PlayersRepository();
        var controller = new GameController(repository, playerRepository);

        // Act 
        var response = controller.IsFinishedGame("test");
        var response1 = controller.IsFinishedGame(null);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(response.Result);
        Assert.IsInstanceOf<NotFoundResult>(response1.Result);
    }
}

internal class GamesRepositoryEmptyTest :  RepositoryBase<GameEntity>, IGamesRepository<GameEntity>
{
    
    /// <inheritdoc />
    public override void Add(GameEntity entity)
    {
        entity.UpdateGame();
        
        base.Add(entity);
    }

    /// <inheritdoc />
    public IEnumerable<GameEntity> AllInQueue()
    {
        return this.All().Where(entity => entity.Game.IsQueued());
    }

    /// <inheritdoc />
    public bool Exists(string? token)
    {
        return token != null && this.Get(token) != null;
    }

    /// <inheritdoc />
    public GameEntity? Get(string? token)
    {
        return this.Items.SingleOrDefault(entity => entity.Token.Equals(token));
    }
    
    /// <inheritdoc />
    public GameEntity? GetByPlayerOne(string? token)
    {
        return this.Items.SingleOrDefault(entity => entity.PlayerOne != null && entity.PlayerOne.Token.Equals(token));
    }
    
    /// <inheritdoc />
    public GameEntity? GetByPlayerTwo(string? token)
    {
        return this.Items.SingleOrDefault(entity => entity.PlayerTwo != null && entity.PlayerTwo.Token.Equals(token));
    }
    
}
