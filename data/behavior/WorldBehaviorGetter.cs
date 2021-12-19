using System.Collections.Generic;
using WormsApplication.entities;

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
        public Position Get(string name)
        {
            var coord = _serverBehaviorContext.Coords.Find(name);
            return new Position() {X = coord.X, Y = coord.Y};
        }
    }
}