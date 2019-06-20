using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restoAPI.Context;
using restoAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public DireccionesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Direccion>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Direcciones.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerDireccionById")]
        public ActionResult<Direccion> Get(Int16 id)
        {
            var direccion = context.Direcciones.FirstOrDefault(x => x.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }
            return direccion;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Direccion direccion)
        {
            context.Direcciones.Add(direccion);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerDireccionById", new { id = direccion.Id }, direccion);
        }


        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Direccion value)
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
        public ActionResult<Direccion> Delete(Int16 id)
        {
            var direccion = context.Direcciones.Include(c => c.Barrio).Include(c=> c.TipoDireccion).FirstOrDefault(x => x.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }
            context.Direcciones.Remove(direccion);
            context.SaveChanges();
            return direccion;

        }
    }
}
