using System.Collections.Generic;
using NUnit.Framework;
using WormsApplication.commands;
using WormsApplication.entities;
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
            var worm1 = new Worm(namesGenerator.Generate(), 0, 0, 100);
            var world = new World(new MockFoodGenerator(), namesGenerator, new List<Worm> {worm1});
            var worm2 = new CommandFactory(world, new MockLogger()).GenerateDownCommand().Invoke(worm1);
            Assert.NotNull(worm2);
            Assert.AreEqual(new Position {X = worm2.Position.X, Y = worm2.Position.Y},
                new Position {X = 0, Y = 1}
            );
        }

        [Test]
        public void GenerateCommandTest_NotEnoughVitality_NoAnotherWorm()
        {
            var namesGenerator = new NamesGenerator();
            var worm = new Worm(namesGenerator.Generate(), 0, 0, 5);
            var world = new World(new MockFoodGenerator(), namesGenerator, new List<Worm> {worm});
            new CommandFactory(world, new MockLogger()).GenerateDownCommand().Invoke(worm);
            Assert.AreEqual(world.GetWorms().Count, 1);
        }

        [Test]
        public void GenerateCommandTest_WormCell_NoAnotherWorm()
        {
            var namesGenerator = new NamesGenerator();
            var firstWorm = new Worm(namesGenerator.Generate(), 0, 0, 100);
            var secondWorm = new Worm(namesGenerator.Generate(), 0, 1, 100);
            var world = new World(new MockFoodGenerator(), namesGenerator, new List<Worm> {firstWorm, secondWorm});
            new CommandFactory(world, new MockLogger()).GenerateDownCommand().Invoke(firstWorm);
            Assert.AreEqual(world.GetWorms().Count, 2);
        }
    }
}