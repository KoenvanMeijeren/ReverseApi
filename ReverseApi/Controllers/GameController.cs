using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ReverseApi.Model;
using ReverseApi.Model.DataTransferObject.Game;
using ReverseApi.Repository;

namespace ReverseApi.Controllers
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
        
        // GET api/game
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetDescriptionsOfGameInQueue()
        {
            return Ok(from game in this._repository.AllInQueue() select game.Description);
        }

        // GET api/game/{token}
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
        
        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // We use a DTO in order to prevent overposting.
        [HttpPost]
        public ActionResult PutGame([FromBody] GameCreateDto gameCreateDto)
        {
            IGame newGame = new Game();
            newGame.TokenPlayerOne = gameCreateDto.TokenPlayerOne;
            newGame.Description = gameCreateDto.Description;
            this._repository.Add(newGame);
            
            return CreatedAtRoute("getGameByTokenRoute", new {token = newGame.Token}, new GameInfoDto(newGame));
        }

    }
}