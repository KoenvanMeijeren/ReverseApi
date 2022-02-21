#nullable enable
using Microsoft.AspNetCore.Mvc;
using ReversiApi.Model.Game;
using ReversiApi.Model.Game.DataTransferObject;
using ReversiApi.Model.Player;
using ReversiApi.Repository;

namespace ReversiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private readonly IGamesRepository _repository;

        public GameController(IGamesRepository repository)
        {
            this._repository = repository;
        }
        
        // GET api/Game/queue
        [HttpGet("queue")]
        public ActionResult<IEnumerable<GameInfoDto>> GetGamesInQueue()
        {
            var result = from game in this._repository.AllInQueue() select new GameInfoDto(game);
            if (!result.Any())
            {
                return NotFound();
            }
                
            return Ok(result);
        }

        // GET api/Game/queue
        [HttpGet("queue/descriptions")]
        public ActionResult<IEnumerable<string>> GetDescriptionsOfGameInQueue()
        {
            var result = from game in this._repository.AllInQueue() select game.Description;
            if (!result.Any())
            {
                return NotFound();
            }
                
            return Ok(result);
        }

        // GET api/Game/{token}
        [HttpGet]
        [Route("{token}", Name = "getGameByTokenRoute")] 
        public ActionResult<IGame> GetByToken(string? token)
        {
            if (token == null)
            {
                return NotFound();
            }
            
            var game = this._repository.Get(token);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(new GameInfoDto(game));
        }
        
        // GET api/Game/player-one/{token}
        [HttpGet]
        [Route("player-one/{token}", Name = "getGameByPlayerOneTokenRoute")] 
        public ActionResult<IGame> GetByPlayerOneToken(string? token)
        {
            if (token == null)
            {
                return NotFound();
            }
            
            var game = this._repository.GetByPlayerOne(token);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(new GameInfoDto(game));
        }
        
        // GET api/Game/player-two/{token}
        [HttpGet]
        [Route("player-two/{token}", Name = "getGameByPlayerTwoTokenRoute")] 
        public ActionResult<IGame> GetByPlayerTwoToken(string? token)
        {
            if (token == null)
            {
                return NotFound();
            }
            
            var game = this._repository.GetByPlayerTwo(token);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(new GameInfoDto(game));
        }

        [HttpGet]
        [Route("{token}/status", Name = "getGameStatus")]
        public ActionResult<IGame> GetGameStatus(string? token)
        {
            if (!this._repository.Exists(token))
            {
                return NotFound();
            }

            var game = this._repository.Get(token);

            return Ok(new GameStatusDto(game));
        }
        
        // POST: api/Game
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // We use a DTO in order to prevent overposting. Overposting is done by changing more fields then allowed. 
        // See: https://andrewlock.net/preventing-mass-assignment-or-over-posting-in-asp-net-core/.
        [HttpPost]
        public ActionResult CreateGame([FromBody] GameCreateDto? gameCreateDto)
        {
            if (gameCreateDto == null)
            {
                return BadRequest();
            }
            
            IGame newGame = new Game();
            newGame.PlayerOne = new PlayerOne(gameCreateDto.TokenPlayerOne);
            newGame.Description = gameCreateDto.Description;
            this._repository.Add(newGame);
            
            return CreatedAtRoute("getGameByTokenRoute", new {token = newGame.Token}, new GameInfoDto(newGame));
        }
        
        [HttpPut("add/player-one")]
        public ActionResult<IGame> AddPlayerOneToGame([FromBody] GameAddPlayer? gameAddPlayer)
        {
            if (gameAddPlayer == null)
            {
                return BadRequest();
            }
            
            if (!this._repository.Exists(gameAddPlayer.Token))
            {
                return NotFound();
            }

            var game = this._repository.Get(gameAddPlayer.Token);
            game.PlayerOne = new PlayerOne(gameAddPlayer.PlayerToken);

            return Ok(new GameInfoDto(game));
        }
        
        [HttpPut("add/player-two")]
        public ActionResult<IGame> AddPlayerTwoToGame([FromBody] GameAddPlayer? gameAddPlayer)
        {
            if (gameAddPlayer == null)
            {
                return BadRequest();
            }
            
            if (!this._repository.Exists(gameAddPlayer.Token))
            {
                return NotFound();
            }

            var game = this._repository.Get(gameAddPlayer.Token);
            game.PlayerTwo = new PlayerTwo(gameAddPlayer.PlayerToken);

            return Ok(new GameInfoDto(game));
        }
        
        [HttpPut("{token}/start")]
        public ActionResult<IGame> StartGame(string? token)
        {
            if (!this._repository.Exists(token))
            {
                return NotFound();
            }

            var game = this._repository.Get(token);
            game.Start();

            return Ok(new GameStatusDto(game));
        }
        
        [HttpPut("do-move")]
        public ActionResult<IGame> DoMoveGame([FromBody] GameDoMoveDto? gameDoMove)
        {
            if (gameDoMove == null)
            {
                return BadRequest();
            }
            
            if (!this._repository.Exists(gameDoMove.Token))
            {
                return NotFound();
            }

            var game = this._repository.Get(gameDoMove.Token);
            if (!game.CurrentPlayer.Token.Equals(gameDoMove.PlayerToken))
            {
                return BadRequest();
            }
            
            game.DoMove(gameDoMove.Row, gameDoMove.Column);
            
            return Ok(new GameStatusDto(game));
        }

        [HttpPut("{token}/quit")]
        public ActionResult<IGame> QuitGame(string? token)
        {
            if (!this._repository.Exists(token))
            {
                return NotFound();
            }

            var game = this._repository.Get(token);
            game.Quit();

            return Ok(new GameStatusDto(game));
        }

        [HttpGet("{token}/finished")]
        public ActionResult<IGame> IsFinishedGame(string? token)
        {
            if (!this._repository.Exists(token))
            {
                return NotFound();
            }

            var game = this._repository.Get(token);
            game.IsFinished();

            return Ok(new GameStatusDto(game));
        }

    }
}