using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restoAPI.Models
{
    public class ProductoDTO
    {
        public Int32 Id { get; set; }
        public Int32 TipoDeProductoId { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public List<PrecioDTO> HistoPrecios { get; set; }
        public Int32 PrecioActualId { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
