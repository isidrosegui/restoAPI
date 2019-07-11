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
            List<Mesa> mesas = context.Mesas.Include(w=>w.DetalleAbierto).ThenInclude(x => x.Pedido).ThenInclude(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).ToList();


            return mesas;
        }

        [HttpGet("{id}", Name = "ObtenerMesaById")]
        public ActionResult<Mesa> Get(Int16 id)
        {
            var value = context.Mesas.Include(w => w.DetalleAbierto).ThenInclude(x => x.Pedido).ThenInclude(p => p.ListaComandas).ThenInclude(d => d.Detalles).ThenInclude(f => f.Producto).Where(s=>s.Id==id).First();
            if (value == null)
            {
                return NotFound();
            }
            return value;
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
            context.Entry(value).State = EntityState.Modified;
            var mesa = context.Mesas.First(x => x.Id == value.Id);
            DetalleMesa detalleMesa = new DetalleMesa();
            detalleMesa.FechaApertura = DateTime.Now;
            detalleMesa.HoraApertura = DateTime.Now.TimeOfDay;
            
            detalleMesa.CantidadComensales = cantComensales;
            detalleMesa.IdMesa = value.Id;

            context.DetallesMesa.Add(detalleMesa);
            context.SaveChanges();
            mesa.DetalleAbierto = detalleMesa;
            context.Entry(mesa).State = EntityState.Modified;
            context.SaveChanges();
            Pedido pedido = new Pedido();
            pedido.FechaAlta = DateTime.Now;
            pedido.HoraAlta= DateTime.Now.TimeOfDay;
            pedido.NroPedido = Convert.ToInt16((context.Pedidos.Where(x => x.FechaAlta.Value.Date.Day == DateTime.Now.Date.Day).Count()) + 1);


            return Ok();

        }

        [HttpPut("cerrar")]
        public async Task<ActionResult> PutCerrar([FromQuery]Int16 id, [FromQuery] Int16 idDetalle, [FromBody] Mesa value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            var mesa = context.Mesas.First(x => x.Id == value.Id);
            DetalleMesa detalleMesa = await context.DetallesMesa.Include(x=>x.Pedido).FirstOrDefaultAsync(x=> x.Id==idDetalle);
            detalleMesa.FechaCierre = DateTime.Now;
            detalleMesa.HoraCierre = DateTime.Now.TimeOfDay;
            if (detalleMesa.Pedido != null)
            {
                detalleMesa.Pedido.EstadoPedido = await context.EstadosPedido.FirstOrDefaultAsync(x => x.Id == 10);
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
