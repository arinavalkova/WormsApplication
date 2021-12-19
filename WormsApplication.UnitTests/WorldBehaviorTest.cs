using System;
using NUnit.Framework;
using WormsApplication.data.behavior;
using WormsApplication.services.generator.food;

namespace WormsApplication.UnitTests
{
    public class WorldBehaviorTest
    {
        [Test]
        public void GenerateTest_NewCoords_UniquenessCoords()
        {
            var countOfMoves = 100;
            var foodGenerator = new FoodGenerator(new Random());
            var worldBehaviorGenerator = new WorldBehaviorGenerator(foodGenerator);
            var listOfGeneratedCoords = worldBehaviorGenerator.Generate(countOfMoves);
            for (var i = 0; i < countOfMoves; i++)
                for (var j = 0; j < countOfMoves && i != j; j++)
                    Assert.AreEqual(!listOfGeneratedCoords[i].Equals(listOfGeneratedCoords[j]) || i - j >= 10, true);
        }

        [Test]
        public void ParseTest_CoordsLine_CoordsList()
        {
            var foodGenerator = new FoodGenerator(new Random());
            var worldBehaviorGenerator = new WorldBehaviorGenerator(foodGenerator);
            var behaviorParser = new BehaviorParser();
            var listOfCoords = worldBehaviorGenerator.Generate(3);
            var coordsLine = worldBehaviorGenerator.CoordsToString(listOfCoords);
            Assert.AreEqual(listOfCoords, behaviorParser.Parse(coordsLine));
        }
    }
}