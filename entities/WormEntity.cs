namespace WormsApplication.entities
{
    public class WormEntity
    {
        public string name { get; set; }
        public int lifeStrength { get; set; }
        public Position position { get; set; }
        
        private static readonly int MaxLifeStrength = 10;
        
        public WormEntity(string name)
        {
            this.name = name;
            position = new Position {X = 0, Y = 0};
            lifeStrength = MaxLifeStrength;
        }

        public WormEntity(string name, int x, int y)
        {
            this.name = name;
            position = new Position {X = x, Y = y};
            lifeStrength = MaxLifeStrength;
        }

        public WormEntity(string name, int x, int y, int lifeStrength)
        {
            this.name = name;
            position = new Position {X = x, Y = y};
            this.lifeStrength = lifeStrength;
        }
        
        public void IncreaseLifeStrength(int count)
        {
            this.lifeStrength += count;
        }

        public void DecreaseLifeStrength(int count)
        {
            this.lifeStrength -= count;
        }
    }
}