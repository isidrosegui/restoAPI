using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bancos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Barrios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barrios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosDelivery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosDelivery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosPedido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosPedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormasPago",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PuntosExpendio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntosExpendio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repartidores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repartidores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposCliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposCliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDireccion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDireccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposProducto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProducto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposTelefono",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposTelefono", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NroPedido = table.Column<short>(nullable: false),
                    HoraSalida = table.Column<TimeSpan>(nullable: false),
                    HoraRegreso = table.Column<TimeSpan>(nullable: false),
                    MontoTotal = table.Column<decimal>(nullable: false),
                    MontoSaldado = table.Column<decimal>(nullable: false),
                    EstadoId = table.Column<int>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    HoraBaja = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_EstadosDelivery_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "EstadosDelivery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoDeProductoId = table.Column<int>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_TiposProducto_TipoDeProductoId",
                        column: x => x.TipoDeProductoId,
                        principalTable: "TiposProducto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoClienteId = table.Column<int>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    TipoTelefonoId = table.Column<int>(nullable: true),
                    UsuarioFacebook = table.Column<string>(nullable: true),
                    UsuarioInstagram = table.Column<string>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    CUIT = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_TiposCliente_TipoClienteId",
                        column: x => x.TipoClienteId,
                        principalTable: "TiposCliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_TiposTelefono_TipoTelefonoId",
                        column: x => x.TipoTelefonoId,
                        principalTable: "TiposTelefono",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Precios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrecioPublico = table.Column<decimal>(nullable: false),
                    PrecioCosto = table.Column<decimal>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    ProductoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Precios_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoDireccionId = table.Column<int>(nullable: true),
                    Calle = table.Column<string>(nullable: true),
                    NroPuerta = table.Column<string>(nullable: true),
                    Piso = table.Column<string>(nullable: true),
                    Dto = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true),
                    EsDefault = table.Column<bool>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    BarrioId = table.Column<int>(nullable: true),
                    ClienteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direcciones_Barrios_BarrioId",
                        column: x => x.BarrioId,
                        principalTable: "Barrios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Direcciones_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Direcciones_TiposDireccion_TipoDireccionId",
                        column: x => x.TipoDireccionId,
                        principalTable: "TiposDireccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NroPedido = table.Column<short>(nullable: false),
                    DireccionId = table.Column<int>(nullable: true),
                    ClienteId = table.Column<int>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: true),
                    HoraAlta = table.Column<TimeSpan>(nullable: false),
                    HoraEntrega = table.Column<TimeSpan>(nullable: false),
                    MontoTotal = table.Column<decimal>(nullable: false),
                    Descuento = table.Column<decimal>(nullable: false),
                    EstadoPedidoId = table.Column<int>(nullable: true),
                    PuntoExpendioId = table.Column<int>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    HoraBaja = table.Column<TimeSpan>(nullable: false),
                    DeliveryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Direcciones_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "Direcciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_EstadosPedido_EstadoPedidoId",
                        column: x => x.EstadoPedidoId,
                        principalTable: "EstadosPedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_PuntosExpendio_PuntoExpendioId",
                        column: x => x.PuntoExpendioId,
                        principalTable: "PuntosExpendio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comandas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NroComanda = table.Column<short>(nullable: false),
                    FechaComanda = table.Column<DateTime>(nullable: false),
                    HoraComanda = table.Column<TimeSpan>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    HoraBaja = table.Column<TimeSpan>(nullable: false),
                    PedidoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comandas_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesPedido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPedido = table.Column<int>(nullable: false),
                    ProductoId = table.Column<int>(nullable: true),
                    Cantidad = table.Column<decimal>(nullable: false),
                    Descuento = table.Column<decimal>(nullable: false),
                    Subtotal = table.Column<decimal>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    HoraBaja = table.Column<TimeSpan>(nullable: false),
                    ComandaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesPedido_Comandas_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesPedido_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormaPagoId = table.Column<int>(nullable: true),
                    Moneda = table.Column<string>(nullable: true),
                    Monto = table.Column<decimal>(nullable: false),
                    MarcaTarjetaId = table.Column<int>(nullable: true),
                    BancoId = table.Column<int>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    HoraAlta = table.Column<TimeSpan>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    HoraBaja = table.Column<TimeSpan>(nullable: false),
                    DetalleCajaId = table.Column<int>(nullable: true),
                    PedidoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagos_Bancos_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Bancos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagos_FormasPago_FormaPagoId",
                        column: x => x.FormaPagoId,
                        principalTable: "FormasPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagos_Tarjetas_MarcaTarjetaId",
                        column: x => x.MarcaTarjetaId,
                        principalTable: "Tarjetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesCaja",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaApertura = table.Column<DateTime>(nullable: true),
                    HoraApertura = table.Column<TimeSpan>(nullable: false),
                    FechaCierre = table.Column<DateTime>(nullable: true),
                    HoraCierre = table.Column<TimeSpan>(nullable: false),
                    MontoApertura = table.Column<decimal>(nullable: false),
                    MontoCierre = table.Column<decimal>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    HoraBaja = table.Column<TimeSpan>(nullable: false),
                    CajaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesCaja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cajas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    DetalleAbiertoId = table.Column<int>(nullable: true),
                    EstaAbierta = table.Column<bool>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cajas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cajas_DetallesCaja_DetalleAbiertoId",
                        column: x => x.DetalleAbiertoId,
                        principalTable: "DetallesCaja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    DetalleAbiertoMesaId = table.Column<int>(nullable: true),
                    PedidoAbiertoId = table.Column<int>(nullable: true),
                    EstaAbierta = table.Column<bool>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mesas_Pedidos_PedidoAbiertoId",
                        column: x => x.PedidoAbiertoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesMesa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaApertura = table.Column<DateTime>(nullable: true),
                    HoraApertura = table.Column<TimeSpan>(nullable: false),
                    FechaCierre = table.Column<DateTime>(nullable: true),
                    HoraCierre = table.Column<TimeSpan>(nullable: false),
                    CantidadComensales = table.Column<string>(nullable: true),
                    IdMesa = table.Column<int>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    HoraBaja = table.Column<TimeSpan>(nullable: false),
                    MesaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesMesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesMesa_Mesas_MesaId",
                        column: x => x.MesaId,
                        principalTable: "Mesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cajas_DetalleAbiertoId",
                table: "Cajas",
                column: "DetalleAbiertoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoClienteId",
                table: "Clientes",
                column: "TipoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoTelefonoId",
                table: "Clientes",
                column: "TipoTelefonoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comandas_PedidoId",
                table: "Comandas",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_EstadoId",
                table: "Deliveries",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCaja_CajaId",
                table: "DetallesCaja",
                column: "CajaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesMesa_MesaId",
                table: "DetallesMesa",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedido_ComandaId",
                table: "DetallesPedido",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedido_ProductoId",
                table: "DetallesPedido",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_BarrioId",
                table: "Direcciones",
                column: "BarrioId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_ClienteId",
                table: "Direcciones",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_TipoDireccionId",
                table: "Direcciones",
                column: "TipoDireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_DetalleAbiertoMesaId",
                table: "Mesas",
                column: "DetalleAbiertoMesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_PedidoAbiertoId",
                table: "Mesas",
                column: "PedidoAbiertoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_BancoId",
                table: "Pagos",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_DetalleCajaId",
                table: "Pagos",
                column: "DetalleCajaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_FormaPagoId",
                table: "Pagos",
                column: "FormaPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_MarcaTarjetaId",
                table: "Pagos",
                column: "MarcaTarjetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_PedidoId",
                table: "Pagos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_DeliveryId",
                table: "Pedidos",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_DireccionId",
                table: "Pedidos",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EstadoPedidoId",
                table: "Pedidos",
                column: "EstadoPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_PuntoExpendioId",
                table: "Pedidos",
                column: "PuntoExpendioId");

            migrationBuilder.CreateIndex(
                name: "IX_Precios_ProductoId",
                table: "Precios",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_TipoDeProductoId",
                table: "Productos",
                column: "TipoDeProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_DetallesCaja_DetalleCajaId",
                table: "Pagos",
                column: "DetalleCajaId",
                principalTable: "DetallesCaja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCaja_Cajas_CajaId",
                table: "DetallesCaja",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mesas_DetallesMesa_DetalleAbiertoMesaId",
                table: "Mesas",
                column: "DetalleAbiertoMesaId",
                principalTable: "DetallesMesa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cajas_DetallesCaja_DetalleAbiertoId",
                table: "Cajas");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TiposCliente_TipoClienteId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TiposTelefono_TipoTelefonoId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Mesas_Pedidos_PedidoAbiertoId",
                table: "Mesas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesMesa_Mesas_MesaId",
                table: "DetallesMesa");

            migrationBuilder.DropTable(
                name: "DetallesPedido");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Precios");

            migrationBuilder.DropTable(
                name: "Repartidores");

            migrationBuilder.DropTable(
                name: "Comandas");

            migrationBuilder.DropTable(
                name: "Bancos");

            migrationBuilder.DropTable(
                name: "FormasPago");

            migrationBuilder.DropTable(
                name: "Tarjetas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "TiposProducto");

            migrationBuilder.DropTable(
                name: "DetallesCaja");

            migrationBuilder.DropTable(
                name: "Cajas");

            migrationBuilder.DropTable(
                name: "TiposCliente");

            migrationBuilder.DropTable(
                name: "TiposTelefono");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropTable(
                name: "EstadosPedido");

            migrationBuilder.DropTable(
                name: "PuntosExpendio");

            migrationBuilder.DropTable(
                name: "EstadosDelivery");

            migrationBuilder.DropTable(
                name: "Barrios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "TiposDireccion");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "DetallesMesa");
        }
    }
}
