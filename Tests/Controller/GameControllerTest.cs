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
        Assert.IsTrue(json.Contains("Status"));
        Assert.IsTrue(json.Contains("Potje snel reveri, dus niet lang nadenken"));
        Assert.IsTrue(json.Contains("PlayerOne"));
        Assert.IsTrue(json.Contains("abcdef"));
        Assert.IsTrue(json.Contains("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]"));
        Assert.IsTrue(json.Contains("PlayerTwo"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Created"));
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
        Assert.IsTrue(json.Contains("Status"));
        Assert.IsTrue(json.Contains("Potje snel reveri, dus niet lang nadenken"));
        Assert.IsTrue(json.Contains("PlayerOne"));
        Assert.IsTrue(json.Contains("abcdef"));
        Assert.IsTrue(json.Contains("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]"));
        Assert.IsTrue(json.Contains("PlayerTwo"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Created"));
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
        Assert.IsTrue(json.Contains("Status"));
        Assert.IsTrue(json.Contains("Potje snel reveri, dus niet lang nadenken"));
        Assert.IsTrue(json.Contains("PlayerOne"));
        Assert.IsTrue(json.Contains("abcdef"));
        Assert.IsTrue(json.Contains("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]"));
        Assert.IsTrue(json.Contains("PlayerTwo"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Created"));
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
        Assert.IsTrue(json.Contains("Status"));
        Assert.IsTrue(json.Contains("Ik zoek een"));
        Assert.IsTrue(json.Contains("PlayerOne"));
        Assert.IsTrue(json.Contains("mnopqr"));
        Assert.IsTrue(json.Contains("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]"));
        Assert.IsTrue(json.Contains("PlayerTwo"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Created"));
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
        };

        // Act
        var response = controller.CreateGame(dto);
        var json = response.ToJson();
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
        Assert.IsTrue(json.Contains("Id"));
        Assert.IsTrue(json.Contains("Description"));
        Assert.IsTrue(json.Contains("Token"));
        Assert.IsTrue(json.Contains("PlayerOne"));
        Assert.IsTrue(json.Contains("test"));
        Assert.IsTrue(json.Contains("Created"));
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
        
        // Assert
        Assert.IsInstanceOf<BadRequestResult>(response.Result);
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
        Assert.IsTrue(json.Contains("Board"));
        Assert.IsTrue(json.Contains("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Color"));
        Assert.IsTrue(json.Contains("None"));
        Assert.IsTrue(json.Contains("Token"));
        Assert.IsTrue(json.Contains("Status"));
        Assert.IsTrue(json.Contains("Created"));
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
        var player = new PlayerEntity(new PlayerOne("dfasfda"));
        var dto = new GameAddPlayerDto()
        {
            Token = entity.Token,
            PlayerToken = player.Token
        };

        // Act
        playerRepository.Add(player);
        repository.Add(entity);
        
        var response = controller.AddPlayerOneToGame(dto);
        var json = response.ToJson();
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
        Assert.IsTrue(json.Contains("Board"));
        Assert.IsTrue(json.Contains("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Color"));
        Assert.IsTrue(json.Contains("None"));
        Assert.IsTrue(json.Contains("Token"));
        Assert.IsTrue(json.Contains("Status"));
        Assert.IsTrue(json.Contains("Created"));
        Assert.IsTrue(json.Contains(player.Token));
        Assert.IsTrue(json.Contains(entity.Token));
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
        var player = new PlayerEntity(new PlayerTwo("vafdas"));
        var dto = new GameAddPlayerDto()
        {
            Token = entity.Token,
            PlayerToken = player.Token
        };

        // Act
        playerRepository.Add(player);
        repository.Add(entity);
        
        var response = controller.AddPlayerTwoToGame(dto);
        var json = response.ToJson();
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
        Assert.IsTrue(json.Contains("Board"));
        Assert.IsTrue(json.Contains("[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Color"));
        Assert.IsTrue(json.Contains("None"));
        Assert.IsTrue(json.Contains("Token"));
        Assert.IsTrue(json.Contains("Status"));
        Assert.IsTrue(json.Contains("Created"));
        Assert.IsTrue(json.Contains(player.Token));
        Assert.IsTrue(json.Contains(entity.Token));
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
        Assert.IsTrue(json.Contains("[0,0,0,1,1,1,0,0],[0,0,0,2,1,0,0,0]"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Color"));
        Assert.IsTrue(json.Contains("Black"));
        Assert.IsTrue(json.Contains("Token"));
        Assert.IsTrue(json.Contains("Status"));
        Assert.IsTrue(json.Contains("Playing"));
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
        var entity = repository.All().First();
        
        // Act 
        var response = controller.DoMoveGame(new GameDoMoveDto()
        {
            Token = entity.Token
        });
        var response1 = controller.DoMoveGame(null);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(response.Result);
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
            PlayerOne = new PlayerEntity(new PlayerOne("abcdef")),
            PlayerTwo = new PlayerEntity(new PlayerTwo("qwerty"))
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
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Color"));
        Assert.IsTrue(json.Contains("White"));
        Assert.IsTrue(json.Contains("Token"));
        Assert.IsTrue(json.Contains("abcdef"));
        Assert.IsFalse(json.Contains("qwerty"));
        Assert.IsTrue(json.Contains("Status"));
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
            PlayerOne = new PlayerEntity(new PlayerOne("abcdef")),
            PlayerTwo = new PlayerEntity(new PlayerTwo("qwerty"))
        };

        // Act 
        repository.Add(entity);
        entity.Game.Start();
        entity.UpdateEntity();
        
        var response = controller.IsFinishedGame(entity.Token);
        var json = response.ToJson();
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result);
        Assert.IsTrue(json.Contains("[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0]"));
        Assert.IsTrue(json.Contains("CurrentPlayer"));
        Assert.IsTrue(json.Contains("Color"));
        Assert.IsTrue(json.Contains("White"));
        Assert.IsTrue(json.Contains("Token"));
        Assert.IsTrue(json.Contains("abcdef"));
        Assert.IsTrue(json.Contains("Status"));
        Assert.IsTrue(json.Contains("Playing"));
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

internal class GamesRepositoryEmptyTest :  RepositoryBase<GameEntity>, IGamesRepository
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
    public bool DoesNotPlayAGame(PlayerEntity playerEntity)
    {
        bool playsAGame = false;
        foreach (var entity in this.All())
        {
            if (entity.PlayerOne != null && DoesPlayerPlayAGame(playerEntity, entity))
            {
                playsAGame = true;
            }
            
            if (entity.PlayerTwo != null && DoesPlayerPlayAGame(playerEntity, entity))
            {
                playsAGame = true;
            }
        }

        return !playsAGame;
    }

    /// <summary>
    /// Determines if the player plays the current game.
    /// </summary>
    /// <param name="playerEntity">The player.</param>
    /// <param name="gameEntity">The game.</param>
    /// <returns>True if the player plays the game.</returns>
    private static bool DoesPlayerPlayAGame(PlayerEntity playerEntity, GameEntity gameEntity)
    {
        if (gameEntity.PlayerOne != null && gameEntity.PlayerOne.Equals(playerEntity))
        {
            return !gameEntity.Game.IsQuit() && !gameEntity.Game.IsFinished();
        }
        
        if (gameEntity.PlayerTwo != null && gameEntity.PlayerTwo.Equals(playerEntity))
        {
            return !gameEntity.Game.IsQuit() && !gameEntity.Game.IsFinished();
        }

        return false;
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
