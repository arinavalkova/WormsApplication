using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WormsApplication.behavior;
using WormsApplication.commands.reader;
using WormsApplication.data;
using WormsApplication.data.behavior;
using WormsApplication.services.generator.food;
using WormsApplication.services.generator.name;
using WormsApplication.services.logger;

namespace WormsApplication
{
    class Program
    {
        private static string _connectionString =
            @"Server=localhost\SQLEXPRESS;Database=wormsDB;Trusted_Connection=True;";
        private static readonly SqlServerBehaviorContext ServerBehaviorContext = new(_connectionString);
        private const string BehaviorName = "first";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //var generateWorldBehavior = new GenerateWorldBehavior(new FoodGenerator(new Random()), ServerBehaviorContext);
            //generateWorldBehavior.Generate("first", 100);
        }
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<WorldSimulatorService>();
                    services.AddScoped<IFoodGenerator, FoodGetter>(
                        _ => new FoodGetter(new WorldBehaviorGetter(ServerBehaviorContext).Get(BehaviorName)));
                    services.AddScoped<NamesGenerator>();
                    services.AddScoped(_ => new WayReader(ReadingWays.Game));
                    services.AddScoped<ILogger,FileLogger>();
                });
        }
    } 
}