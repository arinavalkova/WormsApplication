using System;
using System.Collections.Generic;
using System.Linq;
using EntitiesLibrary.entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NUnit.Framework;
using WormsApplication.data;
using WormsApplication.data.behavior;
using WormsApplication.data.behavior.contexts;
using WormsApplication.data.behavior.entity;
using WormsApplication.entities;
using WormsApplication.services.generator.food;

namespace WormsApplication.UnitTests
{
    public class EFTests
    {
        private List<Position>? _listOfCoords;
        [Test]
        public void BehaviorGenerateTest_NewBehavior_BehaviorInDatabase()
        {
            var countOfMoves = 100;
            var worldBehaviorGenerator = new WorldBehaviorGenerator(new FoodGenerator(new Random()));
            _listOfCoords = worldBehaviorGenerator.Generate(countOfMoves);
            using var context = new BehaviorContext();
            EntityEntry<Behaviors> entityEntry = context.Behaviors.Add(new Behaviors {Name = "name"});
            context.SaveChanges();
            for (var i = 0; i < countOfMoves; i++)
            {
                context.Coords.Add(new Coords
                    {BehaviorId = entityEntry.Entity.Id, Move = i, X = _listOfCoords[i].X, Y = _listOfCoords[i].Y}
                );
            }
            context.SaveChanges();
        }

        [Test]
        public void ReadingBehaviorTest_BehaviorInDatabase_ReadBehavior()
        {
            var context = new BehaviorContext();
            var behaviorId = context.Behaviors.FirstOrDefault(behavior => behavior.Name == "name")!.Id;
            for (var i = 0; i < 100; i++)
            {
                var coord = context.Coords.FirstOrDefault(coords => coords.Move == i && coords.BehaviorId == behaviorId);
                Assert.AreEqual(new Position {X = coord!.X, Y = coord.Y}, _listOfCoords![i]);
            }
        }
    }
}