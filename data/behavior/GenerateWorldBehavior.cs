using Microsoft.EntityFrameworkCore;
using WormsApplication.behavior;
using WormsApplication.data.behavior.entity;
using WormsApplication.services.generator.food;

namespace WormsApplication.data.behavior
{
    public class GenerateWorldBehavior
    {
        private readonly WorldBehaviorGenerator _worldBehaviorGenerator;

        private const string ConnectionString =
            @"Server=localhost\SQLEXPRESS;Database=wormsDB;Trusted_Connection=True;";
        public GenerateWorldBehavior(FoodGenerator foodGenerator)
        {
            _worldBehaviorGenerator = new WorldBehaviorGenerator(foodGenerator);
        }
        public void Generate(string name, int countOfMoves)
        {
            var listOfCoords = _worldBehaviorGenerator.Generate(countOfMoves);
            var coordsLine = _worldBehaviorGenerator.CoordsToString(listOfCoords);
            var options = new DbContextOptionsBuilder<BehaviorContext>()
                .UseSqlServer(ConnectionString)
                .Options;
            using var context = new BehaviorContext(options);
            var behavior = new Behaviors
            {
                Name = name,
                CoordsLine = coordsLine
            };
            context.Behaviors.Add(behavior);
            context.SaveChanges();
        }
    }
}