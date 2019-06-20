﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using restoAPI.Context;

namespace restoAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190618153001_ActualizoPrecio")]
    partial class ActualizoPrecio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("restoAPI.Entities.Banco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaBaja");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Bancos");
                });

            modelBuilder.Entity("restoAPI.Entities.Barrio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaBaja");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Barrios");
                });

            modelBuilder.Entity("restoAPI.Entities.Caja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<int?>("DetalleAbiertoId");

                    b.Property<bool>("EstaAbierta");

                    b.Property<DateTime>("FechaBaja");

                    b.HasKey("Id");

                    b.HasIndex("DetalleAbiertoId");

                    b.ToTable("Cajas");
                });

            modelBuilder.Entity("restoAPI.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido");

                    b.Property<string>("Email");

                    b.Property<DateTime>("FechaAlta");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<string>("Nombre");

                    b.Property<string>("Telefono");

                    b.Property<int?>("TipoClienteId");

                    b.Property<string>("TipoTelefono");

                    b.Property<string>("UsuarioFacebook");

                    b.Property<string>("UsuarioInstagram");

                    b.HasKey("Id");

                    b.HasIndex("TipoClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("restoAPI.Entities.Comanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaBaja");

                    b.Property<DateTime>("FechaComanda");

                    b.Property<TimeSpan>("HoraBaja");

                    b.Property<TimeSpan>("HoraComanda");

                    b.Property<short>("NroComanda");

                    b.Property<string>("Observaciones");

                    b.Property<int?>("PedidoId");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("Comandas");
                });

            modelBuilder.Entity("restoAPI.Entities.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EstadoId");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<TimeSpan>("HoraBaja");

                    b.Property<TimeSpan>("HoraRegreso");

                    b.Property<TimeSpan>("HoraSalida");

                    b.Property<decimal>("MontoSaldado");

                    b.Property<decimal>("MontoTotal");

                    b.Property<short>("NroPedido");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("restoAPI.Entities.DetalleCaja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CajaId");

                    b.Property<DateTime>("FechaApertura");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<DateTime>("FechaCierre");

                    b.Property<TimeSpan>("HoraApertura");

                    b.Property<TimeSpan>("HoraBaja");

                    b.Property<TimeSpan>("HoraCierre");

                    b.Property<decimal>("MontoApertura");

                    b.Property<decimal>("MontoCierre");

                    b.HasKey("Id");

                    b.HasIndex("CajaId");

                    b.ToTable("DetallesCaja");
                });

            modelBuilder.Entity("restoAPI.Entities.DetalleMesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CantidadComensales");

                    b.Property<DateTime>("FechaApertura");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<DateTime>("FechaCierre");

                    b.Property<TimeSpan>("HoraApertura");

                    b.Property<TimeSpan>("HoraBaja");

                    b.Property<TimeSpan>("HoraCierre");

                    b.Property<int>("IdMesa");

                    b.Property<int?>("MesaId");

                    b.HasKey("Id");

                    b.HasIndex("MesaId");

                    b.ToTable("DetallesMesa");
                });

            modelBuilder.Entity("restoAPI.Entities.DetallePedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cantidad");

                    b.Property<int?>("ComandaId");

                    b.Property<decimal>("Descuento");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<TimeSpan>("HoraBaja");

                    b.Property<int>("IdPedido");

                    b.Property<int?>("PedidoId");

                    b.Property<int?>("ProductoId");

                    b.Property<decimal>("Subtotal");

                    b.HasKey("Id");

                    b.HasIndex("ComandaId");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("DetallesPedido");
                });

            modelBuilder.Entity("restoAPI.Entities.Direccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BarrioId");

                    b.Property<string>("Calle");

                    b.Property<int?>("ClienteId");

                    b.Property<string>("Dto");

                    b.Property<bool>("EsDefault");

                    b.Property<DateTime>("FechaAlta");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<string>("NroPuerta");

                    b.Property<string>("Observacion");

                    b.Property<string>("Piso");

                    b.Property<int?>("TipoDireccionId");

                    b.HasKey("Id");

                    b.HasIndex("BarrioId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("TipoDireccionId");

                    b.ToTable("Direcciones");
                });

            modelBuilder.Entity("restoAPI.Entities.EstadoDelivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaBaja");

                    b.HasKey("Id");

                    b.ToTable("EstadosDelivery");
                });

            modelBuilder.Entity("restoAPI.Entities.EstadoPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaBaja");

                    b.HasKey("Id");

                    b.ToTable("EstadosPedido");
                });

            modelBuilder.Entity("restoAPI.Entities.FormaPago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaBaja");

                    b.HasKey("Id");

                    b.ToTable("FormasPago");
                });

            modelBuilder.Entity("restoAPI.Entities.Mesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<int?>("DetalleAbiertoMesaId");

                    b.Property<bool>("EstaAbierta");

                    b.Property<DateTime>("FechaAlta");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<int?>("PedidoAbiertoId");

                    b.HasKey("Id");

                    b.HasIndex("DetalleAbiertoMesaId");

                    b.HasIndex("PedidoAbiertoId");

                    b.ToTable("Mesas");
                });

            modelBuilder.Entity("restoAPI.Entities.Pago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BancoId");

                    b.Property<int?>("DetalleCajaId");

                    b.Property<DateTime>("FechaAlta");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<int?>("FormaPagoId");

                    b.Property<TimeSpan>("HoraAlta");

                    b.Property<TimeSpan>("HoraBaja");

                    b.Property<int?>("MarcaTarjetaId");

                    b.Property<string>("Moneda");

                    b.Property<decimal>("Monto");

                    b.Property<int?>("PedidoId");

                    b.HasKey("Id");

                    b.HasIndex("BancoId");

                    b.HasIndex("DetalleCajaId");

                    b.HasIndex("FormaPagoId");

                    b.HasIndex("MarcaTarjetaId");

                    b.HasIndex("PedidoId");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("restoAPI.Entities.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClienteId");

                    b.Property<int?>("DeliveryId");

                    b.Property<decimal>("Descuento");

                    b.Property<int?>("DireccionId");

                    b.Property<int?>("EstadoPedidoId");

                    b.Property<DateTime>("FechaAlta");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<TimeSpan>("HoraAlta");

                    b.Property<TimeSpan>("HoraBaja");

                    b.Property<TimeSpan>("HoraEntrega");

                    b.Property<decimal>("MontoTotal");

                    b.Property<short>("NroPedido");

                    b.Property<string>("Observaciones");

                    b.Property<int?>("PuntoExpendioId");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("DireccionId");

                    b.HasIndex("EstadoPedidoId");

                    b.HasIndex("PuntoExpendioId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("restoAPI.Entities.Precio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaAlta");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<decimal>("PrecioCosto");

                    b.Property<decimal>("PrecioPublico");

                    b.Property<int?>("ProductoId");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("Precios");
                });

            modelBuilder.Entity("restoAPI.Entities.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaAlta");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<string>("Nombre");

                    b.Property<int?>("PrecioActualId");

                    b.Property<int?>("TipoDeProductoId");

                    b.HasKey("Id");

                    b.HasIndex("PrecioActualId");

                    b.HasIndex("TipoDeProductoId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("restoAPI.Entities.PuntoExpendio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaBaja");

                    b.HasKey("Id");

                    b.ToTable("PuntosExpendio");
                });

            modelBuilder.Entity("restoAPI.Entities.Repartidor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaBaja");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Repartidores");
                });

            modelBuilder.Entity("restoAPI.Entities.Tarjeta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaBaja");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Tarjetas");
                });

            modelBuilder.Entity("restoAPI.Entities.TipoCliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaBaja");

                    b.HasKey("Id");

                    b.ToTable("TiposCliente");
                });

            modelBuilder.Entity("restoAPI.Entities.TipoDireccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaBaja");

                    b.HasKey("Id");

                    b.ToTable("TiposDireccion");
                });

            modelBuilder.Entity("restoAPI.Entities.TipoProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaBaja");

                    b.HasKey("Id");

                    b.ToTable("TiposProducto");
                });

            modelBuilder.Entity("restoAPI.Entities.Caja", b =>
                {
                    b.HasOne("restoAPI.Entities.DetalleCaja", "DetalleAbierto")
                        .WithMany()
                        .HasForeignKey("DetalleAbiertoId");
                });

            modelBuilder.Entity("restoAPI.Entities.Cliente", b =>
                {
                    b.HasOne("restoAPI.Entities.TipoCliente", "TipoCliente")
                        .WithMany()
                        .HasForeignKey("TipoClienteId");
                });

            modelBuilder.Entity("restoAPI.Entities.Comanda", b =>
                {
                    b.HasOne("restoAPI.Entities.Pedido")
                        .WithMany("ListaComandas")
                        .HasForeignKey("PedidoId");
                });

            modelBuilder.Entity("restoAPI.Entities.Delivery", b =>
                {
                    b.HasOne("restoAPI.Entities.EstadoDelivery", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId");
                });

            modelBuilder.Entity("restoAPI.Entities.DetalleCaja", b =>
                {
                    b.HasOne("restoAPI.Entities.Caja")
                        .WithMany("Detalles")
                        .HasForeignKey("CajaId");
                });

            modelBuilder.Entity("restoAPI.Entities.DetalleMesa", b =>
                {
                    b.HasOne("restoAPI.Entities.Mesa")
                        .WithMany("DetallesMesaHisto")
                        .HasForeignKey("MesaId");
                });

            modelBuilder.Entity("restoAPI.Entities.DetallePedido", b =>
                {
                    b.HasOne("restoAPI.Entities.Comanda")
                        .WithMany("Detalles")
                        .HasForeignKey("ComandaId");

                    b.HasOne("restoAPI.Entities.Pedido")
                        .WithMany("DetallesPedido")
                        .HasForeignKey("PedidoId");

                    b.HasOne("restoAPI.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId");
                });

            modelBuilder.Entity("restoAPI.Entities.Direccion", b =>
                {
                    b.HasOne("restoAPI.Entities.Barrio", "Barrio")
                        .WithMany()
                        .HasForeignKey("BarrioId");

                    b.HasOne("restoAPI.Entities.Cliente")
                        .WithMany("Direcciones")
                        .HasForeignKey("ClienteId");

                    b.HasOne("restoAPI.Entities.TipoDireccion", "TipoDireccion")
                        .WithMany()
                        .HasForeignKey("TipoDireccionId");
                });

            modelBuilder.Entity("restoAPI.Entities.Mesa", b =>
                {
                    b.HasOne("restoAPI.Entities.DetalleMesa", "DetalleAbiertoMesa")
                        .WithMany()
                        .HasForeignKey("DetalleAbiertoMesaId");

                    b.HasOne("restoAPI.Entities.Pedido", "PedidoAbierto")
                        .WithMany()
                        .HasForeignKey("PedidoAbiertoId");
                });

            modelBuilder.Entity("restoAPI.Entities.Pago", b =>
                {
                    b.HasOne("restoAPI.Entities.Banco", "Banco")
                        .WithMany()
                        .HasForeignKey("BancoId");

                    b.HasOne("restoAPI.Entities.DetalleCaja")
                        .WithMany("Cobros")
                        .HasForeignKey("DetalleCajaId");

                    b.HasOne("restoAPI.Entities.FormaPago", "FormaPago")
                        .WithMany()
                        .HasForeignKey("FormaPagoId");

                    b.HasOne("restoAPI.Entities.Tarjeta", "MarcaTarjeta")
                        .WithMany()
                        .HasForeignKey("MarcaTarjetaId");

                    b.HasOne("restoAPI.Entities.Pedido")
                        .WithMany("Cobros")
                        .HasForeignKey("PedidoId");
                });

            modelBuilder.Entity("restoAPI.Entities.Pedido", b =>
                {
                    b.HasOne("restoAPI.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.HasOne("restoAPI.Entities.Delivery")
                        .WithMany("listaPedido")
                        .HasForeignKey("DeliveryId");

                    b.HasOne("restoAPI.Entities.Direccion", "Direccion")
                        .WithMany()
                        .HasForeignKey("DireccionId");

                    b.HasOne("restoAPI.Entities.EstadoPedido", "EstadoPedido")
                        .WithMany()
                        .HasForeignKey("EstadoPedidoId");

                    b.HasOne("restoAPI.Entities.PuntoExpendio", "PuntoExpendio")
                        .WithMany()
                        .HasForeignKey("PuntoExpendioId");
                });

            modelBuilder.Entity("restoAPI.Entities.Precio", b =>
                {
                    b.HasOne("restoAPI.Entities.Producto")
                        .WithMany("HistoPrecios")
                        .HasForeignKey("ProductoId");
                });

            modelBuilder.Entity("restoAPI.Entities.Producto", b =>
                {
                    b.HasOne("restoAPI.Entities.Precio", "PrecioActual")
                        .WithMany()
                        .HasForeignKey("PrecioActualId");

                    b.HasOne("restoAPI.Entities.TipoProducto", "TipoDeProducto")
                        .WithMany()
                        .HasForeignKey("TipoDeProductoId");
                });
#pragma warning restore 612, 618
        }
    }
}
