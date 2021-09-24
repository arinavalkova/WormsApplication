using System;
using System.Collections.Generic;
using System.Linq;
using WormsApplication.commands.parser;
using WormsApplication.services.generator.food;
using WormsApplication.services.generator.name;

namespace WormsApplication
{
    public class World
    {
        private int _currentId = 1;
        private const int FoodVitality = 10;
        private const int NoVitality = 0;
        private const int MaxFoodAge = 10;
        private const int MinVitalityToGenerate = 10;

        private readonly NamesGenerator _namesGenerator;
        private readonly IFoodGenerator _foodGenerator;

        private readonly Dictionary<int, Worm> _worms;
        private readonly List<Food> _foodList;

        public World(IFoodGenerator foodGenerator, NamesGenerator namesGenerator, List<Worm> worms)
        {
            _namesGenerator = namesGenerator;
            _foodList = new List<Food>();
            _foodGenerator = foodGenerator;
            var food = _foodGenerator.Generate();
            if (food != null) _foodList.Add(food);
            _worms = new Dictionary<int, Worm>();
            foreach (var worm in worms) _worms.Add(_currentId++, worm);
        }

        public List<int> GetWormsIds()
        {
            return new List<int>(_worms.Keys);
        }

        public Worm GetWormById(int id)
        {
            return _worms[id];
        }

        public List<Worm> GetWorms()
        {
            return new List<Worm>(_worms.Values);
        }

        private bool IsCellNotWorm(int x, int y)
        {
            var worms = GetWorms();
            return worms.All(worm => worm.GetX() != x || worm.GetY() != y);
        }

        public int MoveWorm(int id, int shiftX, int shiftY)
        {
            var worm = GetWormById(id);
            var x = shiftX + worm.GetX();
            var y = shiftY + worm.GetY();
            //Console.WriteLine($"{x} {y}");
            if (!IsCellNotWorm(x, y)) return NoVitality;
            worm.SetX(x);
            worm.SetY(y);
            return EatFoodIfWormCan(x, y) ? FoodVitality : NoVitality;
        }

        public void DecreaseVitality(int id, int countOfDecrease)
        {
            var worm = GetWormById(id);
            if (worm.GetVitality() <= 0) DeleteWormById(id);
            else worm.DecreaseVitality(countOfDecrease);
        }

        private void DeleteWormById(int id)
        {
            _worms.Remove(id);
        }

        private bool EatFoodIfWormCan(int x, int y)
        {
            var food = IsFoodCell(x, y);
            if (food == null) return false;
            _foodList.Remove(food);
            return true;
        }

        private Food IsFoodCell(int x, int y)
        {
            foreach (var food in _foodList.Where(food => food.GetX() == x && food.GetY() == y)) return food;
            return null;
        }

        public void IncreaseVitality(int id, int countOfVitality)
        {
            GetWormById(id).IncreaseVitality(countOfVitality);
        }

        public void GenerateFood()
        {
            IncreaseFoodAge();
            DeleteNotFreshFood();
            AddNewFood(GetNewFood());
        }

        public Food GetNewFood()
        {
            Food newFood = null;
            var isEmpty = false;

            while (!isEmpty)
            {
                newFood = _foodGenerator.Generate();
                if (newFood == null) return null;
                isEmpty = true;
                foreach (var food in _foodList)
                {
                    if (food.GetX() != newFood.GetX() || food.GetY() != newFood.GetY()) continue;
                    isEmpty = false;
                    break;
                }
            }
            return newFood;
        }

        public void AddNewFood(Food newFood)
        {
            if (newFood != null) _foodList.Add(newFood);
        }

        private void DeleteNotFreshFood()
        {
            foreach (var food in _foodList)
            {
                if (food.GetAge() == MaxFoodAge)
                {
                    _foodList.Remove(food);
                    break;
                }
            }
        }
        private void IncreaseFoodAge()
        {
            foreach (var food in _foodList) food.IncreaseAge();
        }

        public List<Food> GetFoods()
        {
            return _foodList;
        }

        public void GenerateNewWorm(int id, int shiftX, int shiftY)
        {
            var worm = GetWormById(id);
            var newWormX = worm.GetX() + shiftX;
            var newWormY = worm.GetY() + shiftY;
            if (worm.GetVitality() <= MinVitalityToGenerate) return;
            if (!IsCellNotWorm(newWormX, newWormY)) return;
            if (IsFoodCell(newWormX, newWormY) != null) return;
            CreateNewWorm(newWormX, newWormY);
        }

        private void CreateNewWorm(int x, int y)
        {
            _worms.Add(_currentId++, new Worm(_namesGenerator.Generate(), x, y));
        }

        public int EatFoodOnWormIfCan(int id)
        {
            var worm = GetWormById(id);
            foreach (var food in _foodList)
                if (food.GetX() == worm.GetX() && food.GetY() == worm.GetY())
                    return FoodVitality;
            return NoVitality;
        }

        private int DistanceBetweenCells(int aX, int bX, int aY, int bY)
        {
            return Math.Abs(aX - bX) + Math.Abs(aY - bY);
        }

        public Commands FindCommandToMoveToNearestFood(int id)
        {
            var worm = GetWormById(id);
            var minDistance = int.MaxValue;
            Food nearestFood = null;
            var foods = GetFoods();
            var vitality = worm.GetVitality();
            foreach (var food in foods)
            {
                var newDistance = DistanceBetweenCells(worm.GetX(), food.GetX(), worm.GetY(), food.GetY());
                if (!(newDistance < minDistance) || 
                    vitality > minDistance && food.GetAge() + minDistance <= MaxFoodAge) continue;
                minDistance = newDistance;
                nearestFood = food;
            }
            return nearestFood != null ? 
                FindCommandForWalkToCell(worm, nearestFood.GetX(), nearestFood.GetY()) 
                : 
                FindCommandForWalkToCell(worm, 0, 0);
        }

        private Commands FindCommandForWalkToCell(Worm worm, int x, int y)
        {
            if (x != worm.GetX() && x < worm.GetX()) return Commands.MoveLeft;
            if (x != worm.GetX() && x > worm.GetX()) return Commands.MoveRight;
            if (y != worm.GetY() && y > worm.GetY()) return Commands.MoveDown;
            if (y != worm.GetY() && y < worm.GetY()) return Commands.MoveUp;
            return Commands.Nothing;
        }
        public Commands StartGame(int wormId)
        {
            var worm = GetWormById(wormId);
            var minDistance = int.MaxValue;
            Food nearestFood = null;
            var foods = new List<Food>(GetFoods());
            var vitality = worm.GetVitality();
            foreach (var food in foods)
            {
                var newDistance = DistanceBetweenCells(worm.GetX(), food.GetX(), worm.GetY(), food.GetY());
                if (!(newDistance < minDistance) || 
                    vitality > minDistance && food.GetAge() + minDistance <= MaxFoodAge) continue;
                minDistance = newDistance;
                nearestFood = food;
            }
            if (worm.GetVitality() > minDistance + MinVitalityToGenerate)
            {
                foods.Remove(nearestFood);
                return FindGenerateCommand(wormId, foods);
            }
            return nearestFood != null ? 
                FindCommandForWalkToCell(worm, nearestFood.GetX(), nearestFood.GetY()) 
                : 
                FindCommandForWalkToCell(worm, 0, 0);
        }
        private Commands FindGenerateCommand(int wormId, List<Food> foods)
        {
            var worm = GetWormById(wormId);
            var minDistance = int.MaxValue;
            Food nearestFood = null;
            var vitality = worm.GetVitality();
            foreach (var food in foods)
            {
                var newDistance = DistanceBetweenCells(worm.GetX(), food.GetX(), worm.GetY(), food.GetY());
                if (!(newDistance < minDistance) || 
                    vitality > minDistance && food.GetAge() + minDistance <= MaxFoodAge) continue;
                minDistance = newDistance;
                nearestFood = food;
            }
            return _moveToGenerateCommand[
                nearestFood != null ? 
                    FindCommandForWalkToCell(worm, nearestFood.GetX(), nearestFood.GetY())
                    : 
                    FindCommandForWalkToCell(worm, 0, 0)
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