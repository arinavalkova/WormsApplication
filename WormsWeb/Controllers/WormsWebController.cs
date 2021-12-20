using System;
using System.Threading.Tasks;
using EntitiesLibrary.entities.commands;
using Microsoft.AspNetCore.Mvc;
using WormsApplication.entities;
using WormsWeb.way;
using WormsWeb.way.type;

namespace WormsWeb.Controllers
{
    [ApiController]
    [Route("worms")]
    public class WormsWebController : ControllerBase
    {
        private const WayType WayType = way.type.WayType.Game;

        [HttpPost]
        [Route("{name}/getAction")]
        public async Task<Command?> GetWay(WorldState worldState, string name, int step, int run)
        {
            var command = new WayManager(WayType).GetWayCommand(worldState, name);
            if (command == null) Console.WriteLine("Bad worm name!");
            return command;
        }
    }
}