using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess.ApiModels
{
    public class NewRoad
    {
        public string StartPointType { get; set; }
        public string StartPointNumber { get; set; }
        public string EndPointType { get; set; }
        public string EndPointNumber { get; set; }
        public int Value { get; set; }
    }
}
