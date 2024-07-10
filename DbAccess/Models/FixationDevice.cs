using System.ComponentModel.DataAnnotations.Schema;

namespace DbAccess.Models
{
    public class FixationDevice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsActual { get; set; } = true;
        public required string Type { get; set; }
        public required string Number { get; set; }
        public string Address { get; set; }
        public List<FixationPoint> Points { get; set; }
    }
}
