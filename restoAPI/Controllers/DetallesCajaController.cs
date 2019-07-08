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
    public class DetallesCajaController : Controller
    {
        private readonly ApplicationDbContext context;
        public DetallesCajaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DetalleCaja>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.DetallesCaja.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerDetalleCajaById")]
        public ActionResult<DetalleCaja> Get(Int16 id)
        {
            var value = context.DetallesCaja.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] DetalleCaja value)
        {
            context.DetallesCaja.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerDetalleCajasById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] DetalleCaja value)
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
        public ActionResult<DetalleCaja> Delete(Int16 id)
        {
            var value = context.DetallesCaja.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.DetallesCaja.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
