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
            var value = context.Comandas.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

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
                Comanda ce = await context.Comandas.Include("Detalles.Producto").FirstOrDefaultAsync(x => x.Id == id);
                //modifico los detalles existentes.
                for (int i = 0; i < ce.Detalles.Count(); i++)
                {
                    DetallePedido det = value.Detalles.FirstOrDefault(x => x.Id == ce.Detalles[i].Id).ShallowCopy();
                    ce.Detalles[i].Producto = det.Producto;
                    ce.Detalles[i].Cantidad = det.Cantidad;
                    ce.Detalles[i].Descuento = det.Descuento;
                    ce.Detalles[i].Subtotal = det.Subtotal;
                    ce.Detalles[i].FechaBaja = det.FechaBaja;
                    ce.Detalles[i].HoraBaja = det.HoraBaja;
                    context.Entry(ce.Detalles[i].Producto).State = EntityState.Detached;
                    context.Entry(ce.Detalles[i]).State = EntityState.Modified;
                    //ce.Detalles[i] = 
                    
                }
                List<DetallePedido> detallesNuevos = value.Detalles.Where(x => !ce.Detalles.Any(x1 => x1.Id == x.Id)).ToList();
                if (detallesNuevos != null && detallesNuevos.Count > 0)
                {
                    //foreach (DetallePedido d in detallesNuevos)
                    //{
                    //    DetallePedido det = new DetallePedido();
                    //    det = d.ShallowCopy();
                    //    ce.Detalles.Add(det);
                    //}

                }
                if (id != value.Id)
                {
                    return BadRequest();
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
