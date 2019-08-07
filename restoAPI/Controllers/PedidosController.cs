using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restoAPI.Context;
using restoAPI.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace restoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext context;
        public PedidosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pedido>> Get()
        {
            
            return context.Pedidos.Include(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i => i.PrecioActual)
                    .Include(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto)
                    .Include(g=>g.Cobros).ThenInclude(t=>t.FormaPago)
                    .Include(i=>i.EstadoPedido).ToList();
        }

        [HttpGet("filtrado")]
        public async Task<ActionResult<IEnumerable<Pedido>>> Get([FromQuery]string idEstado1, [FromQuery]string idEstado2, [FromQuery] string idPuntoExpendio)
        {
            
            var lista = await context.Pedidos.Include(x=>x.PuntoExpendio).Include(x=>x.Direccion).
                Include(x=>x.Cliente).Include(x=>x.ListaComandas).Include(x=>x.Cobros).Include(x=>x.EstadoPedido)
                  .Include(g => g.Cobros).ThenInclude(t => t.FormaPago).
                    Where(x=> ((string.IsNullOrEmpty(idEstado1) || x.EstadoPedido.Id==Convert.ToInt32(idEstado1)) ||
                        (string.IsNullOrEmpty(idEstado2) || x.EstadoPedido.Id == Convert.ToInt32(idEstado2))) &&
                            (string.IsNullOrEmpty(idPuntoExpendio) || x.PuntoExpendio.Id == Convert.ToInt32(idPuntoExpendio))).ToListAsync();
            string comandas = "";
            foreach(Pedido p in lista)
            {
                foreach (Comanda c in p.ListaComandas)
                {
                    comandas = comandas + c.Id + ",";
                }
                comandas = comandas.Substring(0, comandas.Length - 1);
                var detalles = context.DetallesPedido.FromSql("select * from DetallesPedido where ComandaId in (" + comandas + ")").ToList();
                p.DetallesPedido = new List<DetallePedido>();
                p.DetallesPedido.AddRange(detalles);
            }
            context.DetallesPedido.FromSql("select * from DetallesPedido where IdPedido = (" + comandas + ")").ToList();

            return lista;
        }

        [HttpGet("modificables")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAbiertos()
        {
            try
            {
                
                List<int> ids = new List<int>();

                List<Pedido> listaPedidos = await context.Pedidos.Include(x => x.PuntoExpendio)
                    .Include("Direccion.Barrio").Include("Direccion.TipoDireccion")
                    .Include(x => x.ListaComandas).ThenInclude(c=>c.Detalles).Include(x => x.Cliente)
                    .Include(g => g.Cobros).ThenInclude(t => t.FormaPago).
                    Include(x => x.EstadoPedido).Where(x => x.EstadoPedido.Id <=8  && x.IdDetalleMesa == null).ToListAsync();


                foreach (Pedido p in listaPedidos)
                {
                    p.DetallesPedido = new List<DetallePedido>();
                    for (int i = 0; i < p.ListaComandas.Count; i++)
                    {
                        p.ListaComandas[i] = await context.Comandas.Include(x => x.Detalles).ThenInclude(y => y.Producto).ThenInclude(z => z.HistoPrecios).Where(x => x.Id == p.ListaComandas[i].Id).FirstOrDefaultAsync();
                        p.ListaComandas[i].Detalles = p.ListaComandas[i].Detalles.Where(x => x.FechaBaja == null).ToList();
                        foreach (DetallePedido d in p.ListaComandas[i].Detalles)
                        {
                            d.Producto.PrecioActual = d.Producto.HistoPrecios.FirstOrDefault(x => x.FechaBaja == null);

                            p.DetallesPedido.Add(d);

                        }
                    }
                }

                return listaPedidos;
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("modificablesNoMesa")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetModificablesNoMesa()
        {
            
            
            try
            {
                List<int> ids = new List<int>();
                //TODO: Ver el estado pedido que estoy buscando
                List<Pedido> listaPedidos = await context.Pedidos.Include(x => x.PuntoExpendio).Include("Direccion.Barrio")
                    .Include("Direccion.TipoDireccion").Include(x => x.ListaComandas).Include(x => x.Cliente).Include(x => x.Cobros).ThenInclude(x=>x.FormaPago)
                    .Include(x => x.EstadoPedido).Where(x => (x.EstadoPedido.Id <=8) && x.IdDetalleMesa==null && x.FechaBaja==null).ToListAsync();


                foreach (Pedido p in listaPedidos)
                {
                    p.DetallesPedido = new List<DetallePedido>();
                    for (int i = 0; i < p.ListaComandas.Count; i++)
                    {
                        p.ListaComandas[i] = await context.Comandas.Include("Detalles.Producto").
                            Include("Detalles.Producto.HistoPrecios").Where(x => x.Id == p.ListaComandas[i].Id).FirstOrDefaultAsync();
                        foreach (DetallePedido d in p.ListaComandas[i].Detalles)
                        {
                            d.Producto.PrecioActual = d.Producto.HistoPrecios.FirstOrDefault(x => x.FechaBaja == null);

                            p.DetallesPedido.Add(d);

                        }
                    }
                }

                return listaPedidos;


                //List<Pedido> listaPedidos = await context.Pedidos.Include(x => x.PuntoExpendio).
                //    Include(x => x.Direccion).ThenInclude(y => y.Barrio).Include(x => x.Direccion).ThenInclude(y => y.TipoDireccion)
                //    .Include(w => w.ListaComandas).ThenInclude(h=>h.Detalles).ThenInclude(i=>i.Producto).ThenInclude(g=>g.TipoDeProducto)
                //    .Include(w => w.ListaComandas).ThenInclude(h => h.Detalles).ThenInclude(i => i.Producto).ThenInclude(g => g.HistoPrecios)
                //    .Include(x => x.Cliente).Include(x => x.Cobros).Include(x => x.EstadoPedido)
                //    .Where(x => x.EstadoPedido.Id < 4 && x.IdDetalleMesa == null || x.IdDetalleMesa == 0).ToListAsync();

                //foreach (Pedido p in listaPedidos)
                //{
                //    p.DetallesPedido = new List<DetallePedido>();
                //    foreach (Comanda c in p.ListaComandas)
                //    {
                //        foreach (DetallePedido d in c.Detalles)
                //        {
                //            d.Producto.PrecioActual = d.Producto.HistoPrecios.FirstOrDefault(x => x.FechaBaja == null);

                //            p.DetallesPedido.Add(d);

                //        }
                //    }
                //}

                //return listaPedidos;
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("{id}", Name = "ObtenerPedidoById")]
        public ActionResult<Pedido> Get(Int16 id)
        {
            var value = context.Pedidos.Include(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i => i.PrecioActual)
                    .Include(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i => i.TipoDeProducto)
                    .Include(x=>x.Cobros).ThenInclude(y=>y.FormaPago).FirstOrDefault(x => x.Id == id);

            var detalles = context.DetallesPedido.FromSql("select * from DetallesPedido where IdPedido = " + id).ToList();
            value.DetallesPedido = new List<DetallePedido>();
            value.DetallesPedido.AddRange(detalles);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> Post([FromBody] Pedido value)
        {
            try
            {
                Pedido p = new Pedido();
                context.Entry(p).CurrentValues.SetValues(value);
                p.NroPedido = Convert.ToInt16((context.Pedidos.Where(x => x.FechaAlta.Value.Date.Day == DateTime.Now.Date.Day).Count()) + 1);
                if (value.Cliente != null)
                {
                    p.Direccion = value.Direccion;
                    value.Cliente.Direcciones = null;
                }
               
                p.EstadoPedido = await context.EstadosPedido.FirstAsync(x => x.Id == 1);
                p.Cliente = value.Cliente;
                p.PuntoExpendio = value.PuntoExpendio;
                p.ListaComandas = new List<Comanda>();
               
                foreach (Comanda c in value.ListaComandas)
                {
                    Comanda co = new Comanda();
                    context.Entry(co).CurrentValues.SetValues(c);
                    co.NroComanda = Convert.ToInt16((context.Comandas.Where(x => x.FechaComanda.Day == DateTime.Now.Date.Day).Count()) + 1);
                    co.Detalles = new List<DetallePedido>();
                    foreach (DetallePedido d in c.Detalles)
                    {
                        d.Producto.HistoPrecios = null;
                        DetallePedido de = new DetallePedido();
                        context.Entry(de).CurrentValues.SetValues(d);
                        de.Producto = context.Productos.Include(h=>h.TipoDeProducto).FirstOrDefault(x => x.Id == d.Producto.Id);
                        co.Detalles.Add(de);
                    }

                    p.ListaComandas.Add(co);
                }
                await context.Pedidos.AddAsync(p);
                if (p.Direccion != null)
                {
                    context.Entry(p.Direccion).State = EntityState.Detached;
                    context.Entry(p.Direccion.Barrio).State = EntityState.Detached;
                    context.Entry(p.Direccion.TipoDireccion).State = EntityState.Detached;
                    context.Entry(p.Cliente).State = EntityState.Detached;
                    context.Entry(p.Cliente.TipoCliente).State = EntityState.Detached;
                    context.Entry(p.Cliente.TipoTelefono).State = EntityState.Detached;
                }
                if(p.PuntoExpendio!=null)
                    context.Entry(p.PuntoExpendio).State = EntityState.Detached;

                for (int j = 0; j < p.ListaComandas.Count; j++)
                {
                    context.Entry(p.ListaComandas[j]).State = EntityState.Added;
                    for (int i = 0; i< p.ListaComandas[j].Detalles.Count; i++)
                    {
                        context.Entry(p.ListaComandas[j].Detalles[i].Producto).State = EntityState.Detached;
                        context.Entry(p.ListaComandas[j].Detalles[i].Producto.TipoDeProducto).State = EntityState.Detached;
                       // context.Entry(p.ListaComandas[j].Detalles[i].Producto.PrecioActual).State = EntityState.Detached;
                        //context.Entry(p.ListaComandas[j].Detalles[i].Producto.HistoPrecios).State = EntityState.Detached;


                    }
                }
                    
                context.SaveChanges();
                
                var created = new CreatedAtRouteResult("ObtenerPedidoById", new { id = p.Id }, p);
                if (((Pedido)created.Value).ListaComandas[0].Detalles != null && ((Pedido)created.Value).ListaComandas[0].Detalles[0].IdPedido == 0)
                {
                    foreach (DetallePedido d in ((Pedido)created.Value).ListaComandas[0].Detalles)
                    {
                        d.IdPedido = ((Pedido)created.Value).Id;
                        context.Entry(d).State = EntityState.Modified;

                    }
                }
                context.SaveChanges();

                return created;
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Int16 id, [FromBody] Pedido value)
        {
            try
            {
                var pedidoExistente = await context.Pedidos.Include(x => x.ListaComandas).ThenInclude(z=>z.Detalles).ThenInclude(p=>p.Producto).FirstOrDefaultAsync(x => x.Id == value.Id);
                pedidoExistente.MontoTotal = value.MontoTotal;
                    
                if (pedidoExistente.ListaComandas == null)
                {
                    pedidoExistente.ListaComandas = new List<Comanda>();
                }
                if (id != value.Id)
                 {
                     return BadRequest();
                 }

                
                //Hago las comandas nuevas
                foreach (Comanda c in value.ListaComandas.Where(x => x.Id == 0))
                {
                    Comanda com = new Comanda();
                    com.PedidoId = value.Id;
                    com.NroComanda = Convert.ToInt16((context.Comandas.Where(x => x.FechaComanda.Date.Day == DateTime.Now.Date.Day).Count()) + 1);
                    com.FechaComanda = DateTime.Now.Date;
                    com.HoraComanda = DateTime.Now.TimeOfDay;
                    com.Detalles = new List<DetallePedido>();
                    com.Observaciones = c.Observaciones;
                    //creo los detalles para la comanda nueva
                    foreach (DetallePedido d in c.Detalles)
                    {
                        DetallePedido dn = new DetallePedido();
                        dn.Cantidad = d.Cantidad;
                        dn.Descuento = d.Descuento;
                        dn.FechaBaja = d.FechaBaja;
                        dn.HoraBaja = d.HoraBaja;
                        dn.Producto = await context.Productos.Where(p => p.Id == d.Producto.Id).FirstAsync();
                        dn.Subtotal = d.Subtotal;
                        dn.IdPedido = value.Id;
                        dn.PrecioUnitario = d.PrecioUnitario;
                        com.Detalles.Add(dn);
                    }
                    context.Comandas.Add(com);
                    //ERROR TIPO DE PRODUCTO DETACHED.
                    context.SaveChanges();
                    
                }


                //modifico las comandas existentes
                foreach (Comanda c in value.ListaComandas.Where(x => x.Id != 0))
                {
                    Comanda com = context.Comandas.Where(x => x.Id == c.Id).Include(x=>x.Detalles).FirstOrDefault();
                    com.Observaciones = c.Observaciones;
                    //creo los detalles nuevos
                    foreach (DetallePedido d in c.Detalles.Where(x=>x.Id==0))
                    {
                        DetallePedido dn = new DetallePedido();
                        dn.Cantidad = d.Cantidad;
                        dn.Descuento = d.Descuento;
                        dn.FechaBaja = d.FechaBaja;
                        dn.HoraBaja = d.HoraBaja;
                        dn.Producto = await context.Productos.Where(p => p.Id == d.Producto.Id).FirstAsync();
                        dn.Subtotal = d.Subtotal;
                        dn.IdPedido = value.Id;
                        com.Detalles.Add(dn);
                        context.Entry(dn).State = EntityState.Added;
                    }
                   // modifico los existentes
                    foreach (DetallePedido d in c.Detalles.Where(x => x.Id != 0))
                    {
                       DetallePedido det = com.Detalles.Where(x => x.Id == d.Id).First();
                        context.Entry(det).CurrentValues.SetValues(d);
                        context.Entry(det).State = EntityState.Modified;
                    }
                    context.Entry(com).State = EntityState.Modified;
                    //ERROR TIPO DE PRODUCTO DETACHED.
                    
                    context.SaveChanges();

                }


                context.SaveChanges();
               pedidoExistente= context.Pedidos.Include(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i => i.PrecioActual)
                    .Include(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i => i.TipoDeProducto).FirstOrDefault(x => x.Id == id);
                return Ok(pedidoExistente);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<Pedido> Delete(Int16 id)
        {
            var value = context.Pedidos.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Pedidos.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
