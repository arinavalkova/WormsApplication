using System;

namespace WormsApplication.commands
{
    public class BadCommand : ICommand
    {
        public bool Invoke(int id)
        {
            Console.WriteLine($"Bad command for worm id:{id}. Try again!");
            return false;
        }
    }
}