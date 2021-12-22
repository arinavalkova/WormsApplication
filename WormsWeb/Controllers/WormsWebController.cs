using System;
using System.Threading.Tasks;
using EntitiesLibrary.entities;
using EntitiesLibrary.entities.commands;
using Microsoft.AspNetCore.Mvc;
using WormsWeb.way;
using WormsWeb.way.type;

namespace WormsWeb.Controllers
{

    class Empty
    {
    }

    [ApiController]
    [Route("worms")]
    public class WormsWebController : ControllerBase
    {
        private const WayType WayType = way.type.WayType.Game;

        [HttpPost]
        [Route("{name}/getAction")]
        public async Task<object?> GetWay(WorldState worldState, string name, int step, int run)
        {
            var command = new WayManager(WayType).GetWayCommand(worldState, name, step, run);
            if (command == null) Console.WriteLine("Bad worm name!");
            // if (command.Direction == null)
            // {
            //     Console.WriteLine("Empty");
            //     return new JsonResult("{}");
            // }
           // Console.WriteLine(command.Direction);
            return command;
        }
    }
}