using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restoAPI.Models
{
    public class TipoProductoDTO
    {
        public Int32 Id { get; set; }
        public String Descripcion { get; set; }
        public DateTime FechaBaja { get; set; }
    }
}
