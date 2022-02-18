#nullable enable
using Microsoft.AspNetCore.Mvc;
using ReversiApi.Model;
using ReversiApi.Model.DataTransferObject.Game;
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
        
        // GET api/Game
        [HttpGet]
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
        public ActionResult<IGame> GetByToken(string token)
        {
            var game = this._repository.Get(token);
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
            newGame.TokenPlayerOne = gameCreateDto.TokenPlayerOne;
            newGame.Description = gameCreateDto.Description;
            this._repository.Add(newGame);
            
            return CreatedAtRoute("getGameByTokenRoute", new {token = newGame.Token}, new GameInfoDto(newGame));
        }

    }
}