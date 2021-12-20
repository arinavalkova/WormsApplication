using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WormsApplication.data;
using WormsApplication.services.generator.food;
using WormsApplication.services.generator.name;
using WormsApplication.services.logger;

namespace WormsApplication
{
    class Program
    {
        private const string ConnectionString =
            @"Host=localhost;Port=5432;Database=wormsDB;Username=postgres;Password=westa";
        private const string BehaviorName = "first";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //var generateWorldBehavior = new WorldBehaviorSaver(new FoodGenerator(new Random()), ServerBehaviorContext);
            //generateWorldBehavior.Generate(BehaviorName, 101);
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext<SqlServerBehaviorContext>(
                        options => options.UseNpgsql(ConnectionString));
                    services.AddHostedService<WorldSimulatorService>();
                    services.AddSingleton<IFoodGenerator, FoodGetter>(
                        provider => new FoodGetter(BehaviorName, provider.GetService<SqlServerBehaviorContext>()!));
                    services.AddSingleton<NamesGenerator>();
                    //services.AddSingleton(_ => new WayReader(ReadingWays.Game));
                    services.AddSingleton<ILogger, FileLogger>();
                });
        }
    }
}