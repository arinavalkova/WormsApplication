using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WormsApplication.behavior;
using WormsApplication.commands.reader;
using WormsApplication.data.behavior;
using WormsApplication.services.generator.food;
using WormsApplication.services.generator.name;
using WormsApplication.services.logger;

namespace WormsApplication
{
    class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var generateWorldBehavior = new GenerateWorldBehavior(new FoodGenerator(new Random()));
            generateWorldBehavior.Generate("firstBehavior", 100);
        }
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<WorldSimulatorService>();
                    services.AddScoped<IFoodGenerator, FoodGenerator>(_ => new FoodGenerator(new Random()));
                    services.AddScoped<NamesGenerator>();
                    services.AddScoped(_ => new WayReader(ReadingWays.Game));
                    services.AddScoped<ILogger,FileLogger>();
                });
        }
    } 
}