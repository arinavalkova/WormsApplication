using System.ComponentModel.DataAnnotations;

namespace WormsApplication.data.behavior.entity
{
    public class Behaviors
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoordsLine { get; set; }
    }
}