using DbAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess.Models
{
    public class Road
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsActual { get; set; } = true;
        public int StartPointId { get; set; }
        public int EndPointId { get; set; }
        public int Value { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public FixationDevice? StartPoint { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public FixationDevice? EndPoint { get; set; }
    }
}
