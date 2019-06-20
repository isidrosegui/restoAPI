using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restoAPI.Context;
using restoAPI.Entities;

namespace restoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposDireccionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public TiposDireccionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TipoDireccion>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.TiposDireccion.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerTipoDireccionById")]
        public ActionResult<TipoDireccion> Get(Int16 id)
        {
            var tipoDireccion = context.TiposDireccion.FirstOrDefault(x => x.Id == id);
            if (tipoDireccion == null)
            {
                return NotFound();
            }
            return tipoDireccion;
        }

        [HttpPost]
        public ActionResult Post([FromBody] TipoDireccion tipoDireccion)
        {
            context.TiposDireccion.Add(tipoDireccion);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerTipoDireccionById", new { id = tipoDireccion.Id }, tipoDireccion);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] TipoDireccion value)
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
        public ActionResult<TipoDireccion> Delete(Int16 id)
        {
            var tipoDireccion = context.TiposDireccion.FirstOrDefault(x => x.Id == id);
            if (tipoDireccion == null)
            {
                return NotFound();
            }
            context.TiposDireccion.Remove(tipoDireccion);
            context.SaveChanges();
            return tipoDireccion;

        }
    }
}
