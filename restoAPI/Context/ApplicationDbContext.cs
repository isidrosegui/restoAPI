using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restoAPI.Entities;
namespace restoAPI.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Barrio> Barrios { get; set; }
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DetalleCaja> DetallesCaja { get; set; }
        public DbSet<DetalleMesa> DetallesMesa { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<EstadoDelivery> EstadosDelivery { get; set; }
        public DbSet<EstadoPedido> EstadosPedido { get; set; }
        public DbSet<FormaPago> FormasPago { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Pedido> Pedidos{ get; set; }
        public DbSet<Precio> Precios { get; set; }
        public DbSet<Producto> Productos{ get; set; }
        public DbSet<PuntoExpendio> PuntosExpendio { get; set; }
        public DbSet<Repartidor> Repartidores { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<TipoCliente> TiposCliente { get; set; }
        public DbSet<TipoDireccion> TiposDireccion { get; set; }
        public DbSet<TipoProducto> TiposProducto { get; set; }
        public DbSet<TipoTelefono> TiposTelefono{ get; set; }
        public DbSet<DetalleArqueo> DetallesArqueo { get; set; }
        public DbSet<ArqueoCaja> ArqueoCajas { get; set; }
        public DbSet<EstadoArqueo> EstadosArqueo{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>().Ignore(c => c.DetallesPedido);
        }


    }
}
