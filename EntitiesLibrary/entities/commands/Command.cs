using System;
using System.Text.Json.Serialization;

namespace EntitiesLibrary.entities.commands
{
    public class Command
    {
        [JsonPropertyName("direction")]
        public Direction? Direction { get; set; }
        
        [JsonPropertyName("split")]
        public bool? Split { get; set; }
        
        private bool Equals(Command other)
        {
            return Direction == other.Direction && Split == other.Split;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Command) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Direction, Split);
        }

        public override string ToString()
        {
            return $"{Direction.ToString()} {Split.Value}";
        }
    }
}