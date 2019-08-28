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
    public class RepartidoresController : Controller
    {
        private readonly ApplicationDbContext context;
        public RepartidoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Repartidor>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Repartidores.Where(x=>x.FechaBaja==null).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerRepartidorById")]
        public ActionResult<Repartidor> Get(Int16 id)
        {
            var value = context.Repartidores.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Repartidor value)
        {
            context.Repartidores.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerRepartidorById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Repartidor value)
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
        public ActionResult<Repartidor> Delete(Int16 id)
        {
            var value = context.Repartidores.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Repartidores.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
