using EntitiesLibrary.entities;
using WormsApplication.entities;

namespace WormsApplication.services.generator.food
{
    public interface IFoodGenerator
    {
        public Food? Generate();
    }
}