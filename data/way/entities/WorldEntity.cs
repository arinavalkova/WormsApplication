using System.Collections.Generic;

namespace WormsApplication.data.way.entities
{
    public class WorldEntity
    {
        public List<WormEntity> worms { get; set; }
        public List<FoodEntity> food { get; set; }
    }
}