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
    
    // GET api/Game/queue
    [HttpGet("queue")]
    public ActionResult<IEnumerable<GameInfoDto>> GetGamesInQueue()
    {
        var entities = from entity in this._repository.AllInQueue() select new GameInfoDto(entity);
        if (!entities.Any())
        {
            return NotFound();
        }
                
        return Ok(entities);
    }

    // GET api/Game/{token}
    [HttpGet]
    [Route("{token}", Name = "getGameByTokenRoute")] 
    public ActionResult<GameInfoDto> GetByToken(string? token)
    {
        if (token == null)
        {
            return BadRequest();
        }
            
        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return NotFound();
        }
        
        return Ok(new GameInfoDto(entity));
    }
        
    // GET api/Game/player-one/{token}
    [HttpGet]
    [Route("player-one/{token}", Name = "getGameByPlayerOneTokenRoute")] 
    public ActionResult<GameInfoDto> GetByPlayerOneToken(string? token)
    {
        if (token == null)
        {
            return BadRequest();
        }
            
        var entity = this._repository.GetByPlayerOne(token);
        if (entity == null)
        {
            return NotFound();
        }
    
        return Ok(new GameInfoDto(entity));
    }
        
    // GET api/Game/player-two/{token}
    [HttpGet]
    [Route("player-two/{token}", Name = "getGameByPlayerTwoTokenRoute")] 
    public ActionResult<GameInfoDto> GetByPlayerTwoToken(string? token)
    {
        if (token == null)
        {
            return BadRequest();
        }
            
        var entity = this._repository.GetByPlayerTwo(token);
        if (entity == null)
        {
            return NotFound();
        }
    
        return Ok(new GameInfoDto(entity));
    }
    
    [HttpGet]
    [Route("{token}/status", Name = "getGameStatus")]
    public ActionResult<GameStatusDto> GetGameStatus(string? token)
    {
        if (token == null)
        {
            return BadRequest();
        }
        
        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(new GameStatusDto(entity));
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
            return BadRequest();
        }

        GameEntity entity = new GameEntity
        {
            Description = gameCreateDto.Description
        };
        
        this._repository.Add(entity);
            
        return Ok(new GameInfoDto(entity));
    }
        
    [HttpPut("add/player-one")]
    public ActionResult<GameInfoDto> AddPlayerOneToGame([FromBody] GameAddPlayerDto? gameAddPlayer)
    {
        if (gameAddPlayer == null || !gameAddPlayer.ValidData())
        {
            return BadRequest();
        }
            
        var entity = this._repository.Get(gameAddPlayer.Token);
        if (entity == null)
        {
            return NotFound();
        }

        if (entity.PlayerOne != null)
        {
            throw new InvalidOperationException("Speler 1 is al ingesteld!");
        }
        
        PlayerEntity player = new PlayerEntity(new PlayerOne(gameAddPlayer.PlayerToken));
        player = this._playersRepository.FirstOrCreate(player);
        if (!this._repository.DoesNotPlayAGame(player))
        {
            throw new InvalidOperationException("Deze speler speelt al een Reversi potje!");
        }
        
        if (!player.ValidPlayerOne())
        {
            throw new ArgumentException("De gevonden speler is niet ingesteld als speler 1!");
        }
        
        entity.PlayerOne = player;
        this._repository.Update(entity);
    
        return Ok(new GameInfoDto(entity));
    }
        
    [HttpPut("add/player-two")]
    public ActionResult<GameInfoDto> AddPlayerTwoToGame([FromBody] GameAddPlayerDto? gameAddPlayer)
    {
        if (gameAddPlayer == null || !gameAddPlayer.ValidData())
        {
            return BadRequest();
        }
            
        var entity = this._repository.Get(gameAddPlayer.Token);
        if (entity == null)
        {
            return NotFound();
        }

        if (entity.PlayerTwo != null)
        {
            throw new InvalidOperationException("Speler 2 is al ingesteld!");
        }
    
        PlayerEntity player = new PlayerEntity(new PlayerTwo(gameAddPlayer.PlayerToken));
        player = this._playersRepository.FirstOrCreate(player);
        if (!this._repository.DoesNotPlayAGame(player))
        {
            throw new InvalidOperationException("Deze speler speelt al een Reversi potje!");
        }
        
        if (!player.ValidPlayerTwo())
        {
            throw new ArgumentException("De gevonden speler is niet ingesteld als speler 2!");
        }

        entity.PlayerTwo = player;
        this._repository.Update(entity);
    
        return Ok(new GameInfoDto(entity));
    }
        
    [HttpPut("{token}/start")]
    public ActionResult<GameStatusDto> StartGame(string? token)
    {
        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return NotFound();
        }
        
        entity.Game.Start();
        this._repository.Update(entity);
    
        return Ok(new GameStatusDto(entity));
    }
        
    [HttpPut("do-move")]
    public ActionResult<GameStatusDto> DoMoveGame([FromBody] GameDoMoveDto? gameDoMove)
    {
        if (gameDoMove == null || !gameDoMove.ValidData())
        {
            return BadRequest();
        }
            
        var entity = this._repository.Get(gameDoMove.Token);
        if (entity == null)
        {
            return NotFound();
        }
    
        if (entity.CurrentPlayer == null || !entity.CurrentPlayer.Token.Equals(gameDoMove.PlayerToken))
        {
            return BadRequest();
        }
            
        entity.Game.DoMove(gameDoMove.Row, gameDoMove.Column);
        this._repository.Update(entity);
            
        return Ok(new GameStatusDto(entity));
    }
    
    [HttpPut("{token}/quit")]
    public ActionResult<GameStatusDto> QuitGame(string? token)
    {
        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return NotFound();
        }
        
        entity.Game.Quit();
        this._repository.Update(entity);
    
        return Ok(new GameStatusDto(entity));
    }
    
    [HttpGet("{token}/finished")]
    public ActionResult<GameStatusDto> IsFinishedGame(string? token)
    {
        var entity = this._repository.Get(token);
        if (entity == null)
        {
            return NotFound();
        }
    
        entity.Game.IsFinished();
        this._repository.Update(entity);
    
        return Ok(new GameStatusDto(entity));
    }

}