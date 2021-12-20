using WormsApplication.entities;
using WormsApplication.services.logger;
using WormsApplication.services.world;

namespace WormsApplication.commands
{
    public class GeneralCommand
    {
        private readonly ILogger _fileLogger;
        private readonly WorldHandler _worldHandler;
        private readonly int _countOfDecreaseVitality;

        protected GeneralCommand(WorldHandler worldHandler, ILogger fileLogger, int countOfDecreaseVitality)
        {
            _fileLogger = fileLogger;
            _worldHandler = worldHandler;
            _countOfDecreaseVitality = countOfDecreaseVitality;
        }

        protected void GeneralEndMove(Worm worm)
        {
            _worldHandler.DecreaseVitality(worm, _countOfDecreaseVitality);
            _fileLogger.Log(_worldHandler);
        }

        protected void GeneralStartMove(Worm worm)
        {
            _worldHandler.IncreaseVitality(worm, _worldHandler.EatFoodOnWormIfCan(worm));
        }
    }
}