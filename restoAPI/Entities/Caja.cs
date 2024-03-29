﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Caja
    {
        public Int32 Id { get; set; }
        public String Descripcion { get; set; }
        [NotMapped]
        public DetalleCaja DetalleAbierto { get; set; }
        public List<DetalleCaja> Detalles { get; set; }
        public Boolean EstaAbierta { get; set; }
        public DateTime? FechaBaja { get; set; }

        public Caja ShallowCopy()
        {
            return (Caja)this.MemberwiseClone();
        }
    }
}
