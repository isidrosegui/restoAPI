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
    public class DeliveriesController : Controller
    {
        private readonly ApplicationDbContext context;
        public DeliveriesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Delivery>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Deliveries.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerDeliveryById")]
        public ActionResult<Delivery> Get(Int16 id)
        {
            var value = context.Deliveries.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Delivery value)
        {
            context.Deliveries.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerDeliveryById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Delivery value)
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
        public ActionResult<Delivery> Delete(Int16 id)
        {
            var value = context.Deliveries.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Deliveries.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
