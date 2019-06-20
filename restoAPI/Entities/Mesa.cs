using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Mesa
    {
        public Int32 Id { get; set; }
        public String Descripcion { get; set; }
        public DetalleMesa DetalleAbiertoMesa { get; set; }
        public List<DetalleMesa> DetallesMesaHisto { get; set; }
        public Pedido PedidoAbierto { get; set; }
        public Boolean EstaAbierta { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }


        //public Mesa ShallowCopy()
        //{
        //    return (Mesa)this.MemberwiseClone();
        //}
        //public Mesa DeepCopy()
        //{
        //    Mesa deepcopyCompany = new Mesa(this.IdMesa,  this.Descripcion, this.EstaAbierta,  
        //                                    this.DetalleAbiertoMesa.
        //                        desc.CompanyName, desc.Owner);
        //    return deepcopyCompany;
        //}

        //public Mesa DeepCopy()
        //{
        //    Mesa other = (Mesa)this.MemberwiseClone();

        //    other.Descripcion = String.Copy(Descripcion);
        //    other.DetalleAbiertoMesa.CantidadComensales = (DetalleAbiertoMesa.);
        //    this.DeepCopy
        //    other.IdInfo = new IdInfo(IdInfo.IdNumber);
        //    other.Name = String.Copy(Name);
        //    return other;
        //}
    }
}
