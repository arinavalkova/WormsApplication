using System.Collections.Generic;
using EntitiesLibrary.entities;
using WormsApplication.entities;

namespace WormsApplication.services.generator.food
{
    public class CustomFoodGenerator : IFoodGenerator
    {
        private readonly List<Position> _listOfCoord;
        private int _currentPosition = 0;

        public CustomFoodGenerator(List<Position> listOfCoord)
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