using System.IO;
using WormsApplication.services.world;

namespace WormsApplication.services.logger
{
    public class FileLogger : ILogger
    {
        private const string FilePath = "logFile.txt";

        public FileLogger()
        {
            if (File.Exists(FilePath)) File.Delete(FilePath);
        }

        public void Log(WorldHandler worldHandler)
        {
            var worms = worldHandler.GetWorms();
            using (var streamWriter = new StreamWriter(FilePath, true))
            {
                streamWriter.Write($"{worldHandler.GetWorms().Count}  Worms:[");
                foreach (var worm in worms)
                {
                    streamWriter.Write($"{worm.Name}-{worm.LifeStrength}({worm.Position.X},{worm.Position.Y})");
                }

                var foods = worldHandler.GetFoods();
                streamWriter.Write($"], Food:[");
                foreach (var food in foods)
                {
                    streamWriter.Write($"({food.Position.X},{food.Position.Y})");
                }

                streamWriter.WriteLine($"]");
            }
        }
    }
}