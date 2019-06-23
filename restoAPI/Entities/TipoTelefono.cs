using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class TipoTelefono
    {
        public Int32 Id { get; set; }
        public String Nombre { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
