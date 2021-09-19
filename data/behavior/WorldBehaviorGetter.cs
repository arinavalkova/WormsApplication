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

        public List<Coord> Get(string name)
        {
            var behavior = _serverBehaviorContext.Behaviors.Find(name);
            return _behaviorParser.Parse(behavior!.CoordsLine);
        }
    }
}