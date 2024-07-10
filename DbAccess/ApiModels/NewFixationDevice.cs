using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess.ApiModels
{
    public class NewFixationDevice
    {
        public required string Type { get; set; }
        public required string Number { get; set; }
        public string Address { get; set; }
    }
}
