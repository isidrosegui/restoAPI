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
    public class EstadosDeliveryController : Controller
    {
        private readonly ApplicationDbContext context;
        public EstadosDeliveryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EstadoDelivery>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.EstadosDelivery.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerEstadoDeliveryById")]
        public ActionResult<EstadoDelivery> Get(Int16 id)
        {
            var value = context.EstadosDelivery.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] EstadoDelivery value)
        {
            context.EstadosDelivery.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerEstadoDeliveryById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] EstadoDelivery value)
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
        public ActionResult<EstadoDelivery> Delete(Int16 id)
        {
            var value = context.EstadosDelivery.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.EstadosDelivery.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
