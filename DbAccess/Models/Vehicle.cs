using System.ComponentModel.DataAnnotations.Schema;

namespace DbAccess.Models
{
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsActual { get; set; } = true;
        public required string StateRegistrationPlate { get; set; }
        public required bool IsTruck { get; set; }
        public int VehicleOwnerId { get; set; }
        public VehicleOwner? VehicleOwner { get; set; }
        public List<FixationPoint>? FixationPoints { get; set; }
    }
}
