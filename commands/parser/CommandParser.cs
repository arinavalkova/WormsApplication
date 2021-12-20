using System.Collections.Generic;
using EntitiesLibrary.entities.commands;
using WormsApplication.services.logger;

namespace WormsApplication.commands.parser
{
    public class CommandParser
    {
        private readonly Dictionary<Command, ICommandHandler> _dictionary;

        public CommandParser(WorldHandler worldHandler, ILogger fileLogger)
        {
            var commandFactory = new CommandFactory(worldHandler, fileLogger);
            _dictionary = new Dictionary<Command, ICommandHandler>
            {
                [new Command {Direction = Direction.Up, Split = false}] = commandFactory.MoveUpCommand(),
                [new Command {Direction = Direction.Down, Split = false}] = commandFactory.MoveDownCommand(),
                [new Command {Direction = Direction.Left, Split = false}] = commandFactory.MoveLeftCommand(),
                [new Command {Direction = Direction.Right, Split = false}] = commandFactory.MoveRightCommand(),
                [new Command {Direction = null, Split = null}] = commandFactory.NothingCommand(),
                [new Command {Direction = Direction.Up, Split = true}] = commandFactory.GenerateUpCommand(),
                [new Command {Direction = Direction.Down, Split = true}] = commandFactory.GenerateDownCommand(),
                [new Command {Direction = Direction.Left, Split = true}] = commandFactory.GenerateLeftCommand(),
                [new Command {Direction = Direction.Right, Split = true}] = commandFactory.GenerateRightCommand()
            };
        }
        public ICommandHandler Parse(Command command)
        {
            try
            {
                return _dictionary[command];
            }
            catch (KeyNotFoundException)
            {
                return new BadCommandHandler();
            }
        }
    }
}