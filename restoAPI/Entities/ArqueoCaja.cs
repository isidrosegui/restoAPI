using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class ArqueoCaja
    {
        public Int32 Id { get; set; }
        public List<DetalleArqueo> Detalles { get; set; }
        public  DateTime FechaArqueo { get; set; }
        public TimeSpan HoraArqueo { get; set; }
        public DateTime? FechaBaja { get; set; }
        public TimeSpan? HoraBaja { get; set; }
        public DateTime? FechaCierreArqueo { get; set; }
        public TimeSpan? HoraCierreArqueo { get; set; }

    }
}
