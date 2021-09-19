using System.Collections.Generic;
using NUnit.Framework;
using WormsApplication.commands;
using WormsApplication.services.generator.food;
using WormsApplication.services.generator.name;
using WormsApplication.services.logger;

namespace WormsApplication.UnitTests
{
    public class GenerateTest
    {
        [Test]
        public void GenerateCommandTest_LuckyTry_AnotherWorm()
        {
            var namesGenerator = new NamesGenerator();
            var worm = new Worm(namesGenerator.Generate(), 0, 0, 100);
            var world = new World(new MockFoodGenerator(), namesGenerator, new List<Worm> {worm});
            new CommandFactory(world, new MockLogger()).GenerateDownCommand().Invoke(1);
            var generatedWorm = world.GetWormById(2);
            Assert.AreEqual(new Coord {X = generatedWorm.GetX(), Y = generatedWorm.GetY()},
                new Coord {X = 0, Y = 1}
            );
        }

        [Test]
        public void GenerateCommandTest_NotEnoughVitality_NoAnotherWorm()
        {
            var namesGenerator = new NamesGenerator();
            var worm = new Worm(namesGenerator.Generate(), 0, 0, 5);
            var world = new World(new MockFoodGenerator(), namesGenerator, new List<Worm> {worm});
            new CommandFactory(world, new MockLogger()).GenerateDownCommand().Invoke(1);
            Assert.AreEqual(world.GetWormsIds().Count, 1);
        }

        [Test]
        public void GenerateCommandTest_WormCell_NoAnotherWorm()
        {
            var namesGenerator = new NamesGenerator();
            var firstWorm = new Worm(namesGenerator.Generate(), 0, 0, 100);
            var secondWorm = new Worm(namesGenerator.Generate(), 0, 1, 100);
            var world = new World(new MockFoodGenerator(), namesGenerator, new List<Worm> {firstWorm, secondWorm});
            new CommandFactory(world, new MockLogger()).GenerateDownCommand().Invoke(1);
            Assert.AreEqual(world.GetWormsIds().Count, 2);
        }
    }
}