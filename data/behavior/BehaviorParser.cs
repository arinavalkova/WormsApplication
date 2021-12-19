using System;
using System.Collections.Generic;
using WormsApplication.entities;

namespace WormsApplication.data.behavior
{
    public class BehaviorParser
    {
        public List<Position> Parse(string behaviorLine)
        {
            var coordsLines = new List<string>(behaviorLine.Split(" "));
            var listOfCoords = new List<Position>();
            foreach (var coordLine in coordsLines)
            {
                var x = Convert.ToInt32(coordLine.Split(',')[0]);
                var y = Convert.ToInt32(coordLine.Split(',')[1]);
                listOfCoords.Add(new Position() {X = x, Y = y});
            }
            return listOfCoords;
        }
    }
}