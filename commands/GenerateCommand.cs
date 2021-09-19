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
        public bool Invoke(int id)
        {
            GeneralStartMove(id);
            GenerateCommands(id, _shiftX, _shiftY);
            return GeneralEndMove(id);
        }

        private void GenerateCommands(int wormId, int shiftX, int shiftY)
        {
            _world.GenerateNewWorm(wormId, shiftX, shiftY);
        }
    }
}