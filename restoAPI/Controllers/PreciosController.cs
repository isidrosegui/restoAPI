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
    public class PreciosController : Controller
    {
        private readonly ApplicationDbContext context;
        public PreciosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Precio>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Precios.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerPrecioById")]
        public ActionResult<Precio> Get(Int16 id)
        {
            var value = context.Precios.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Precio value)
        {
            context.Precios.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerPrecioById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Precio value)
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
        public ActionResult<Precio> Delete(Int16 id)
        {
            var value = context.Precios.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Precios.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
