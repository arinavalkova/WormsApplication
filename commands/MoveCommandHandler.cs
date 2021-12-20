using WormsApplication.entities;
using WormsApplication.services.logger;

namespace WormsApplication.commands
{
    public class MoveCommandHandler : GeneralCommand, ICommandHandler
    {
        private const int CountOfDecreaseVitality = 1;
        private readonly int _shiftX;
        private readonly int _shiftY;
        private readonly WorldHandler _worldHandler;

        public MoveCommandHandler(WorldHandler worldHandler, ILogger fileLogger, int shiftX, int shiftY) 
            : base(worldHandler, fileLogger, CountOfDecreaseVitality)
        {
            _worldHandler = worldHandler;
            _shiftX = shiftX;
            _shiftY = shiftY;
        }

        public Worm Invoke(Worm worm)
        {
            GeneralStartMove(worm);
            MoveCommands(worm, _shiftX, _shiftY);
            GeneralEndMove(worm);
            return worm;
        }

        private void MoveCommands(Worm worm, int shiftX, int shiftY)
        {
            _worldHandler.IncreaseVitality(worm,  _worldHandler.MoveWorm(worm, shiftX, shiftY));
        }
    }
}