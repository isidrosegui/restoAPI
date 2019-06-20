using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Precio
    {
        public Int32 Id { get; set; }
        public Decimal PrecioPublico { get; set; }
        public Decimal PrecioCosto { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
                
    }
}
