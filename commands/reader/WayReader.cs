using System.Collections.Generic;
using WormsApplication.commands.parser;

namespace WormsApplication.commands.reader
{
    public class WayReader
    {
        private readonly ReadingWays _readingWay;

        private readonly Dictionary<ReadingWays, IReader> _reader = new()
        {
            [ReadingWays.Circle] = new CircleReader(),
            [ReadingWays.NearestFood] = new NearestFoodReader(),
            [ReadingWays.Game] = new GameReader()
        };

        private static readonly List<Commands> CircleCommandList = new()
        {
            Commands.MoveUp, Commands.MoveRight, 
            Commands.MoveDown, Commands.MoveDown,
            Commands.MoveLeft, Commands.MoveLeft, 
            Commands.MoveUp, Commands.MoveUp, 
            Commands.MoveRight, Commands.MoveDown
        };

        public WayReader(ReadingWays readingWay)
        {
            _readingWay = readingWay;
        }

        private static int _circleCommandListPosition = 0;

        public Commands Walk(World world, int id)
        {
            return _reader[_readingWay].Invoke(world, id);
        }

        private interface IReader
        {
            public Commands Invoke(World world, int id);
        }

        private class CircleReader : IReader
        {
            public Commands Invoke(World world,int id)
            {
                var command = CircleCommandList[_circleCommandListPosition];
                if (_circleCommandListPosition + 1 == CircleCommandList.Count) _circleCommandListPosition = 0;
                else _circleCommandListPosition++;
                return command;
            }
        }

        private class NearestFoodReader : IReader
        {
            public Commands Invoke(World world, int id)
            {
                return world.FindCommandToMoveToNearestFood(id);
            }
        }

        private class GameReader : IReader
        {
            public Commands Invoke(World world, int id)
            {
                return world.StartGame(id);
            }
        }
    }
}