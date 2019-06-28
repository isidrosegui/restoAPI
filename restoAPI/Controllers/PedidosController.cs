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
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Pedidos.ToList();
        }

        [HttpGet("filtrado")]
        public async Task<ActionResult<IEnumerable<Pedido>>> Get([FromQuery]int idEstado)
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return await context.Pedidos.Include(x=>x.DetallesPedido).Include(x=>x.PuntoExpendio).Include(x=>x.Direccion).Include(x=>x.Cliente).Include(x=>x.ListaComandas).Include(x=>x.Cobros).Include(x=>x.EstadoPedido).Where(x=>x.EstadoPedido.Id==idEstado).ToListAsync();

        }


        [HttpGet("{id}", Name = "ObtenerPedidoById")]
        public ActionResult<Pedido> Get(Int16 id)
        {
            var value = context.Pedidos.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Pedido value)
        {
            try
            {
                Pedido p = new Pedido();
                context.Entry(p).CurrentValues.SetValues(value);
                p.NroPedido = Convert.ToInt16((context.Pedidos.Where(x => x.FechaAlta == DateTime.Now.Date).Count()) + 1);
                p.Direccion = value.Direccion;
                value.Cliente.Direcciones = null;
                
                p.Cliente = value.Cliente;
                
                p.ListaComandas = new List<Comanda>();

                foreach (Comanda c in value.ListaComandas)
                {
                    Comanda co = new Comanda();
                    context.Entry(co).CurrentValues.SetValues(c);
                    co.NroComanda = Convert.ToInt16((context.Comandas.Where(x => x.FechaComanda == DateTime.Now.Date).Count()) + 1);
                    co.Detalles = new List<DetallePedido>();
                    foreach (DetallePedido d in c.Detalles)
                    {
                        d.Producto.HistoPrecios = null;
                        DetallePedido de = new DetallePedido();
                        context.Entry(de).CurrentValues.SetValues(d);
                        de.Producto = d.Producto;
                        co.Detalles.Add(de);
                       
                    }

                    p.ListaComandas.Add(co);
                }
                await context.Pedidos.AddAsync(p);
                context.Entry(p.Direccion).State = EntityState.Detached;
                context.Entry(p.Direccion.Barrio).State = EntityState.Detached;
                context.Entry(p.Direccion.TipoDireccion).State = EntityState.Detached;
                context.Entry(p.Cliente).State = EntityState.Detached;
                context.Entry(p.Cliente.TipoCliente).State = EntityState.Detached;
                context.Entry(p.Cliente.TipoTelefono).State = EntityState.Detached;

                for (int j = 0; j < p.ListaComandas.Count; j++)
                {
                    context.Entry(p.ListaComandas[j]).State = EntityState.Added;
                    for (int i = 0; i< p.ListaComandas[j].Detalles.Count; i++)
                    {
                        context.Entry(p.ListaComandas[j].Detalles[i].Producto).State = EntityState.Detached;
                        context.Entry(p.ListaComandas[j].Detalles[i].Producto.TipoDeProducto).State = EntityState.Detached;
                        context.Entry(p.ListaComandas[j].Detalles[i].Producto.PrecioActual).State = EntityState.Detached;
                        //context.Entry(p.ListaComandas[j].Detalles[i].Producto.HistoPrecios).State = EntityState.Detached;


                    }
                }
                    
                context.SaveChanges();
                return new CreatedAtRouteResult("ObtenerPedidoById", new { id = value.Id }, value);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Pedido value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();

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
