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

        protected bool GeneralEndMove(int wormId)
        {
            _world.DecreaseVitality(wormId,_countOfDecreaseVitality);
            _fileLogger.Log(_world);
            return true;
        }

        protected void GeneralStartMove(int wormId)
        {
            _world.IncreaseVitality(wormId,  _world.EatFoodOnWormIfCan(wormId));
        }
    }
}