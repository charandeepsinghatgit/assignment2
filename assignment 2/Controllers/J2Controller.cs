using assignment_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace assignment_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class J2Controller : ControllerBase
    {
        /// Calculates the total Scoville Heat Units (SHU) for a comma-separated list of peppers.
        /// GET /api/J2/ChiliPeppers?Ingredients=Poblano,Thai returns 76500
        [HttpGet("ChiliPeppers")]
        public IActionResult GetTotalSHU([FromQuery] string Ingredients)
        {
            var pepperMap = new Dictionary<string, int>(System.StringComparer.OrdinalIgnoreCase)
            {
                { "Poblano", 1500 },
                { "Mirasol", 6000 },
                { "Serrano", 15500 },
                { "Cayenne", 40000 },
                { "Thai", 75000 },
                { "Habanero", 125000 }
            };

            int totalSHU = 0;
            if (!string.IsNullOrEmpty(Ingredients))
            {
                string[] peppers = Ingredients.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var pepper in peppers)
                {
                    if (pepperMap.TryGetValue(pepper.Trim(), out int shu))
                        totalSHU += shu;
                }
            }
            return Ok(totalSHU);
        }

        /// Determines the winner of a silent auction based on highest bid.
        /// POST /api/J2/SilentAuction with body {"bids":[{"name":"Alice","amount":20},{"name":"Bob","amount":30}]} returns "Bob"
        [HttpPost("SilentAuction")]
        public IActionResult GetWinner([FromBody] AuctionInput input)
        {
            if (input?.Bids == null || input.Bids.Count == 0)
                return BadRequest("No bids submitted.");

            Bid winner = input.Bids[0];
            foreach (var bid in input.Bids)
            {
                if (bid.Amount > winner.Amount)
                    winner = bid;
            }
            return Ok(winner.Name);
        }
    }
}
