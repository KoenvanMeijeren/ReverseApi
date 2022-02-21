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
        
        // POST: api/Game
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // We use a DTO in order to prevent overposting.
        [HttpPost]
        public ActionResult PostGame([FromBody] GameCreateDto? gameCreateDto)
        {
            if (gameCreateDto == null)
            {
                return BadRequest();
            }
            
            IGame newGame = new Game();
            newGame.PlayerOne = new Player(Color.White, gameCreateDto.TokenPlayerOne);
            newGame.Description = gameCreateDto.Description;
            this._repository.Add(newGame);
            
            return CreatedAtRoute("getGameByTokenRoute", new {token = newGame.Token}, new GameInfoDto(newGame));
        }

        [HttpGet]
        [Route("{token}/status", Name = "getGameStatus")]
        public ActionResult<IGame> GetGameStatus(string? token)
        {
            if (!this.GameExists(token))
            {
                return NotFound();
            }

            var game = this._repository.Get(token);

            return Ok(new GameStatusDto(game));
        }

        [HttpPut("{token}/quit")]
        public ActionResult<IGame> QuitGame(string? token)
        {
            if (!this.GameExists(token))
            {
                return NotFound();
            }

            var game = this._repository.Get(token);

            return Ok(new GameStatusDto(game));
        }

        /// <summary>
        /// Determines if the game exists.
        /// </summary>
        /// <param name="token">The unique token of the game.</param>
        /// <returns>Whether the game exists or not.</returns>
        private bool GameExists(string? token)
        {
            if (token == null)
            {
                return false;
            }
            
            return this._repository.Get(token) != null;
        }

    }
}