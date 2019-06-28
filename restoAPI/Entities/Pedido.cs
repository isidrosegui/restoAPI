using restoAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoAPI.Entities
{
    public class Pedido : IDisposable
    {
        public Int32 Id { get; set; }
        public Int16 NroPedido { get; set; }
        public Direccion Direccion { get; set; }
        public Cliente Cliente { get; set; }
        public String Observaciones { get; set; }
        public DateTime? FechaAlta { get; set; }
        public TimeSpan HoraAlta { get; set; }
        public TimeSpan HoraEntrega { get; set; }
        [NotMapped]
        public List<DetallePedido> DetallesPedido { get; set; }
        public Decimal MontoTotal { get; set; }
        public Decimal Descuento { get; set; }
        public List<Comanda> ListaComandas { get; set; }
        public List<Pago> Cobros { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
        public PuntoExpendio PuntoExpendio { get; set; }
        public DateTime? FechaBaja { get; set; }
        public TimeSpan HoraBaja { get; set; }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~Pedido() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion




        /*falta usuario y Perfil*/
    }
}
