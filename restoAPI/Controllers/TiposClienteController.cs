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
    public class TiposClienteController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public TiposClienteController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TipoCliente>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.TiposCliente.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerTipoClienteById")]
        public ActionResult<TipoCliente> Get(Int16 id)
        {
            var tipoCliente = context.TiposCliente.FirstOrDefault(x => x.Id == id);
            if (tipoCliente == null)
            {
                return NotFound();
            }
            return tipoCliente;
        }

        [HttpPost]
        public ActionResult Post([FromBody] TipoCliente tipoCliente)
        {
            context.TiposCliente.Add(tipoCliente);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerTipoClienteById", new { id = tipoCliente.Id }, tipoCliente);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] TipoCliente value)
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
        public ActionResult<TipoCliente> Delete(Int16 id)
        {
            var tipoCliente = context.TiposCliente.FirstOrDefault(x => x.Id == id);
            if (tipoCliente == null)
            {
                return NotFound();
            }
            context.TiposCliente.Remove(tipoCliente);
            context.SaveChanges();
            return tipoCliente;

        }
    }
}
