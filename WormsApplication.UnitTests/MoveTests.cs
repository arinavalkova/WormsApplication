using System;
using System.Collections.Generic;
using NUnit.Framework;
using WormsApplication.commands;
using WormsApplication.entities;
using WormsApplication.services.generator.food;
using WormsApplication.services.generator.name;
using WormsApplication.services.logger;

namespace WormsApplication.UnitTests
{
    public class MoveTests
    {
        [Test]
        public void MoveCommandTest_EmptyCell_WormMoved()
        {
            var foodGenerator = new MockFoodGenerator();
            var namesGenerator = new NamesGenerator();
            var fileLogger = new MockLogger();
            var dictionary = new Dictionary<Func<CommandFactory, MoveCommand>, Position>
            {
                [factory => factory.MoveDownCommand()] = new() {X = 0, Y = 1},
                [factory => factory.MoveUpCommand()] = new() {X = 0, Y = -1},
                [factory => factory.MoveLeftCommand()] = new() {X = -1, Y = 0},
                [factory => factory.MoveRightCommand()] = new() {X = 1, Y = 0}
            };
            foreach (var (command, coord) in dictionary)
            {
                var worm = new Worm(namesGenerator.Generate(), 0, 0);
                var world = new World(foodGenerator, namesGenerator, new List<Worm> {worm});
                command(new CommandFactory(world, fileLogger)).Invoke(worm);
                Assert.AreEqual(new Position {X = worm.Position.X, Y = worm.Position.Y}, coord);
            }
        }

        [Test]
        public void MoveCommandTest_FoodCell_VitalityIncreased()
        {
            var startVitality = 10;
            var vitalityAfterEating = 10;
            var vitalityAfterMove = 1;
            var foodCoord = new Position {X = 0, Y = 1};
            var namesGenerator = new NamesGenerator();
            var worm = new Worm(namesGenerator.Generate(), 0, 0);
            var world = new World(
                new CustomFoodGenerator(new List<Position> {foodCoord}),
                namesGenerator,
                new List<Worm> {worm}
            );

            new CommandFactory(world, new MockLogger()).MoveDownCommand().Invoke(worm);
            Assert.AreEqual(new Position {X = worm.Position.X, Y = worm.Position.Y}, foodCoord);
            Assert.AreEqual(worm.LifeStrength, startVitality + vitalityAfterEating - vitalityAfterMove);
        }

        [Test]
        public void MoveCommandTest_WormCell_NoMove()
        {
            var namesGenerator = new NamesGenerator();
            var firstWorm = new Worm(namesGenerator.Generate(), 0, 0);
            var secondWorm = new Worm(namesGenerator.Generate(), 0, 1);
            var world = new World(new MockFoodGenerator(), namesGenerator, new List<Worm> {firstWorm, secondWorm});
            new CommandFactory(world, new MockLogger()).MoveDownCommand().Invoke(firstWorm);
            Assert.AreEqual(new Position {X = firstWorm.Position.X, Y = firstWorm.Position.Y},
                new Position {X = 0, Y = 0});
        }
    }
}