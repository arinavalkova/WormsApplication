using WormsApplication.entities;

namespace WormsApplication.commands
{
    public interface ICommandHandler
    {
        public Worm? Invoke(Worm worm);
    }
}