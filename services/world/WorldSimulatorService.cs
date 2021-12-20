using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WormsApplication.commands.parser;
using WormsApplication.entities;
using WormsApplication.services.generator.food;
using WormsApplication.services.generator.name;
using WormsApplication.services.logger;
using WormsApplication.services.way;

namespace WormsApplication.services.world
{
    public class WorldSimulatorService : IHostedService
    {
        private const int NumberOfMoves = 100;
        private const int CenterXCoord = 0;
        private const int CenterYCoord = 0;

        private readonly WorldHandler _worldHandler;
        private readonly CommandParser _commandParser;
        private readonly IHostApplicationLifetime _appLifetime;

        public WorldSimulatorService(IFoodGenerator foodGenerator,
            NamesGenerator namesGenerator,
            ILogger fileLogger,
            IHostApplicationLifetime appLifetime)
        {
            _worldHandler = new WorldHandler(foodGenerator, namesGenerator,
                new List<Worm> {new(namesGenerator.Generate(), CenterXCoord, CenterYCoord)});
            _commandParser = new CommandParser(_worldHandler, fileLogger);
            _appLifetime = appLifetime;
        }

        private async Task Start()
        {
            WayGetter wayGetter = new();
            for (var i = 0; i < NumberOfMoves; i++)
            {
                var currentWormsList = new List<Worm>(_worldHandler.GetWorms());
                foreach (var worm in currentWormsList)
                {
                    while (true)
                    {
                        var way = await wayGetter.Get(_worldHandler.GetWorldState(), worm);
                        if (way == null) Console.WriteLine("Bad command!");
                        if (_commandParser.Parse(way!).Invoke(worm) != null) break;
                    }
                }

                _worldHandler.GenerateFood();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    await Start();
                    _appLifetime.StopApplication();
                }, cancellationToken);
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}