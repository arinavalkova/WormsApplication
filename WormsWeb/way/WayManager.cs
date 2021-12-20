using System.Collections.Generic;
using EntitiesLibrary;
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
            //[WayType.Circle] = new CircleTypeHandler(),
            [WayType.NearestFood] = new NearestFoodTypeHandler(),
            [WayType.Game] = new GameTypeHandler(),
        };
        
        private static int _circleCommandListPosition = 0;

        // private static readonly List<Command> CircleCommandList = new()
        // {
        //     Commands.MoveUp, Commands.MoveRight, 
        //     Commands.MoveDown, Commands.MoveDown,
        //     Commands.MoveLeft, Commands.MoveLeft, 
        //     Commands.MoveUp, Commands.MoveUp, 
        //     Commands.MoveRight, Commands.MoveDown
        // };

        public WayManager(WayType wayType)
        {
            _wayType = wayType;
        }

        public Command? GetWayCommand(WorldState worldState, string name)
        {
            Worm? worm = null;
            var wormList = worldState.Worms;
            foreach (var currentWorm in wormList)
            {
                if (currentWorm.Name == name)
                {
                    worm = currentWorm;
                    break;
                }
            }

            if (worm == null) return null;
            return _wayHandler[_wayType].Invoke(worldState, worm);
        }
        
        public Command? GetWayCommand(WorldState worldState, Worm worm)
        {
            return _wayHandler[_wayType].Invoke(worldState, worm);
        }

        private interface ITypeHandler
        {
            public Command Invoke(WorldState worldState, Worm worm);
        }

        // private class CircleTypeHandler : ITypeHandler
        // {
        //     public Command Invoke(WorldState worldState, Worm worm)
        //     {
        //         var command = CircleCommandList[_circleCommandListPosition];
        //         if (_circleCommandListPosition + 1 == CircleCommandList.Count) _circleCommandListPosition = 0;
        //         else _circleCommandListPosition++;
        //         return command;
        //     }
        // }

        private class NearestFoodTypeHandler : ITypeHandler
        {
            public Command Invoke(WorldState worldState, Worm worm)
            {
                return new NearestFoodHandler().GetCommand(worldState, worm);
            }
        }

        private class GameTypeHandler : ITypeHandler
        {
            public Command Invoke(WorldState worldState, Worm worm)
            {
                return new GameHandler().GetCommand(worldState, worm);
            }
        }
    }
}