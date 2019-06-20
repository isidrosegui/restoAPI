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
        public ActionResult Put(Int16 id, [FromBody] Comanda value)
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
