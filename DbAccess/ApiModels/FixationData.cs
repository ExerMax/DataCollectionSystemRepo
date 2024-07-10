using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess.ApiModels
{
    public class FixationData
    {
        public string Type { get; set; }
        public string Number { get; set; }
        public string StateRegistrationPlate { get; set; }
        public DateTime DateTime { get; set; }
    }
}
