using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

    }
}