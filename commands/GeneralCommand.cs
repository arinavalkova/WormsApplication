using WormsApplication.entities;
using WormsApplication.services.logger;

namespace WormsApplication.commands
{
    public class GeneralCommand 
    {
        private readonly ILogger _fileLogger;
        private readonly World _world;
        private readonly int _countOfDecreaseVitality;
        
        protected GeneralCommand(World world, ILogger fileLogger, int countOfDecreaseVitality)
        {
            _fileLogger = fileLogger;
            _world = world;
            _countOfDecreaseVitality = countOfDecreaseVitality;
        }

        protected void GeneralEndMove(Worm worm)
        {
            _world.DecreaseVitality(worm,_countOfDecreaseVitality);
            _fileLogger.Log(_world);
        }

        protected void GeneralStartMove(Worm worm)
        {
            _world.IncreaseVitality(worm,  _world.EatFoodOnWormIfCan(worm));
        }
    }
}