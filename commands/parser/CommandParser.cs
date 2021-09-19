using System.Collections.Generic;
using WormsApplication.services.logger;

namespace WormsApplication.commands.parser
{
    public class CommandParser
    {
        private readonly Dictionary<Commands, ICommand> _dictionary;

        public CommandParser(World world, ILogger fileLogger)
        {
            var commandFactory = new CommandFactory(world, fileLogger);
            _dictionary = new Dictionary<Commands, ICommand>
            {
                [Commands.MoveUp] = commandFactory.MoveUpCommand(),
                [Commands.MoveDown] = commandFactory.MoveDownCommand(),
                [Commands.MoveLeft] = commandFactory.MoveLeftCommand(),
                [Commands.MoveRight] = commandFactory.MoveRightCommand(),
                [Commands.Nothing] = commandFactory.NothingCommand(),
                [Commands.GenerateUp] = commandFactory.GenerateUpCommand(),
                [Commands.GenerateDown] = commandFactory.GenerateDownCommand(),
                [Commands.GenerateLeft] = commandFactory.GenerateLeftCommand(),
                [Commands.GenerateRight] = commandFactory.GenerateRightCommand()
            };
        }

        public ICommand GetCommand(Commands command)
        {
            try
            {
                return _dictionary[command];
            }
            catch (KeyNotFoundException)
            {
                return new BadCommand();
            }
        }
    }
}