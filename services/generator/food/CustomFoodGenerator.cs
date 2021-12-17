using System.Collections.Generic;

namespace WormsApplication.services.generator.food
{
    public class CustomFoodGenerator : IFoodGenerator
    {
        private readonly List<Coord> _listOfCoord;
        private int _currentPosition = 0;

        public CustomFoodGenerator(List<Coord> listOfCoord)
        {
            _listOfCoord = listOfCoord;
        }

        public Food Generate()
        {
            if (_currentPosition == _listOfCoord.Count) return null;
            var newFood = new Food(_listOfCoord[_currentPosition].X, _listOfCoord[_currentPosition].Y);
            _currentPosition++;
            return newFood;
        }

        public void AddContext()
        {
        }
    }
}