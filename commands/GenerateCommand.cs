using WormsApplication.entities;
using WormsApplication.services.logger;

namespace WormsApplication.commands
{
    public class GenerateCommand : GeneralCommand, ICommand
    {
        private const int CountOfDecreaseVitality = 10;
        private readonly int _shiftX;
        private readonly int _shiftY;
        private readonly World _world;
        public GenerateCommand(World world, ILogger fileLogger, int shiftX, int shiftY) 
            : base(world, fileLogger, CountOfDecreaseVitality)
        {
            _world = world;
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
            return _world.GenerateNewWorm(worm, shiftX, shiftY);
        }
    }
}