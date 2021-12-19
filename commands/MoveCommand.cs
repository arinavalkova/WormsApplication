using WormsApplication.entities;
using WormsApplication.services.logger;

namespace WormsApplication.commands
{
    public class MoveCommand : GeneralCommand, ICommand
    {
        private const int CountOfDecreaseVitality = 1;
        private readonly int _shiftX;
        private readonly int _shiftY;
        private readonly World _world;

        public MoveCommand(World world, ILogger fileLogger, int shiftX, int shiftY) 
            : base(world, fileLogger, CountOfDecreaseVitality)
        {
            _world = world;
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
            _world.IncreaseVitality(worm,  _world.MoveWorm(worm, shiftX, shiftY));
        }
    }
}