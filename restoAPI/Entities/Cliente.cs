using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Cliente
    {
        public Int32 Id{ get; set; }
        public TipoCliente TipoCliente { get; set; }
        public String Apellido { get; set; }
        public String Nombre { get; set; }
        public String Email { get; set; }
        public String Telefono { get; set; }
        public String TipoTelefono { get; set; }
        public String UsuarioFacebook { get; set; }
        public String UsuarioInstagram { get; set; }
        public List<Direccion> Direcciones { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
