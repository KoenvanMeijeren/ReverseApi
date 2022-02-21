namespace ReversiApi.Model;

public class Player : IPlayer
{
    public string Token { get; }
    public Color Color { get; }

    public Player(Color color, string token = "")
    {
        this.Token = token;
        this.Color = color;
    }

    public override bool Equals(object? obj)
    {
        if (obj is IPlayer player)
        {
            return this.Token.Equals(player.Token) 
                   && this.Color.Equals(player.Color);
        }
        
        return base.Equals(obj);
    }
}