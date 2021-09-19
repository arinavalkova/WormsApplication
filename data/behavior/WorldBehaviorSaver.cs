using Microsoft.EntityFrameworkCore;
using WormsApplication.behavior;
using WormsApplication.data.behavior.entity;
using WormsApplication.services.generator.food;

namespace WormsApplication.data.behavior
{
    public class GenerateWorldBehavior
    {
        private readonly WorldBehaviorGenerator _worldBehaviorGenerator;
        private readonly SqlServerBehaviorContext _behaviorContext;

        public GenerateWorldBehavior(FoodGenerator foodGenerator, SqlServerBehaviorContext behaviorContext)
        {
            _behaviorContext = behaviorContext;
            _worldBehaviorGenerator = new WorldBehaviorGenerator(foodGenerator);
        }
        public void Generate(string name, int countOfMoves)
        {
            var listOfCoords = _worldBehaviorGenerator.Generate(countOfMoves);
            var coordsLine = _worldBehaviorGenerator.CoordsToString(listOfCoords);
            _behaviorContext.Behaviors.Add(new Behaviors {Name = name, CoordsLine = coordsLine});
            _behaviorContext.SaveChanges();
        }
    }
}