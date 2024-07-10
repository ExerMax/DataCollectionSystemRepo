using System.ComponentModel.DataAnnotations.Schema;

namespace DbAccess.Models
{
    public class VehicleOwner
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Patronymic { get; set; }
        public required string TaxpayerIdentificationNumber { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required string Document { get; set; }
    }
}
