using System.Collections.Generic;
using System.Text;
using EntitiesLibrary.entities;
using WormsApplication.services.generator.food;

namespace WormsApplication.data.behavior
{
    public class WorldBehaviorGenerator
    {
        private readonly FoodGenerator _foodGenerator;

        public WorldBehaviorGenerator(FoodGenerator foodGenerator)
        {
            _foodGenerator = foodGenerator;
        }

        public List<Position> Generate(int countOfMoves)
        {
            var resultListOfCoord = new List<Position>();
            var resultListOfFoods = new List<Food>();
            for (var i = 0; i < countOfMoves; i++)
            {
                foreach (var food in resultListOfFoods) food.DecreaseExpiresIn();
                while (true)
                {
                    var food = _foodGenerator.Generate();
                    var lastFood = FindLastFood(food, resultListOfFoods);
                    if (lastFood != null && lastFood.ExpiresIn == 0) continue;
                    resultListOfFoods.Add(food);
                    break;
                }
            }

            foreach (var food in resultListOfFoods)
                resultListOfCoord.Add(new Position {X = food.Position.X, Y = food.Position.Y});
            return resultListOfCoord;
        }

        private Food? FindLastFood(Food food, List<Food> list)
        {
            Food? answerFood = null;
            foreach (var currentFood in list)
                if (currentFood.Position.X == food.Position.X && currentFood.Position.Y == food.Position.Y)
                    answerFood = currentFood;
            return answerFood;
        }

        public string CoordsToString(List<Position> coords)
        {
            var stringBuilder = new StringBuilder();
            foreach (var coord in coords)
            {
                if (stringBuilder.Length != 0)
                    stringBuilder.Append(' ');
                stringBuilder.Append($"{coord.X},{coord.Y}");
            }

            return stringBuilder.ToString();
        }
    }
}