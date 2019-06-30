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
    public class FormasPagoController : Controller
    {
        private readonly ApplicationDbContext context;
        public FormasPagoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FormaPago>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.FormasPago.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerFormaPagoById")]
        public ActionResult<FormaPago> Get(Int16 id)
        {
            var value = context.FormasPago.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FormaPago value)
        {
            try
            {
                context.FormasPago.Add(value);
                await context.SaveChangesAsync();
                return new CreatedAtRouteResult("ObtenerFormaPagoById", new { id = value.Id }, value);
            }
            catch(Exception ex)
            {
               return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] FormaPago value)
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
        public ActionResult<FormaPago> Delete(Int16 id)
        {
            var value = context.FormasPago.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.FormasPago.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
