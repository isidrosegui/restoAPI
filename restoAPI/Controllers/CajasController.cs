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
    public class CajasController : Controller
    {
        private readonly ApplicationDbContext context;
        public CajasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Caja>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Cajas.Where(x => x.FechaBaja == null).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerCajaById")]
        public ActionResult<Caja> Get(Int16 id)
        {
            var value = context.Cajas.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Caja value)
        {
            context.Cajas.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerCajaById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Caja value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();

        }

        [HttpPut("abrir/{id}")]
        public ActionResult PutAbrir(Int16 id, [FromBody] Caja value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            DetalleCaja det = new DetalleCaja();
            det.MontoApertura = value.DetalleAbierto.MontoApertura;
            det.FechaApertura = DateTime.Now.Date;
            det.HoraApertura = DateTime.Now.TimeOfDay;
            context.DetallesCaja.Add(det);
            context.SaveChanges();
            value.DetalleAbierto = det;
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok(value);

        }

        [HttpDelete("{id}")]
        public ActionResult<Caja> Delete(Int16 id)
        {
            var value = context.Cajas.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Cajas.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
