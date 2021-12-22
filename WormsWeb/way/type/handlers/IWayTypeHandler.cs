using EntitiesLibrary;
using EntitiesLibrary.entities;
using EntitiesLibrary.entities.commands;
using WormsApplication.entities;

namespace WormsWeb.way.type.handlers
{
    public interface IWayTypeHandler
    {
        public Command GetCommand(WorldState worldState, Worm worm, int step, int run);
    }
}