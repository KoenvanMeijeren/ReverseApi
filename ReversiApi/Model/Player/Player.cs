namespace ReversiApi.Model.Player;

public class PlayerOne : Player
{
    public PlayerOne(string token = ""): base(Color.White, token)
    {
        
    }
}

public class PlayerTwo : Player
{
    public PlayerTwo(string token = ""): base(Color.Black, token)
    {
        
    }
}

public class PlayerUndefined : Player
{
    public PlayerUndefined() : base(Color.None)
    {
        
    }
}

public abstract class Player : IPlayer
{
    public string Token { get; }
    public Color Color { get; }

    protected Player(Color color, string token = "")
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
        
        return false;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Token, (int) this.Color);
    }
}