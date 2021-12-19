using System.Collections.Generic;
using System.Text.Json.Serialization;
using EntitiesLibrary.entities;

namespace WormsApplication.entities
{
    public class WorldState
    {
        [JsonPropertyName("worms")]
        public List<Worm> Worms { get; set; }
        
        [JsonPropertyName("food")]
        public List<Food> Food { get; set; }
        
        [JsonConstructor]
        public WorldState(List<Worm> worms, List<Food> food){
            Worms = worms;
            Food = food;
        }
        
        public WorldState() {}

        public override string ToString()
        {
            string answer = "Дракончики: \n";
            foreach (var worm in Worms)
            {
                answer += worm.ToString();
            }
            answer += "\n" + "Еда: \n";
            foreach (var food in Food)
            {
                answer += food.ToString();
            }
            return answer;
        }
    }
}