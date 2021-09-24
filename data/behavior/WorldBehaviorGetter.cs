using System.Collections.Generic;
using WormsApplication.behavior;

namespace WormsApplication.data.behavior
{
    public class WorldBehaviorGetter
    {
        private readonly SqlServerBehaviorContext _serverBehaviorContext;
        private readonly BehaviorParser _behaviorParser; 
        public WorldBehaviorGetter(SqlServerBehaviorContext serverBehaviorContext)
        {
            _serverBehaviorContext = serverBehaviorContext;
            _behaviorParser = new BehaviorParser();
        }
        public Coord Get(string name)
        {
            var coord = _serverBehaviorContext.Coords.Find(name);
            return new Coord {X = coord.X, Y = coord.Y};
        }
    }
}