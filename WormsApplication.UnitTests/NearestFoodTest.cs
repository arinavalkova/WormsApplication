﻿using System.Collections.Generic;
using NUnit.Framework;
using WormsApplication.commands.parser;
using WormsApplication.commands.reader;
using WormsApplication.entities;
using WormsApplication.services.generator.food;
using WormsApplication.services.generator.name;
using WormsApplication.services.logger;

namespace WormsApplication.UnitTests
{
    public class NearestFoodTest
    {
        [Test]
        public void NearestFoodTest_WormMoves_WormMovedToNearestFood()
        {
            const int movesToNearestFood = 5;
            var namesGenerator = new NamesGenerator();
            var worm = new Worm(namesGenerator.Generate());
            var firstFoodCoord = new Position {X = 2, Y = 3};
            var secondFoodCoord = new Position {X = -3, Y = -4};
            var foodGenerator = new CustomFoodGenerator(new List<Position> {firstFoodCoord, secondFoodCoord});
            var wayReader = new WayReader(ReadingWays.NearestFood);
            var world = new World(foodGenerator, namesGenerator, new List<Worm> {worm});
            var commandParser = new CommandParser(world, new MockLogger());
            for (var i = 0; i < movesToNearestFood; i++)
                while (commandParser.GetCommand(wayReader.Walk(world, worm)).Invoke(worm) == null)
                    ;
            Assert.AreEqual(new Position {X = worm.Position.X, Y = worm.Position.Y}, firstFoodCoord);
        }
    }
}