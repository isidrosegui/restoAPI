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
    public class BarriosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        public BarriosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Barrio>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Barrios.Where(x => x.FechaBaja == null).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerBarrioById")]
        public ActionResult<Barrio> Get(Int16 id)
        {
            var value = context.Barrios.FirstOrDefault(x => x.Id == id);
            if(value==null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Barrio value)
        {
            context.Barrios.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerBarrioById", new { id= value.Id}, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Barrio value)
        {
            if(id != value.Id)
            {
                return BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult<Barrio> Delete(Int16 id)
        {
            var value = context.Barrios.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Barrios.Remove(value);
            context.SaveChanges();
            return value;

        }



    }
}
