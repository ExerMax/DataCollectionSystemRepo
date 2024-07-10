using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace DbAccess.Models
{
    public class FixationPoint
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FixationDeviceId { get; set; }
        public int VehicleId { get; set; }
        public DateTime? DateTime { get; set; }
        public FixationDevice? FixationDevice { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}
