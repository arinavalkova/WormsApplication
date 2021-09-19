using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WormsApplication.behavior;
using WormsApplication.data;
using WormsApplication.data.behavior.entity;
using WormsApplication.services.generator.food;

namespace WormsApplication.UnitTests
{
    public class EFTests
    {
        private string _behaviorLine;
        [Test]
        public void BehaviorGenerateTest_NewBehavior_BehaviorInDatabase()
        {
            var worldBehaviorGenerator = new WorldBehaviorGenerator(new FoodGenerator(new Random()));
            _behaviorLine = worldBehaviorGenerator.CoordsToString(worldBehaviorGenerator.Generate(100));
            using var context = new BehaviorContext();
            context.Behaviors.Add(new Behaviors {Name = "name", CoordsLine = _behaviorLine});
            context.SaveChanges();
        }

        [Test]
        public void ReadingBehaviorTest_BehaviorInDatabase_ReadBehavior()
        {
            Assert.AreEqual(new BehaviorContext().Behaviors.Find("name")!.CoordsLine, _behaviorLine);
        }
    }
}