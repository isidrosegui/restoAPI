using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Delivery
    {
        public Int32 Id { get; set; }
        public List<Pedido> listaPedido { get; set; }
        public Int16 NroPedido { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraRegreso { get; set; }
        public Decimal MontoTotal { get; set; }
        public Decimal MontoSaldado { get; set; }
        public EstadoDelivery Estado { get; set; }
        public DateTime? FechaBaja { get; set; }
        public TimeSpan HoraBaja { get; set; }

    }
}
