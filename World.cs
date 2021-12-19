using System;
using System.Collections.Generic;
using System.Linq;
using EntitiesLibrary.entities;
using WormsApplication.commands.parser;
using WormsApplication.data.way;
using WormsApplication.entities;
using WormsApplication.services.generator.food;
using WormsApplication.services.generator.name;

namespace WormsApplication
{
    public class World
    {
        private const int FoodVitality = 10;
        private const int NoVitality = 0;
        private const int MinVitalityToGenerate = 10;

        private readonly NamesGenerator _namesGenerator;
        private readonly IFoodGenerator _foodGenerator;

        private WorldState _worldState; 

        public World(IFoodGenerator foodGenerator, NamesGenerator namesGenerator, List<Worm> worms)
        {
            _namesGenerator = namesGenerator;
            var foodList = new List<Food>();
            _foodGenerator = foodGenerator;
            var food = _foodGenerator.Generate();
            foodList.Add(food!);
            _worldState = new WorldState
            {
                Food = foodList,
                Worms = worms
            };
        }

        public WorldState GetWorldState()
        {
            return _worldState;
        }

        public List<Worm> GetWorms()
        {
            return _worldState.Worms;
        }

        private bool IsCellNotWorm(int x, int y)
        {
            var worms = GetWorms();
            return worms.All(worm => worm.Position.X != x || worm.Position.Y != y);
        }

        public int MoveWorm(Worm worm, int shiftX, int shiftY)
        {
            var x = shiftX + worm.Position.X;
            var y = shiftY + worm.Position.Y;
            if (!IsCellNotWorm(x, y)) return NoVitality;
            worm.Position.X = x;
            worm.Position.Y = y;
            return EatFoodIfWormCan(x, y) ? FoodVitality : NoVitality;
        }

        public void DecreaseVitality(Worm worm, int countOfDecrease)
        {
            if (worm.LifeStrength <= 0) _worldState.Worms.Remove(worm);
            else worm.DecreaseLifeStrength(countOfDecrease);
        }

        private bool EatFoodIfWormCan(int x, int y)
        {
            var food = IsFoodCell(x, y);
            if (food == null) return false;
            _worldState.Food.Remove(food);
            return true;
        }

        private Food? IsFoodCell(int x, int y)
        {
            foreach (var food in _worldState.Food.Where(food => food.Position.X == x && food.Position.Y == y))
                return food;
            return null;
        }

        public void IncreaseVitality(Worm worm, int countOfVitality)
        {
            worm.IncreaseLifeStrength(countOfVitality);
        }

        public void GenerateFood()
        {
            IncreaseFoodAge();
            DeleteNotFreshFood();
            AddNewFood(GetNewFood());
        }

        private Food? GetNewFood()
        {
            Food? newFood = null;
            var isEmpty = false;

            while (!isEmpty)
            {
                newFood = _foodGenerator.Generate();
                if (newFood == null) return null;
                isEmpty = true;
                var foodList = _worldState.Food;
                foreach (var food in foodList)
                {
                    if (food.Position.X != newFood.Position.X || food.Position.Y != newFood.Position.Y) continue;
                    isEmpty = false;
                    break;
                }
            }

            return newFood;
        }

        private void AddNewFood(Food newFood)
        {
            if (newFood != null) _worldState.Food.Add(newFood);
        }

        private void DeleteNotFreshFood()
        {
            var foodList = _worldState.Food;
            foreach (var food in foodList)
            {
                if (food.ExpiresIn == 0)
                {
                    foodList.Remove(food);
                    break;
                }
            }
        }

        private void IncreaseFoodAge()
        {
            var foodList = _worldState.Food;
            foreach (var food in foodList) food.DecreaseExpiresIn();
        }

        public List<Food> GetFoods()
        {
            return _worldState.Food;
        }

        public Worm GenerateNewWorm(Worm worm, int shiftX, int shiftY)
        {
            var newWormX = worm.Position.X + shiftX;
            var newWormY = worm.Position.Y + shiftY;
            if (worm.LifeStrength <= MinVitalityToGenerate) return worm;
            if (!IsCellNotWorm(newWormX, newWormY)) return worm;
            if (IsFoodCell(newWormX, newWormY) != null) return worm;
            return CreateNewWorm(newWormX, newWormY);
        }

        private Worm CreateNewWorm(int x, int y)
        {
            Worm worm = new(_namesGenerator.Generate(), x, y);
            _worldState.Worms.Add(worm);
            return worm;
        }

        public int EatFoodOnWormIfCan(Worm worm)
        {
            var foodList = _worldState.Food;
            foreach (var food in foodList)
                if (food.Position.X == worm.Position.X && food.Position.Y == worm.Position.Y)
                    return FoodVitality;
            return NoVitality;
        }

        private int DistanceBetweenCells(int aX, int bX, int aY, int bY)
        {
            return Math.Abs(aX - bX) + Math.Abs(aY - bY);
        }

        public Commands FindCommandToMoveToNearestFood(Worm worm)
        {
            var minDistance = int.MaxValue;
            Food? nearestFood = null;
            var foods = GetFoods();
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

            return nearestFood != null
                ? FindCommandForWalkToCell(worm, nearestFood.Position.X, nearestFood.Position.Y)
                : FindCommandForWalkToCell(worm, 0, 0);
        }

        private Commands FindCommandForWalkToCell(Worm worm, int x, int y)
        {
            if (x != worm.Position.X && x < worm.Position.X) return Commands.MoveLeft;
            if (x != worm.Position.X && x > worm.Position.X) return Commands.MoveRight;
            if (y != worm.Position.Y && y > worm.Position.Y) return Commands.MoveDown;
            if (y != worm.Position.Y && y < worm.Position.Y) return Commands.MoveUp;
            return Commands.Nothing;
        }

        public Commands StartGame(Worm worm)
        {
            var minDistance = int.MaxValue;
            Food? nearestFood = null;
            var foods = new List<Food>(GetFoods());
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

        private Commands FindGenerateCommand(Worm worm, List<Food> foods)
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

        private readonly Dictionary<Commands, Commands> _moveToGenerateCommand = new()
        {
            [Commands.MoveLeft] = Commands.GenerateLeft,
            [Commands.MoveRight] = Commands.GenerateRight,
            [Commands.MoveUp] = Commands.GenerateUp,
            [Commands.MoveDown] = Commands.GenerateDown,
            [Commands.Nothing] = Commands.GenerateUp
        };
    }
}