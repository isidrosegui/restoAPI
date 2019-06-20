using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restoAPI.Models
{
    public class PrecioDTO
    {
        public Int32 Id { get; set; }
        public Decimal PrecioPublico { get; set; }
        public Decimal PrecioCosto { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
    }
}
