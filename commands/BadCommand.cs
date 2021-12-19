using System;
using WormsApplication.entities;

namespace WormsApplication.commands
{
    public class BadCommand : ICommand
    {
        public Worm? Invoke(Worm worm)
        {
            Console.WriteLine($"Bad command for worm {worm.Name}. Try again!");
            return null;
        }
    }
}