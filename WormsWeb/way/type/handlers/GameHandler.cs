using System;
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
        private const int HardGenerateStep = 95;

        public Command GetCommand(WorldState worldState, Worm worm, int step, int run)
        {
            try
            {
                if (worldState.Food == null || worldState.Worms == null)
                    return new Command {Direction = null, Split = false};
                if (worldState.Worms.Count == 1 && worm.LifeStrength < 50)
                {
                    var minLifeStraight = int.MaxValue;
                    Food? nearestFood1 = null;
                    foreach (var food in worldState.Food)
                    {
                        var currentLifeStraight = GetLifeStraightCountForGettingFood(worm, food);
                        if (currentLifeStraight != null && currentLifeStraight < minLifeStraight)
                        {
                            minLifeStraight = (int) currentLifeStraight;
                            nearestFood1 = food;
                        }
                    }

                    if (nearestFood1 != null)
                        return FindCommandForWalkToCell(worm, nearestFood1.Position.X, nearestFood1.Position.Y);
                    return FindCommandForWalkToCell(worm, 0, 0);
                }

                var markedFood = GetMarkedFood(worldState.Food, worldState.Worms);
                var foods = worldState.Food;

                if (step > HardGenerateStep)
                {
                    Command? commandToHardGenerate = HardGenerate(worm, foods, worldState.Worms);
                    if (commandToHardGenerate != null) return commandToHardGenerate;
                }

                Food? nearestFood = null;
                if (markedFood.ContainsKey(worm))
                {
                    nearestFood = markedFood[worm];
                    if (worm.LifeStrength >
                        DistanceBetweenCells(nearestFood.Position.X, worm.Position.X, nearestFood.Position.Y,
                            worm.Position.Y) + MinVitalityToGenerate + 1)
                    {
                        var command = FindGenerateCommand(worm, foods, worldState.Worms);
                        if (command != null) return command;
                    }
                }

                if (nearestFood != null)
                {
                    Command command = FindCommandForWalkToCell(worm, nearestFood.Position.X, nearestFood.Position.Y);
                    return command;
                }

                Command[] commands =
                {
                    new() {Direction = Direction.LEFT, Split = false},
                    new() {Direction = Direction.RIGHT, Split = true}
                };

                if (step > HardGenerateStep)
                {
                    return commands[new Random().Next(0, 1)];
                }

                return FindCommandForWalkToCell(worm, 0, 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return FindCommandForWalkToCell(worm, 0, 0);
            }
        }

        private Command? HardGenerate(Worm worm, List<Food> foods, List<Worm> worms)
        {
            List<Worm> generatedWorms = new()
            {
                new Worm("", worm.Position.X, worm.Position.Y + 1),
                new Worm("", worm.Position.X, worm.Position.Y - 1),
                new Worm("", worm.Position.X - 1, worm.Position.Y),
                new Worm("", worm.Position.X + 1, worm.Position.Y)
            };
            foreach (var generatedWorm in generatedWorms)
            {
                var badGeneratedWorm = false;
                foreach (var currentWorm in worms)
                {
                    if (currentWorm.Position.X == generatedWorm.Position.X &&
                        currentWorm.Position.Y == generatedWorm.Position.Y)
                    {
                        badGeneratedWorm = true;
                        break;
                    }
                }

                foreach (var currentFood in foods)
                {
                    if (currentFood.Position.X == generatedWorm.Position.X &&
                        currentFood.Position.Y == generatedWorm.Position.Y)
                    {
                        badGeneratedWorm = true;
                        break;
                    }
                }

                if (!badGeneratedWorm)
                {
                    Command command =
                        FindCommandForWalkToCell(worm, generatedWorm.Position.X, generatedWorm.Position.Y);
                    return _moveToGenerateCommand[command];
                }
            }

            return null;
        }

        Dictionary<Worm, Food> GetMarkedFood(List<Food> foods, List<Worm> worms)
        {
            var markedDictionary = new Dictionary<Worm, Food>();
            var currentWorms = new List<Worm>(worms);
            foreach (var food in foods)
            {
                var minStraightCount = int.MaxValue;
                Worm? bestWorm = null;
                foreach (var worm in currentWorms)
                {
                    var currentLifeStraight = GetLifeStraightCountForGettingFood(worm, food);
                    if (currentLifeStraight != null && currentLifeStraight < minStraightCount)
                    {
                        minStraightCount = (int) currentLifeStraight;
                        bestWorm = worm;
                    }
                }

                if (bestWorm != null)
                {
                    markedDictionary.Add(bestWorm,
                        new Food(food.ExpiresIn, new Position(food.Position.X, food.Position.Y)));
                    currentWorms.Remove(bestWorm);
                }
            }

            return markedDictionary;
        }

        private int? GetLifeStraightCountForGettingFood(Worm worm, Food food)
        {
            if (Math.Abs(food.Position.X) > 9 || Math.Abs(food.Position.Y) > 9) return null;
            var distance = DistanceBetweenCells(worm.Position.X, food.Position.X, worm.Position.Y, food.Position.Y);
            if (worm.LifeStrength - distance > 0 && food.ExpiresIn - distance > 0)
                return distance;
            return null;
        }

        private Command? FindGenerateCommand(Worm worm, List<Food> foods, List<Worm> worms)
        {
            if (worm.LifeStrength < 45) return null;
            if (foods.Count / 2 < worms.Count) return null;
            
            List<Worm> generatedWorms = new()
            {
                new Worm("", worm.Position.X, worm.Position.Y + 1),
                new Worm("", worm.Position.X, worm.Position.Y - 1),
                new Worm("", worm.Position.X - 1, worm.Position.Y),
                new Worm("", worm.Position.X + 1, worm.Position.Y)
            };
            Dictionary<Food, Worm> nearestFoodsWithWorms = new();

            for (var i = 0; i < 4; i++)
            {
                List<Worm> newWorms = new(worms) {generatedWorms[i]};
                var markedDictionary = GetMarkedFood(new List<Food>(foods), newWorms);
                if (markedDictionary.ContainsKey(generatedWorms[i]))
                {
                    nearestFoodsWithWorms.Add(markedDictionary[generatedWorms[i]], generatedWorms[i]);
                }
            }

            if (nearestFoodsWithWorms.Count == 0) return null;

            var minDistance = int.MaxValue;
            Food? nearestFood = null;
            foreach (var (key, value) in nearestFoodsWithWorms)
            {
                var distance = DistanceBetweenCells(value.Position.X, key.Position.X, value.Position.Y, key.Position.Y);
                if (distance < minDistance)
                {
                    nearestFood = key;
                    minDistance = distance;
                }
            }

            Command command = FindCommandForWalkToCell(worm, nearestFood!.Position.X, nearestFood.Position.Y);
            if (IsNotBadCommand(worm, foods, command, worms))
                return _moveToGenerateCommand[command];
            return null;
        }

        private bool IsNotBadCommand(Worm worm, List<Food> foods, Command command, List<Worm> worms)
        {
            Food predictedFood = CalculateCell(worm, command);
            if (IsFindPredictedFood(predictedFood, foods)) return false;
            if (IsFindWorm(predictedFood, worms)) return false;
            return true;
        }

        private bool IsFindWorm(Food predictedFood, List<Worm> worms)
        {
            foreach (var worm in worms)
            {
                if (worm.Position.X == predictedFood.Position.X && worm.Position.Y == predictedFood.Position.Y)
                    return true;
            }

            return false;
        }

        private bool IsFindPredictedFood(Food predictedFood, List<Food> foods)
        {
            foreach (var food in foods)
            {
                if (food.Position.X == predictedFood.Position.X && food.Position.Y == predictedFood.Position.Y)
                    return true;
            }

            return false;
        }

        private Food? CalculateCell(Worm worm, Command command)
        {
            if (command.Direction == Direction.UP)
                return new Food(worm.Position.X, worm.Position.Y + 1);
            if (command.Direction == Direction.DOWN)
                return new Food(worm.Position.X, worm.Position.Y - 1);
            if (command.Direction == Direction.LEFT)
                return new Food(worm.Position.X - 1, worm.Position.Y);
            if (command.Direction == Direction.RIGHT)
                return new Food(worm.Position.X + 1, worm.Position.Y);
            return null;
        }

        private readonly Dictionary<Command, Command> _moveToGenerateCommand = new()
        {
            [new Command {Direction = Direction.LEFT, Split = false}] =
                new Command {Direction = Direction.LEFT, Split = true},
            [new Command {Direction = Direction.RIGHT, Split = false}] =
                new Command {Direction = Direction.RIGHT, Split = true},
            [new Command {Direction = Direction.UP, Split = false}] =
                new Command {Direction = Direction.UP, Split = true},
            [new Command {Direction = Direction.DOWN, Split = false}] =
                new Command {Direction = Direction.DOWN, Split = true}
        };
    }
}