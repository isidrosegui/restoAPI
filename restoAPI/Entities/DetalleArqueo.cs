﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class DetalleArqueo
    {
        public Int32 Id { get; set; }
        public FormaPago FormaPago { get; set; }
        public Decimal Monto { get; set; }
        public Int32 DetalleCajaId { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.Now.Date;
        public TimeSpan HoraAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public TimeSpan? HoraBaja { get; set; }
        public String Observaciones { get; set; }

    }
}
