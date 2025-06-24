using Microsoft.AspNetCore.Mvc;
namespace assignment_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class J3Controller : ControllerBase
    {
        /// Decodes a list of 5-digit instructions into movement directions and steps.
        /// POST /api/J3/SecretFormula with body {"instructions":["57234","00907","34100","99999"]}
        /// returns ["right 234", "right 907", "left 100"]
        [HttpPost("SecretFormula")]
        public IActionResult DecodeInstructions([FromBody] InstructionInput input)
        {
            List<string> output = new List<string>();
            string previousDirection = "";

            foreach (var instruction in input.Instructions)
            {
                if (instruction == "99999") break;
                string direction;
                int d1 = int.Parse(instruction.Substring(0, 1));
                int d2 = int.Parse(instruction.Substring(1, 1));
                int sum = d1 + d2;
                string steps = instruction.Substring(2);

                if (sum == 0)
                {
                    direction = previousDirection;
                }
                else if (sum % 2 == 1)
                {
                    direction = "left";
                }
                else
                {
                    direction = "right";
                }

                output.Add($"{direction} {int.Parse(steps)}");
                previousDirection = direction;
            }
            return Ok(output);
        }
    }
}
