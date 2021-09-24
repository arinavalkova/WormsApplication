using System.ComponentModel.DataAnnotations;

namespace WormsApplication.data.behavior.entity
{
    public class Coords
    {
        [Key] public int Id { get; set; }
        public int BehaviorId { get; set; }
        public int Move { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}