using ReversiApi.Model;

namespace ReversiApi.Repository;

/// <summary>
/// Provides a repository for the game.
/// </summary>
public class GamesRepository : IGamesRepository
{

    private readonly List<IGame> _games;

    public GamesRepository()
    {
        IGame game1 = new Game();
        IGame game2 = new Game();
        IGame game3 = new Game();

        game1.PlayerOne = new Player(Color.White, "abcdef");
        game1.Description = "Potje snel reveri, dus niet lang nadenken";
        game2.PlayerOne = new Player(Color.White, "ghijkl");
        game2.PlayerTwo = new Player(Color.Black, "mnopqr");
        game2.Description = "Ik zoek een gevorderde tegenspeler!";
        game3.PlayerOne = new Player(Color.White, "stuvwx");
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
        return this.All().Where(game => game.PlayerTwo == null);
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