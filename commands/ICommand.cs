using WormsApplication.entities;

namespace WormsApplication.commands
{
    public interface ICommand
    {
        public Worm? Invoke(Worm worm);
    }
}