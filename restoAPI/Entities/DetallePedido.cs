using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class DetallePedido
    {
        public Int32 Id { get; set; }
        public Int32 IdPedido { get; set; }
        public Producto Producto { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Descuento { get; set; }
        public Decimal Subtotal { get; set; }
        public DateTime? FechaBaja { get; set; }
        public TimeSpan? HoraBaja { get; set; }
        
        public DetallePedido ShallowCopy()
        {
            return (DetallePedido)this.MemberwiseClone();
        }
    }

    
    //public Mesa DeepCopy()
    //{
    //    Mesa deepcopyCompany = new Mesa(this.IdMesa,  this.Descripcion, this.EstaAbierta,  
    //                                    this.DetalleAbiertoMesa.
    //                        desc.CompanyName, desc.Owner);
    //    return deepcopyCompany;
    //}
}
