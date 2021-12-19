using System;
using System.Text.Json.Serialization;

namespace WormsApplication.entities
{
    public class Position
    {
        [JsonPropertyName("x")]
        public int X { get; init; }
        [JsonPropertyName("y")]
        public int Y { get; init; }
        
        private bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}