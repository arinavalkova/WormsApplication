using WormsApplication.services.generator.food;

namespace WormsApplication.behavior
{
    public class GenerateWorldBehavior
    {
        private WorldBehaviorGenerator _worldBehaviorGenerator;
        
        public GenerateWorldBehavior(FoodGenerator foodGenerator)
        {
            _worldBehaviorGenerator = new WorldBehaviorGenerator(foodGenerator);
        }

        public void Generate(string name, int countOfMoves)
        {
            var listOfCoords = _worldBehaviorGenerator.Generate(countOfMoves);
            
        }
    }
}