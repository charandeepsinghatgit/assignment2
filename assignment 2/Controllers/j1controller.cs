using Microsoft.AspNetCore.Mvc;


namespace CCCWebAPI.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class J1Controller : ControllerBase
        {
            /// Calculates the final score in the "Deliv-e-droid" game based on deliveries and collisions.
            /// POST /api/J1/Delivedroid with body Collisions=2&Deliveries=5 returns 730
            /// POST /api/J1/Delivedroid with body Collisions=10&Deliveries=0 returns -100
            [HttpPost("Delivedroid")]
            public IActionResult CalculateScore([FromForm] int collisions, [FromForm] int deliveries)
            {
                int score = (deliveries * 50) - (collisions * 10);

                if (deliveries > collisions)
                {
                    score += 500;
                }

                return Ok(score);
            }
            /// Determines whether the user will get on the next roller coaster train ride.
            /// POST /api/J1/Rollercoaster with body PositionInLine=14&Cars=3&PeoplePerCar=2 returns "no"
            /// POST /api/J1/Rollercoaster with body PositionInLine=12&Cars=4&PeoplePerCar=3 returns "yes"
            [HttpPost("Rollercoaster")]
            public IActionResult WillRide([FromForm] int positionInLine, [FromForm] int cars, [FromForm] int peoplePerCar)
            {
                int capacity = cars * peoplePerCar;
                string result = positionInLine <= capacity ? "yes" : "no";
                return Ok(result);
            }

        }

   }