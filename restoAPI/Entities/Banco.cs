using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Banco
    {
        public Int32 Id { get; set; }
        [Required]
        public String Nombre { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
