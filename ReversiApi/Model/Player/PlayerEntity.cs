using System.ComponentModel.DataAnnotations;

namespace ReversiApi.Model.Player;

/// <summary>
/// Provides an entity for saving the players to the database. The game entity is responsible for converting the players
/// back to their corresponding objects.
/// </summary>
public class PlayerEntity : IEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Token { get; set; }
    
    [Required]
    public Color Color { get; set; }
    
    public ICollection<GameEntity> GamesPlayerOne { get; set; }
    public ICollection<GameEntity> GamesPlayerTwo { get; set; }

    public PlayerEntity()
    {
        
    }

    public PlayerEntity(IPlayer player)
    {
        this.Token = player.Token;
        this.Color = player.Color;
    }
    
    
}