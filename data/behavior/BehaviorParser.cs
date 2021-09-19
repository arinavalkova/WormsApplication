using System;
using System.Collections.Generic;

namespace WormsApplication.behavior
{
    public class BehaviorParser
    {
        public List<Coord> Parse(string behaviorLine)
        {
            var coordsLines = new List<string>(behaviorLine.Split(" "));
            var listOfCoords = new List<Coord>();
            foreach (var coordLine in coordsLines)
            {
                var x = Convert.ToInt32(coordLine.Split(',')[0]);
                var y = Convert.ToInt32(coordLine.Split(',')[1]);
                listOfCoords.Add(new Coord {X = x, Y = y});
            }
            return listOfCoords;
        }
    }
}