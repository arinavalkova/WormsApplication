using System.Collections.Generic;
using WormsApplication.data.way.entities;

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
                PositionEntity positionEntity = new()
                {
                    x = worm.GetX(),
                    y = worm.GetY()
                };
                WormEntity wormEntity = new()
                {
                    name = worm.GetName(),
                    position = positionEntity,
                    lifeStrength = worm.GetVitality()
                };
                wormEntities.Add(wormEntity);
            }

            foreach (var food in foods)
            {
                PositionEntity positionEntity = new()
                {
                    x = food.GetX(),
                    y = food.GetY()
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