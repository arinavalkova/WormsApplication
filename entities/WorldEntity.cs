using System.Collections.Generic;

namespace WormsApplication.entities
{
    public class WorldEntity
    {
        public List<WormEntity> worms { get; set; }
        public List<FoodEntity> food { get; set; }
        
        public static int mx = 100;

        public int test(int x)
        {
            return mx;
        }
    }
}