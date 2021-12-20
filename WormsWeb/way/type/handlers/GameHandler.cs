using System.Collections.Generic;
using EntitiesLibrary;
using EntitiesLibrary.entities;
using EntitiesLibrary.entities.commands;
using WormsApplication.entities;

namespace WormsWeb.way.type.handlers
{
    public class GameHandler : WayHandler, IWayTypeHandler
    {
        private const int MinVitalityToGenerate = 10;

        public Command GetCommand(WorldState worldState, Worm worm)
        {
            var minDistance = int.MaxValue;
            Food? nearestFood = null;
            var foods = new List<Food>(worldState.Food);
            var vitality = worm.LifeStrength;
            foreach (var food in foods)
            {
                var newDistance =
                    DistanceBetweenCells(worm.Position.X, food.Position.X, worm.Position.Y, food.Position.Y);
                if (!(newDistance < minDistance) ||
                    vitality > minDistance && food.ExpiresIn + minDistance > 0) continue;
                minDistance = newDistance;
                nearestFood = food;
            }

            if (worm.LifeStrength > minDistance + MinVitalityToGenerate)
            {
                foods.Remove(nearestFood);
                return FindGenerateCommand(worm, foods);
            }

            return nearestFood != null
                ? FindCommandForWalkToCell(worm, nearestFood.Position.X, nearestFood.Position.Y)
                : FindCommandForWalkToCell(worm, 0, 0);
        }

        private Command FindGenerateCommand(Worm worm, List<Food> foods)
        {
            var minDistance = int.MaxValue;
            Food? nearestFood = null;
            var vitality = worm.LifeStrength;
            foreach (var food in foods)
            {
                var newDistance =
                    DistanceBetweenCells(worm.Position.X, food.Position.X, worm.Position.Y, food.Position.Y);
                if (!(newDistance < minDistance) ||
                    vitality > minDistance && food.ExpiresIn + minDistance > 0) continue;
                minDistance = newDistance;
                nearestFood = food;
            }

            return _moveToGenerateCommand[
                nearestFood != null
                    ? FindCommandForWalkToCell(worm, nearestFood.Position.X, nearestFood.Position.Y)
                    : FindCommandForWalkToCell(worm, 0, 0)
            ];
        }

        private readonly Dictionary<Command, Command> _moveToGenerateCommand = new()
        {
            [new Command {Direction = Direction.Left, Split = false}] =
                new Command {Direction = Direction.Left, Split = true},
            [new Command {Direction = Direction.Right, Split = false}] =
                new Command {Direction = Direction.Right, Split = true},
            [new Command {Direction = Direction.Up, Split = false}] =
                new Command {Direction = Direction.Up, Split = true},
            [new Command {Direction = Direction.Down, Split = false}] =
                new Command {Direction = Direction.Down, Split = true},
            [new Command {Direction = null, Split = null}] = new Command {Direction = Direction.Up, Split = true}
        };
    }
}