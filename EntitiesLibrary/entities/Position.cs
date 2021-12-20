using System;
using System.Text.Json.Serialization;

namespace EntitiesLibrary.entities
{
    public class Position
    {
        [JsonPropertyName("x")] public int X { get; set; }
        [JsonPropertyName("y")] public int Y { get; set; }

        [JsonConstructor]
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position()
        {
        }

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

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
    }
}