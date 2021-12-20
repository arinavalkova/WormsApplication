using WormsApplication.services.logger;

namespace WormsApplication.commands
{
    public class CommandFactory 
    {
        private readonly WorldHandler _worldHandler;
        private readonly ILogger _fileLogger;

        public CommandFactory(WorldHandler worldHandler, ILogger fileLogger)
        {
            _worldHandler = worldHandler;
            _fileLogger = fileLogger;
        }

        public MoveCommandHandler MoveDownCommand()
        {
            return new MoveCommandHandler(_worldHandler, _fileLogger, 0, 1);
        }

        public MoveCommandHandler MoveUpCommand()
        {
            return new MoveCommandHandler(_worldHandler, _fileLogger, 0, -1);
        }

        public MoveCommandHandler MoveLeftCommand()
        {
            return new MoveCommandHandler(_worldHandler, _fileLogger, -1, 0);
        }

        public MoveCommandHandler MoveRightCommand()
        {
            return new MoveCommandHandler(_worldHandler, _fileLogger, 1, 0);
        }

        public GenerateCommandHandler GenerateDownCommand()
        {
            return new GenerateCommandHandler(_worldHandler, _fileLogger, 0, 1);
        }

        public GenerateCommandHandler GenerateUpCommand()
        {
            return new GenerateCommandHandler(_worldHandler, _fileLogger, 0, -1);
        }

        public GenerateCommandHandler GenerateLeftCommand()
        {
            return new GenerateCommandHandler(_worldHandler, _fileLogger, -1, 0);
        }

        public GenerateCommandHandler GenerateRightCommand()
        {
            return new GenerateCommandHandler(_worldHandler, _fileLogger, 1, 0);
        }

        public MoveCommandHandler NothingCommand()
        {
            return new MoveCommandHandler(_worldHandler, _fileLogger, 0, 0);
        }
    }
}