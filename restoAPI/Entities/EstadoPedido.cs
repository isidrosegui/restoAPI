﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class EstadoPedido
    {
        public Int32 Id { get; set; }
        public String Descripcion { get; set; }
        public DateTime? FechaBaja { get; set; }

    }
}
