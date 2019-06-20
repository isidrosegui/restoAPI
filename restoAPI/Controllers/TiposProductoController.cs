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
    public class TiposProductoController : Controller
    {
        private readonly ApplicationDbContext context;
        public TiposProductoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TipoProducto>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.TiposProducto.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerTipoProductoById")]
        public ActionResult<TipoProducto> Get(Int16 id)
        {
            var value = context.TiposProducto.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] TipoProducto value)
        {
            context.TiposProducto.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerTipoProductoById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] TipoProducto value)
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
        public ActionResult<TipoProducto> Delete(Int16 id)
        {
            var value = context.TiposProducto.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.TiposProducto.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
