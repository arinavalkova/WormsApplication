using System;
using EntitiesLibrary;
using EntitiesLibrary.entities;
using EntitiesLibrary.entities.commands;
using WormsApplication.entities;

namespace WormsWeb.way.type.handlers
{
    public class NearestFoodHandler : WayHandler, IWayTypeHandler
    {
        public Command GetCommand(WorldState worldState, Worm worm)
        {
            var minDistance = int.MaxValue;
            Food? nearestFood = null;
            var foods = worldState.Food;
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
    }
}