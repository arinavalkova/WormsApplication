using System.Linq;
using WormsApplication.data;
using WormsApplication.data.behavior.entity;

namespace WormsApplication.services.generator.food
{
    public class FoodGetter : IFoodGenerator
    {
        private int _currentMove = 0;
        private readonly SqlServerBehaviorContext _serverBehaviorContext;
        private readonly string _name;

        public FoodGetter(string name, SqlServerBehaviorContext serverBehaviorContext)
        {
            _serverBehaviorContext = serverBehaviorContext;
            _name = name;
        }

        public Food Generate()
        {
            var behaviorId = _serverBehaviorContext.Behaviors.FirstOrDefault(behaviors => behaviors.Name == _name)!.Id;
            var coord = _serverBehaviorContext.Coords.FirstOrDefault(coords =>
                coords.BehaviorId == behaviorId && coords.Move == _currentMove);
            _currentMove++;
            return new Food(coord!.X, coord.Y);
        }
    }
}