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
    public string Name { get; set; }

    public ICollection<GameEntity> GamesPlayerOne { get; set; }
    public ICollection<GameEntity> GamesPlayerTwo { get; set; }

    public PlayerEntity(int id = IEntity.IdUndefined, string token = "", string name = "")
    {
        this.Id = id;
        this.Token = token;
        this.Name = name;
        this.GamesPlayerOne = new List<GameEntity>();
        this.GamesPlayerTwo = new List<GameEntity>();
    }

    public override bool Equals(object? obj)
    {
        if (obj is PlayerEntity player)
        {
            return this.Token.Equals(player.Token);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Token);
    }

}
