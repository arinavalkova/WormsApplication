using WormsApplication.services.world;

namespace WormsApplication.services.logger
{
    public interface ILogger
    {
        public void Log(WorldHandler worldHandler);
    }
}