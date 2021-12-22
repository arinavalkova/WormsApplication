using System;
using EntitiesLibrary;
using EntitiesLibrary.entities.commands;
using WormsApplication.entities;

namespace WormsWeb.way.type.handlers
{
    public class WayHandler
    {
        private protected int DistanceBetweenCells(int aX, int bX, int aY, int bY)
        {
            return Math.Abs(aX - bX) + Math.Abs(aY - bY);
        }

        private protected Command FindCommandForWalkToCell(Worm worm, int x, int y)
        {
            if (x < worm.Position.X)
                return new Command {Direction = Direction.LEFT, Split = false};
            if (x > worm.Position.X)
                return new Command {Direction = Direction.RIGHT, Split = false};
            if (y < worm.Position.Y)
                return new Command {Direction = Direction.DOWN, Split = false};
            if (y > worm.Position.Y)
                return new Command {Direction = Direction.UP, Split = false};
            return new Command {Direction = Direction.DOWN, Split = false};
        }
    }
}