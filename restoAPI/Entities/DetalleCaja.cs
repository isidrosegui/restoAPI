using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class DetalleCaja
    {
        public Int32 Id { get; set; }
        public DateTime? FechaApertura { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public TimeSpan HoraCierre { get; set; }
        public Decimal MontoApertura { get; set; }
        public Decimal MontoCierre { get; set; }
        public List<Pago> Cobros { get; set; }
        public DateTime? FechaBaja { get; set; }
        public TimeSpan? HoraBaja { get; set; }
        public List<DetalleArqueo> Arqueo { get; set; }
        
    }
}
