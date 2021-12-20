using System.Text.Json.Serialization;

namespace EntitiesLibrary.entities
{
    public class Food
    {
        [JsonPropertyName("expiresIn")] public int ExpiresIn { get; set; }
        [JsonPropertyName("position")] public Position Position { get; set; }

        private static readonly int MaxAge = 10;

        [JsonConstructor]
        public Food(int expiresIn, Position position)
        {
            ExpiresIn = expiresIn;
            Position = position;
        }

        public Food(int x, int y)
        {
            Position = new Position {X = x, Y = y};
            ExpiresIn = MaxAge;
        }

        public void DecreaseExpiresIn()
        {
            ExpiresIn--;
        }
    }
}