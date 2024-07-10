using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess.Models
{
    public class TaxMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Tax { get; set; }
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
