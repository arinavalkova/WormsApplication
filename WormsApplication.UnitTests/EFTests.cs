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
        [Test]
        public void BehaviorTest_AddBehavior_AddedBehavior()
        {
            var options = new DbContextOptionsBuilder<BehaviorContext>()
                .UseInMemoryDatabase("Behaviors")
                .Options;
            
            var worldBehaviorGenerator = new WorldBehaviorGenerator(new FoodGenerator(new Random()));
            var behaviorLine = 
                worldBehaviorGenerator.CoordsToString(worldBehaviorGenerator.Generate(100));

            using var context = new BehaviorContext(options);
            // var behavior = new Behaviors
            // {
            //     //name = "FirstWorld",
            //     // = behaviorLine
            // };
            //
            // context.Behaviors.Add(behavior);
            // context.SaveChanges();
        }
    }
}