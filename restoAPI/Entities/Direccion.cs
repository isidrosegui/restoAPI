using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Direccion
    {
        public Int32 Id { get; set; }
        public TipoDireccion TipoDireccion { get; set; }
        public String Calle { get; set; }
        public String NroPuerta { get; set; }
        public String Piso { get; set; }
        public String Dto { get; set; }
        public String Observacion { get; set; }
        public Boolean EsDefault { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public Barrio Barrio { get; set; }

    }
}
