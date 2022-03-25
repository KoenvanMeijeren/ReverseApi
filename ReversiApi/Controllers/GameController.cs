using ReversiApi.Repository.Contracts;

#nullable enable

namespace ReversiApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{

    private readonly IGamesRepository _repository;
    private readonly IPlayersRepository _playersRepository;

    public GameController(IGamesRepository repository, IPlayersRepository playersRepository)
    {
        this._repository = repository;
        this._playersRepository = playersRepository;
    }

    // GET api/Game/all/{status?}
    [HttpGet("all/{status?}")]
    public ActionResult<IEnumerable<GameInfoDto>> All(string? status = "queued")
    {
        var entities = from entity in this._repository.AllByStatus(status) select new GameInfoDto(entity);
        if (!entities.Any())
        {
            return this.NotFound();
        }

        return this.Ok(entities);
    }

    // GET api/Game/{token}
    [HttpGet]
    [Route("{token}", Name = "getGameByTokenRoute")]
    public ActionResult<GameInfoDto> GetByToken(string? token)
    {
        if (token == null)
        {
            return this.BadRequest();
        }

        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return this.NotFound();
        }

        return this.Ok(new GameInfoDto(entity));
    }

    // GET api/Game/player-one/{token}
    [HttpGet]
    [Route("player-one/{token}/{status?}", Name = "getGameByPlayerOneTokenRoute")]
    public ActionResult<GameInfoDto> GetByPlayerOneToken(string? token, string? status = "all")
    {
        if (token == null)
        {
            return this.BadRequest();
        }

        var entity = this._repository.GetByPlayerOne(token, status);
        if (entity == null)
        {
            return this.NotFound();
        }

        return this.Ok(new GameInfoDto(entity));
    }

    // GET api/Game/player-two/{token}
    [HttpGet]
    [Route("player-two/{token}/{status?}", Name = "getGameByPlayerTwoTokenRoute")]
    public ActionResult<GameInfoDto> GetByPlayerTwoToken(string? token, string? status = "all")
    {
        if (token == null)
        {
            return this.BadRequest();
        }

        var entity = this._repository.GetByPlayerTwo(token, status);
        if (entity == null)
        {
            return this.NotFound();
        }

        return this.Ok(new GameInfoDto(entity));
    }

    [HttpGet]
    [Route("{token}/status", Name = "getGameStatus")]
    public ActionResult<GameStatusDto> GetGameStatus(string? token)
    {
        if (token == null)
        {
            return this.BadRequest();
        }

        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return this.NotFound();
        }

        return this.Ok(new GameStatusDto(entity));
    }
    
    [HttpGet]
    [Route("move-possible", Name = "isMovePossible")]
    public ActionResult<bool> IsMovePossible([FromBody] GameCanMoveDto? gameCanMoveDto)
    {
        var entity = this._repository.Get(gameCanMoveDto?.Token);
        if (entity == null || gameCanMoveDto == null || !gameCanMoveDto.ValidData())
        {
            return this.NotFound();
        }

        return this.Ok(entity.Game.IsMovePossible(gameCanMoveDto.Row, gameCanMoveDto.Column));
    }
    
    [HttpGet]
    [Route("{token}/possible-moves", Name = "getPossibleMoves")]
    public ActionResult<bool[,]> GetPossibleMoves(string? token)
    {
        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return this.NotFound();
        }

        return this.Ok(entity.GetPossibleMoves());
    }

    // POST: api/Game
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754.
    // We use a DTO in order to prevent overposting. Overposting is done by changing more fields then allowed. 
    // See: https://andrewlock.net/preventing-mass-assignment-or-over-posting-in-asp-net-core/.
    [HttpPost]
    public ActionResult<GameInfoDto> CreateGame([FromBody] GameCreateDto? gameCreateDto)
    {
        if (gameCreateDto == null || !gameCreateDto.ValidData())
        {
            return this.BadRequest();
        }

        var entity = new GameEntity
        {
            Description = gameCreateDto.Description
        };

        this._repository.Add(entity);

        return this.Ok(new GameInfoDto(entity));
    }

    [HttpPut("add/player-one")]
    public ActionResult<GameInfoDto> AddPlayerOneToGame([FromBody] GameAddPlayerDto? gameAddPlayer)
    {
        if (gameAddPlayer == null || !gameAddPlayer.ValidData())
        {
            return this.BadRequest();
        }

        var entity = this._repository.Get(gameAddPlayer.Token);
        if (entity == null)
        {
            return this.NotFound();
        }

        if (entity.PlayerOne != null)
        {
            throw new InvalidOperationException("Speler 1 is al ingesteld!");
        }

        var player = new PlayerEntity(token: gameAddPlayer.PlayerToken, name: gameAddPlayer.Name);
        player = this._playersRepository.FirstOrCreate(player);
        if (!this._repository.DoesNotPlayAGame(player))
        {
            throw new InvalidOperationException("Deze speler speelt al een Reversi potje!");
        }

        entity.PlayerOne = player;
        this._repository.Update(entity);

        return this.Ok(new GameInfoDto(entity));
    }

    [HttpPut("add/player-two")]
    public ActionResult<GameInfoDto> AddPlayerTwoToGame([FromBody] GameAddPlayerDto? gameAddPlayer)
    {
        if (gameAddPlayer == null || !gameAddPlayer.ValidData())
        {
            return this.BadRequest();
        }

        var entity = this._repository.Get(gameAddPlayer.Token);
        if (entity == null)
        {
            return this.NotFound();
        }

        if (entity.PlayerTwo != null)
        {
            throw new InvalidOperationException("Speler 2 is al ingesteld!");
        }

        var player = new PlayerEntity(token: gameAddPlayer.PlayerToken, name: gameAddPlayer.Name);
        player = this._playersRepository.FirstOrCreate(player);
        if (!this._repository.DoesNotPlayAGame(player))
        {
            throw new InvalidOperationException("Deze speler speelt al een Reversi potje!");
        }

        entity.PlayerTwo = player;
        this._repository.Update(entity);

        return this.Ok(new GameInfoDto(entity));
    }

    [HttpPut("{token}/start")]
    public ActionResult<GameStatusDto> StartGame(string? token)
    {
        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return this.NotFound();
        }

        entity.Game.Start();
        this._repository.Update(entity);

        return this.Ok(new GameStatusDto(entity));
    }

    [HttpPut("do-move")]
    public ActionResult<GameStatusDto> DoMoveGame([FromBody] GameDoMoveDto? gameDoMove)
    {
        if (gameDoMove == null || !gameDoMove.ValidData())
        {
            return this.BadRequest();
        }

        var entity = this._repository.Get(gameDoMove.Token);
        if (entity == null)
        {
            return this.NotFound();
        }

        if (entity.CurrentPlayer == null || !entity.CurrentPlayer.Token.Equals(gameDoMove.PlayerToken))
        {
            return this.BadRequest();
        }

        entity.Game.DoMove(gameDoMove.Row, gameDoMove.Column);
        this._repository.Update(entity);

        return this.Ok(new GameStatusDto(entity));
    }

    [HttpPut("{token}/quit")]
    public ActionResult<GameStatusDto> QuitGame(string? token)
    {
        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return this.NotFound();
        }

        entity.Game.Quit();
        this._repository.Update(entity);

        return this.Ok(new GameStatusDto(entity));
    }

    [HttpGet("{token}/finished")]
    public ActionResult<GameStatusDto> IsFinishedGame(string? token)
    {
        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return this.NotFound();
        }

        entity.Game.IsFinished();
        this._repository.Update(entity);

        return this.Ok(new GameStatusDto(entity));
    }

}
