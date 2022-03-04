using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ReversiApi.Model.Game;

public class GameEntity : IEntity
{
    [Key]
    public int Id { get; set;  }
    
    public string? Description { get; set; }
    
    [Required]
    public string Token { get; set; }
    
    public int? PlayerOneId { get; set; }
    public PlayerEntity? PlayerOne { get; set; }
    
    public int? PlayerTwoId { get; set; }
    public PlayerEntity? PlayerTwo { get; set; }
    
    public PlayerEntity? CurrentPlayer { get; set; }
    
    [Required]
    public string Board { get; set; }
    
    [Required]
    public Status Status { get; set; }

    [NotMapped]
    public IGame Game { get; }

    public GameEntity(int id = IEntity.IdUndefined) : this(new Game(), id)
    {
        
    }
    
    public GameEntity(IGame game, int id = IEntity.IdUndefined)
    {
        this.Id = id;
        this.Game = game;
        this.Board = JsonConvert.SerializeObject(game.Board);
        this.Status = game.Status;
        this.Token = GameEntity.GenerateToken();
        
        this.UpdateGame();
    }

    public void UpdateGame()
    {
        this.Game.Board = JsonConvert.DeserializeObject<Color[,]>(this.Board);
        this.Game.Status = this.Status;

        if (this.PlayerOne != null)
        {
            this.Game.PlayerOne = new PlayerOne(this.PlayerOne.Token);
        }
        
        if (this.PlayerTwo != null)
        {
            this.Game.PlayerTwo = new PlayerTwo(this.PlayerTwo.Token);
        }

        if (!this.Game.IsPlaying())
        {
            return;
        }
        
        this.Game.CurrentPlayer = this.Game.PlayerOne;
        if (this.CurrentPlayer != null && this.CurrentPlayer.Token.Equals(this.PlayerTwo?.Token))
        {
            this.Game.CurrentPlayer = this.Game.PlayerTwo;
        }
    }
    
    public void UpdateEntity()
    {
        this.Board = JsonConvert.SerializeObject(this.Game.Board);
        this.Status = this.Game.Status;

        if (this.Game.IsCreated() && this.PlayerOne != null && this.PlayerTwo != null)
        {
            this.Status = Status.Pending;
        }
        
        if (!this.Game.IsPlaying())
        {
            return;
        }
        
        this.CurrentPlayer = this.PlayerOne;
        if (this.Game.CurrentPlayer.Token.Equals(this.PlayerTwo?.Token))
        {
            this.CurrentPlayer = this.PlayerTwo;
        }
    }
    
    /// <summary>
    /// Generates the token for the game.
    ///
    /// Avoids using the '/' and '+' symbols, because the requests are done via API calls with the token as ID.
    /// </summary>
    /// <returns>The generated token.</returns>
    private static string GenerateToken()
    {
        return Convert
            .ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("/", "s")
            .Replace("=", "i")
            .Replace("?", "q")
            .Replace("+", "p");
    }
    
}