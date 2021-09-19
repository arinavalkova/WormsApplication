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

        public bool Invoke(int id)
        {
            GeneralStartMove(id);
            MoveCommands(id, _shiftX, _shiftY);
            return GeneralEndMove(id);
        }

        private void MoveCommands(int id, int shiftX, int shiftY)
        {
            _world.IncreaseVitality(id,  _world.MoveWorm(id, shiftX, shiftY));
        }
    }
}