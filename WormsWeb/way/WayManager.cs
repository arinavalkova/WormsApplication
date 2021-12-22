using System.Collections.Generic;
using System.Linq;
using EntitiesLibrary;
using EntitiesLibrary.entities;
using EntitiesLibrary.entities.commands;
using WormsApplication.entities;
using WormsWeb.way.type;
using WormsWeb.way.type.handlers;

namespace WormsWeb.way
{
    public class WayManager
    {
        private readonly WayType _wayType;

        private readonly Dictionary<WayType, ITypeHandler> _wayHandler = new()
        {
            [WayType.Circle] = new CircleTypeHandler(),
            [WayType.NearestFood] = new NearestFoodTypeHandler(),
            [WayType.Game] = new GameTypeHandler(),
        };

        private static int _circleCommandListPosition = 0;

        private static readonly List<Command> CircleCommandList = new()
        {
            new Command {Direction = Direction.UP, Split = false},
            new Command {Direction = Direction.RIGHT, Split = false},
            new Command {Direction = Direction.DOWN, Split = false},
            new Command {Direction = Direction.DOWN, Split = false},
            new Command {Direction = Direction.LEFT, Split = false},
            new Command {Direction = Direction.LEFT, Split = false},
            new Command {Direction = Direction.UP, Split = false},
            new Command {Direction = Direction.UP, Split = false},
            new Command {Direction = Direction.RIGHT, Split = false},
            new Command {Direction = Direction.DOWN, Split = false}
        };

        public WayManager(WayType wayType)
        {
            _wayType = wayType;
        }

        public Command? GetWayCommand(WorldState worldState, string name, int step, int run)
        {
            var wormList = worldState.Worms;
            var worm = wormList!.FirstOrDefault(currentWorm => currentWorm.Name == name);

            return worm == null ?
                null 
                : 
                _wayHandler[_wayType].Invoke(worldState, worm, step, run);
        }

        public Command GetWayCommand(WorldState worldState, Worm worm, int step, int run)
        {
            return _wayHandler[_wayType].Invoke(worldState, worm, step, run);
        }

        private interface ITypeHandler
        {
            public Command Invoke(WorldState worldState, Worm worm, int step, int run);
        }

        private class CircleTypeHandler : ITypeHandler
        {
            public Command Invoke(WorldState worldState, Worm worm, int step, int run)
            {
                var command = CircleCommandList[_circleCommandListPosition];
                if (_circleCommandListPosition + 1 == CircleCommandList.Count) _circleCommandListPosition = 0;
                else _circleCommandListPosition++;
                return command;
            }
        }

        private class NearestFoodTypeHandler : ITypeHandler
        {
            public Command Invoke(WorldState worldState, Worm worm, int step, int run)
            {
                return new NearestFoodHandler().GetCommand(worldState, worm, step, run);
            }
        }

        private class GameTypeHandler : ITypeHandler
        {
            public Command Invoke(WorldState worldState, Worm worm, int step, int run)
            {
                return new GameHandler().GetCommand(worldState, worm, step, run);
            }
        }
    }
}