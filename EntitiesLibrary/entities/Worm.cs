using System.Text.Json.Serialization;
using EntitiesLibrary.entities;

namespace WormsApplication.entities
{
    public class Worm
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("lifeStrength")] public int LifeStrength { get; set; }
        [JsonPropertyName("position")] public Position Position { get; set; }

        private static readonly int MaxLifeStrength = 10;

        [JsonConstructor]
        public Worm(string name, int lifeStrength, Position position)
        {
            Name = name;
            LifeStrength = lifeStrength;
            Position = position;
        }

        public Worm(string name)
        {
            Name = name;
            Position = new Position {X = 0, Y = 0};
            LifeStrength = MaxLifeStrength;
        }

        public Worm(string name, int x, int y)
        {
            Name = name;
            Position = new Position {X = x, Y = y};
            LifeStrength = MaxLifeStrength;
        }

        public Worm(string name, int x, int y, int lifeStrength)
        {
            Name = name;
            Position = new Position {X = x, Y = y};
            LifeStrength = lifeStrength;
        }

        public void IncreaseLifeStrength(int count)
        {
            LifeStrength += count;
        }

        public void DecreaseLifeStrength(int count)
        {
            LifeStrength -= count;
        }
    }
}