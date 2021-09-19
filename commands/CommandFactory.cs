using WormsApplication.services.logger;

namespace WormsApplication.commands
{
    public class CommandFactory 
    {
        private readonly World _world;
        private readonly ILogger _fileLogger;

        public CommandFactory(World world, ILogger fileLogger)
        {
            _world = world;
            _fileLogger = fileLogger;
        }

        public MoveCommand MoveDownCommand()
        {
            return new MoveCommand(_world, _fileLogger, 0, 1);
        }

        public MoveCommand MoveUpCommand()
        {
            return new MoveCommand(_world, _fileLogger, 0, -1);
        }

        public MoveCommand MoveLeftCommand()
        {
            return new MoveCommand(_world, _fileLogger, -1, 0);
        }

        public MoveCommand MoveRightCommand()
        {
            return new MoveCommand(_world, _fileLogger, 1, 0);
        }

        public GenerateCommand GenerateDownCommand()
        {
            return new GenerateCommand(_world, _fileLogger, 0, 1);
        }

        public GenerateCommand GenerateUpCommand()
        {
            return new GenerateCommand(_world, _fileLogger, 0, -1);
        }

        public GenerateCommand GenerateLeftCommand()
        {
            return new GenerateCommand(_world, _fileLogger, -1, 0);
        }

        public GenerateCommand GenerateRightCommand()
        {
            return new GenerateCommand(_world, _fileLogger, 1, 0);
        }

        public MoveCommand NothingCommand()
        {
            return new MoveCommand(_world, _fileLogger, 0, 0);
        }
    }
}