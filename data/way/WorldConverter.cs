using System.Collections.Generic;
using WormsApplication.entities;

namespace WormsApplication.data.way
{
    public class WorldConverter
    {
        public WorldEntity Convert(World world)
        {
            WorldEntity worldEntity = new();
            List<FoodEntity> foodEntities = new();
            List<WormEntity> wormEntities = new();

            List<Worm> worms = world.GetWorms();
            List<Food> foods = world.GetFoods();

            foreach (var worm in worms)
            {
                Position positionEntity = new()
                {
                    X = worm.GetX(),
                    Y = worm.GetY()
                };
                WormEntity wormEntity =
                    new(worm.GetName(), positionEntity.X, positionEntity.Y, worm.GetVitality());
                wormEntities.Add(wormEntity);
            }

            foreach (var food in foods)
            {
                Position positionEntity = new()
                {
                    X = food.GetX(),
                    Y = food.GetY()
                };
                FoodEntity foodEntity = new()
                {
                    expiresIn = food.GetExpiresIn(),
                    position = positionEntity
                };
                foodEntities.Add(foodEntity);
            }

            worldEntity.food = foodEntities;
            worldEntity.worms = wormEntities;

            return worldEntity;
        }
    }
}