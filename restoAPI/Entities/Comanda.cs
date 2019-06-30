using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Comanda
    {
        public Int32 Id { get; set; }
        public Int16 NroComanda { get; set; }
        public DateTime FechaComanda { get; set; }
        public TimeSpan HoraComanda { get; set; }
        public List<DetallePedido> Detalles { get; set; }
        public String Observaciones { get; set; }
        public DateTime? FechaBaja { get; set; }
        public TimeSpan? HoraBaja { get; set; }

        public Comanda ShallowCopy()
        {
            return (Comanda)this.MemberwiseClone();
        }

    }
}
