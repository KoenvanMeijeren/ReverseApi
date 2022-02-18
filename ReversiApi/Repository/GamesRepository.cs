﻿using ReversiApi.Model;

namespace ReversiApi.Repository;

/// <summary>
/// Provides a repository for the game.
/// </summary>
public class GamesRepository : IGamesRepository
{

    private List<IGame> _games;

    public GamesRepository()
    {
        IGame game1 = new Game();
        IGame game2 = new Game();
        IGame game3 = new Game();

        game1.TokenPlayerOne = "abcdef";
        game1.Description = "Potje snel reveri, dus niet lang nadenken";
        game2.TokenPlayerOne = "ghijkl";
        game2.TokenPlayerTwo = "mnopqr";
        game2.Description = "Ik zoek een gevorderde tegenspeler!";
        game3.TokenPlayerOne = "stuvwx";
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
        return this.All().Where(game => game.TokenPlayerTwo == null);
    }

    /// <inheritdoc />
    public IGame? Get(string token)
    {
        return this._games.Find(game => game.Token.Equals(token));
    }
}