using System.Collections.Generic;

namespace WormsApplication.services.generator.food
{
    public class FoodGetter : IFoodGenerator
    {
        private readonly List<Coord> _listCoords;

        public FoodGetter(List<Coord> listCoords)
        {
            _listCoords = listCoords;
        }

        public Food Generate()
        {
            var coord = _listCoords[0];
            _listCoords.Remove(coord);
            return new Food(coord.X, coord.Y);
        }
    }
}