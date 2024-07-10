using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess.ApiModels
{
    public class NewVehicle
    {
        public required string StateRegistrationPlate { get; set; }
        public required bool IsTruck { get; set; }
        public int VehicleOwnerId { get; set; }
    }
}
