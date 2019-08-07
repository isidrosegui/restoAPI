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
    public class MesasController : Controller
    {
        private readonly ApplicationDbContext context;
        public MesasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Mesa>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            List<Mesa> mesas = context.Mesas
                .Include(w=>w.DetalleAbierto).ThenInclude(x => x.Pedido).ThenInclude(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i=>i.PrecioActual)
                    .Include(w => w.DetalleAbierto).ThenInclude(x => x.Pedido).ThenInclude(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i => i.TipoDeProducto).ToList();

            foreach (Mesa m in mesas.Where(x=>x.DetalleAbierto != null).ToList())
            {
                m.DetalleAbierto.Pedido.DetallesPedido = context.DetallesPedido.Where(x => x.IdPedido == m.DetalleAbierto.Pedido.Id && x.FechaBaja == null).ToList();
            }

            return mesas;
        }

        [HttpGet("{id}", Name = "ObtenerMesaById")]
        public ActionResult<Mesa> Get(Int16 id)
        {
            var value = context.Mesas.Include(w => w.DetalleAbierto).ThenInclude(x => x.Pedido).ThenInclude(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i => i.PrecioActual)
                    .Include(w => w.DetalleAbierto).ThenInclude(x => x.Pedido).ThenInclude(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i => i.TipoDeProducto).Where(s=>s.Id==id).First();
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpGet("abiertas")]
        public ActionResult<IEnumerable<Mesa>> GetAbiertas()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            List<Mesa> mesas = context.Mesas.Include(w => w.DetalleAbierto).
                ThenInclude(x => x.Pedido).ThenInclude(p => p.ListaComandas).
                ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).
                ThenInclude(i => i.PrecioActual).Include(w => w.DetalleAbierto).ThenInclude(x => x.Pedido).
                ThenInclude(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).
                ThenInclude(i => i.TipoDeProducto).Where(x=>x.EstaAbierta).ToList();

            foreach (Mesa m in mesas.Where(x => x.DetalleAbierto != null).ToList())
            {
                m.DetalleAbierto.Pedido.DetallesPedido = context.DetallesPedido.Where(x => x.IdPedido == m.DetalleAbierto.Pedido.Id && x.FechaBaja == null).ToList();

            }

            return mesas;
        }


        [HttpGet("porcobrar")]
        public ActionResult<IEnumerable<Mesa>> GetPorCobrar()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            List<Mesa> mesas = 
                context.Mesas.Include(w => w.DetalleAbierto).ThenInclude(x => x.Pedido).ThenInclude(p => p.ListaComandas).
                    ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i => i.PrecioActual).
                Include(w => w.DetalleAbierto).ThenInclude(x => x.Pedido).ThenInclude(p => p.ListaComandas).
                    ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ThenInclude(i => i.TipoDeProducto).
                Include(w => w.DetalleAbierto).ThenInclude(x => x.Pedido).ThenInclude(t=>t.Cobros).ThenInclude(w=>w.FormaPago)
                .Where(x => x.DetalleAbierto.Pedido.EstadoPedido.Id<8).ToList();

            foreach (Mesa m in mesas.Where(x => x.DetalleAbierto != null).ToList())
            {
                m.DetalleAbierto.Pedido.DetallesPedido = context.DetallesPedido.Where(x => x.IdPedido == m.DetalleAbierto.Pedido.Id && x.FechaBaja == null).ToList();

            }

            return mesas;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Mesa value)
        {
            value.EstaAbierta = false;
            value.FechaAlta = DateTime.Now;
            context.Mesas.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerMesaById", new { id = value.Id }, value);
        }



        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Mesa value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();

        }

        [HttpPut("abrir")]
        public ActionResult PutAbrir([FromQuery]Int16 id,[FromQuery] Int16 cantComensales, [FromBody] Mesa value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            //asigno los valoras de la mesa
            context.Entry(value).State = EntityState.Modified;
            var mesa = context.Mesas.First(x => x.Id == value.Id);
            //creo el detall de la mesa.
            DetalleMesa detalleMesa = new DetalleMesa();
            detalleMesa.FechaApertura = DateTime.Now;
            detalleMesa.HoraApertura = DateTime.Now.TimeOfDay;
            detalleMesa.CantidadComensales = cantComensales;
            detalleMesa.IdMesa = value.Id;
            //creo el pedido
            Pedido pedido = new Pedido();
            pedido.FechaAlta = DateTime.Now;
            pedido.HoraAlta = DateTime.Now.TimeOfDay;
            pedido.NroPedido = Convert.ToInt16((context.Pedidos.Where(x => x.FechaAlta.Value.Date.Day == DateTime.Now.Date.Day).Count()) + 1);
            pedido.EstadoPedido = context.EstadosPedido.FirstOrDefaultAsync(x => x.Id == 1).Result;
            
            //lo agrego al context;
            context.Pedidos.Add(pedido);
            context.SaveChanges();
            //agrego el pedido a la mesa
            detalleMesa.Pedido= pedido;
            context.DetallesMesa.Add(detalleMesa);
            context.SaveChanges();
            pedido.IdDetalleMesa = detalleMesa.Id;
            context.SaveChanges();
            mesa.DetalleAbierto = detalleMesa;
            context.Entry(mesa).State = EntityState.Modified;
            context.SaveChanges();


            return Ok();

        }

        [HttpPut("cerrar")]
        public async Task<ActionResult> PutCerrar([FromQuery]Int16 id, [FromQuery] Int16 idDetalle, [FromBody] Mesa value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            value.DetalleAbierto = null;
            context.Entry(value).State = EntityState.Modified;
            var mesa = context.Mesas.First(x => x.Id == value.Id);
            DetalleMesa detalleMesa = await context.DetallesMesa.Include(x=>x.Pedido).FirstOrDefaultAsync(x=> x.Id==idDetalle);
            detalleMesa.FechaCierre = DateTime.Now;
            detalleMesa.HoraCierre = DateTime.Now.TimeOfDay;
            if (detalleMesa.Pedido != null)
            {
                detalleMesa.Pedido.EstadoPedido = await context.EstadosPedido.FirstOrDefaultAsync(x => x.Id == 6);
                context.Entry(detalleMesa.Pedido).State = EntityState.Modified;
            }
            
            context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult<Mesa> Delete(Int16 id)
        {
            var value = context.Mesas.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Mesas.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
