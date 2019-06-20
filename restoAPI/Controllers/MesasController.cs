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
    public class MesasController : Controller
    {
        private readonly ApplicationDbContext context;
        public MesasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Mesa>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Mesas.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerMesaById")]
        public ActionResult<Mesa> Get(Int16 id)
        {
            var value = context.Mesas.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Mesa value)
        {
            context.Mesas.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerMesaById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Mesa value)
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
        public ActionResult<Mesa> Delete(Int16 id)
        {
            var value = context.Mesas.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Mesas.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
