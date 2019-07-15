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
    public class ComandasController : Controller
    {
        private readonly ApplicationDbContext context;
        public ComandasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Comanda>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Comandas.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerComandaById")]
        public ActionResult<Comanda> Get(Int16 id)
        {
            var value = context.Comandas.Include("Detalles.Producto").FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }
        
        //[HttpGet("byPedido/{idPedido}")]
        //public async Task<ActionResult<List<Comanda>>> GetByPedido(Int16 idPedido)
        //{
        //    var value = await context.Comandas.Include("Detalles.Producto").Where(x => x.Pedido.Id == idPedido).ToListAsync();
        //    if (value == null)
        //    {
        //        return NotFound();
        //    }
        //    return value;
        //}


        [HttpPost]
        public ActionResult Post([FromBody] Comanda value)
        {
            context.Comandas.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerComandaById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Int16 id, [FromBody] Comanda value)
        {
            try
            {

                if (id != value.Id)
                {
                    return BadRequest();
                }
                Comanda ce = await context.Comandas.Include("Detalles.Producto").FirstOrDefaultAsync(x => x.Id == id);

                List<DetallePedido> detallesNuevos = value.Detalles.Where(x => !ce.Detalles.Any(x1 => x1.Id == x.Id)).ToList();
                context.Entry(ce).CurrentValues.SetValues(value);
                DetallePedido det = new DetallePedido();
                foreach (DetallePedido d in detallesNuevos)
                {
                    DetallePedido dn = new DetallePedido();
                    dn.Cantidad = d.Cantidad;
                    dn.Descuento = d.Descuento;
                    dn.FechaBaja = d.FechaBaja;
                    dn.HoraBaja = d.HoraBaja;
                    dn.Producto = await context.Productos.Where(p => p.Id == d.Producto.Id).FirstAsync();
                    dn.Subtotal = d.Subtotal;
                    ce.Detalles.Add(dn);
                }

                context.Entry(ce).State = EntityState.Modified;

                context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex);
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<Comanda> Delete(Int16 id)
        {
            var value = context.Comandas.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Comandas.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
