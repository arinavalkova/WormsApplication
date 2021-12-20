using System.Collections.Generic;
using System.Text.Json.Serialization;
using WormsApplication.entities;

namespace EntitiesLibrary.entities
{
    public class WorldState
    {
        [JsonPropertyName("worms")] public List<Worm>? Worms { get; set; }
        [JsonPropertyName("food")] public List<Food>? Food { get; set; }

        [JsonConstructor]
        public WorldState(List<Worm> worms, List<Food> food)
        {
            Worms = worms;
            Food = food;
        }

        public WorldState()
        {
            Worms = new List<Worm>();
            Food = new List<Food>();
        }
    }
}