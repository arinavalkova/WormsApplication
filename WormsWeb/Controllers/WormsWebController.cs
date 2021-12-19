using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WormsApplication.entities;

namespace WormsWeb.Controllers
{
    [ApiController]
    [Route("worms")]
    public class WormsWebController : ControllerBase
    {
       
        [HttpPost]
        public async Task<ActionResult> Post(WorldState worldState)
        {
            Console.WriteLine(worldState);
            return Ok();
        }
    }
}