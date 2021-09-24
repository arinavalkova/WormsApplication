using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WormsApplication.behavior;
using WormsApplication.data.behavior.entity;
using WormsApplication.services.generator.food;

namespace WormsApplication.data.behavior
{
    public class WorldBehaviorSaver
    {
        private readonly WorldBehaviorGenerator _worldBehaviorGenerator;
        private readonly SqlServerBehaviorContext _behaviorContext;

        public WorldBehaviorSaver(FoodGenerator foodGenerator, SqlServerBehaviorContext behaviorContext)
        {
            _behaviorContext = behaviorContext;
            _worldBehaviorGenerator = new WorldBehaviorGenerator(foodGenerator);
        }

        public void Generate(string name, int countOfMoves)
        {
            var listOfCoords = _worldBehaviorGenerator.Generate(countOfMoves);
            var entityEntry = _behaviorContext.Behaviors.Add(new Behaviors {Name = name});
            _behaviorContext.SaveChanges();
            for (var i = 0; i < countOfMoves; i++)
            {
                _behaviorContext.Coords.Add(new Coords
                    {BehaviorId = entityEntry.Entity.Id, Move = i, X = listOfCoords[i].X, Y = listOfCoords[i].Y}
                );
            }
            _behaviorContext.SaveChanges();
        }
    }
}