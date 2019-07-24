using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace restoAPI.Entities
{
    public class Producto
    {
        public Int32 Id { get; set; }
        public TipoProducto TipoDeProducto { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public List<Precio> HistoPrecios { get; set; }
        public Precio PrecioActual { get; set; }
        public DateTime FechaAlta { get; set;}
        public DateTime? FechaBaja { get; set; }
        public bool ImprimeEnComanda { get; set; }
        //public List<String> UrlImage  { get; set; }
    }
}
