using System.Collections.Generic;
using NUnit.Framework;
using WormsApplication.commands.parser;
using WormsApplication.commands.reader;
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
            var firstFoodCoord = new Coord {X = 2, Y = 3};
            var secondFoodCoord = new Coord {X = -3, Y = -4};
            var foodGenerator = new CustomFoodGenerator(new List<Coord> {firstFoodCoord, secondFoodCoord});
            var wayReader = new WayReader(ReadingWays.NearestFood);
            var world = new World(foodGenerator, namesGenerator, new List<Worm> {worm});
            var commandParser = new CommandParser(world, new MockLogger());
            for (var i = 0; i < movesToNearestFood; i++) while (!commandParser.GetCommand(wayReader.Walk(world, 1)).Invoke(1)) ;
            Assert.AreEqual(new Coord{X = worm.GetX(), Y = worm.GetY()}, firstFoodCoord);
        }
    }
}