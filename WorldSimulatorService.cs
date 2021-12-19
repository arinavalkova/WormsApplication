using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WormsApplication.commands.parser;
using WormsApplication.commands.reader;
using WormsApplication.data.way;
using WormsApplication.services.generator.food;
using WormsApplication.services.generator.name;
using WormsApplication.services.logger;

namespace WormsApplication
{
    public class WorldSimulatorService : IHostedService
    {
        private const int NumberOfMoves = 100;
        private const int CenterXCoord = 0;
        private const int CenterYCoord = 0;

        private readonly World _world;
        private readonly CommandParser _commandParser;
        private readonly WayReader _wayReader;
        private readonly IHostApplicationLifetime _appLifetime;

        public WorldSimulatorService(IFoodGenerator foodGenerator,
            NamesGenerator namesGenerator,
            //WayReader wayReader,
            ILogger fileLogger,
            IHostApplicationLifetime appLifetime)
        {
            _world = new World(foodGenerator, namesGenerator,
                new List<Worm> {new(namesGenerator.Generate(), CenterXCoord, CenterYCoord)});
            _commandParser = new CommandParser(_world, fileLogger);
            //_wayReader = wayReader;
            _appLifetime = appLifetime;
        }

        private async Task Start()
        {
            // for (var i = 0; i < NumberOfMoves; i++)
            // {
            //     var currentWormsId = _world.GetWormsIds();
            //     foreach (var id in currentWormsId)
            //     {
            //         while (!_commandParser.GetCommand(_wayReader.Walk(_world, id)).Invoke(id)) ;
            //     }
            //     _world.GenerateFood();
            // }
            await new WayGetter().Get(_world, 1);
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