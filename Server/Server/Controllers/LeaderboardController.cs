using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class LeaderboardController : Controller
    {
        private LeaderboardModel _leaderboard;

        public LeaderboardController(LeaderboardModel model)
        {
            _leaderboard = model;
        }

        // GET api/leaderboard
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_leaderboard.GetLeaderboardDto());;
        }

        // POST api/leaderboard
        [HttpPost]
        public IActionResult Post([FromBody]PlayerResultModel newResult)
        {
            if (newResult == null)
                return NotFound();

            _leaderboard.AddResult(newResult);
            return Ok();
        }
    }
}
