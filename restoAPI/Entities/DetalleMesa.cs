using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class DetalleMesa
    {
        public Int32 Id { get; set; }
        public DateTime? FechaApertura { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public TimeSpan HoraCierre { get; set; }
        public String CantidadComensales { get; set; }
        public Int32 IdMesa { get; set; }
        public DateTime? FechaBaja { get; set; }
        public TimeSpan HoraBaja { get; set; }
    }
}
