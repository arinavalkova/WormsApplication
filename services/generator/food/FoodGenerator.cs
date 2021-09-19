using System;

namespace WormsApplication.services.generator.food
{
    public class FoodGenerator : IFoodGenerator
    {
        private readonly Random _random;

        public FoodGenerator(Random random)
        {
            _random = random;
        }
        private int NextNormal(double mu = 0, double sigma = 1)
        {
            var u1 = _random.NextDouble();
            var u2 = _random.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            var randNormal = mu + sigma * randStdNormal;
            return (int) Math.Round(randNormal);
        }

        public Food Generate()
        {
            return new Food(NextNormal(), NextNormal());
        }
    }
}