using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Pago
    {
        public Int32 Id { get; set; }
        public FormaPago FormaPago { get; set; }
        public String Moneda { get; set; }
        public Decimal Monto { get; set; }
        public Tarjeta MarcaTarjeta { get; set; }
        public Banco Banco { get; set; }
        public DateTime FechaAlta { get; set; }
        public TimeSpan HoraAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public TimeSpan? HoraBaja { get; set; }
        public String MotivoBaja { get; set; }
        public Int32 IdPedido { get; set; }
        public Int32 IdDetalleCaja { get; set; }

        }

        /*falta usuario y Perfil*/
    
}
