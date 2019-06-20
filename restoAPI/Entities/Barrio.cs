using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Barrio
    {
        public Int32 Id { get; set; }
        [Required]
        public String Nombre { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
