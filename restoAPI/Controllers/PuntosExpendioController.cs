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
    public class PuntosExpendioController : Controller
    {
        private readonly ApplicationDbContext context;
        public PuntosExpendioController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PuntoExpendio>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.PuntosExpendio.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerPuntoExpendioById")]
        public ActionResult<PuntoExpendio> Get(Int16 id)
        {
            var value = context.PuntosExpendio.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] PuntoExpendio value)
        {
            context.PuntosExpendio.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerPuntoExpendioById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] PuntoExpendio value)
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
        public ActionResult<PuntoExpendio> Delete(Int16 id)
        {
            var value = context.PuntosExpendio.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.PuntosExpendio.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
