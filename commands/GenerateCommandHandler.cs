using WormsApplication.entities;
using WormsApplication.services.logger;
using WormsApplication.services.world;

namespace WormsApplication.commands
{
    public class GenerateCommandHandler : GeneralCommand, ICommandHandler
    {
        private const int CountOfDecreaseVitality = 10;
        private readonly int _shiftX;
        private readonly int _shiftY;
        private readonly WorldHandler _worldHandler;

        public GenerateCommandHandler(WorldHandler worldHandler, ILogger fileLogger, int shiftX, int shiftY)
            : base(worldHandler, fileLogger, CountOfDecreaseVitality)
        {
            _worldHandler = worldHandler;
            _shiftX = shiftX;
            _shiftY = shiftY;
        }

        public Worm Invoke(Worm worm)
        {
            GeneralStartMove(worm);
            var wormAfterMove = GenerateCommands(worm, _shiftX, _shiftY);
            GeneralEndMove(worm);
            return wormAfterMove;
        }

        private Worm GenerateCommands(Worm worm, int shiftX, int shiftY)
        {
            return _worldHandler.GenerateNewWorm(worm, shiftX, shiftY);
        }
    }
}