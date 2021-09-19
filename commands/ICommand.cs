namespace WormsApplication.commands
{
    public interface ICommand
    {
        public bool Invoke(int id);
    }
}