using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restoAPI.Context;
using restoAPI.Entities;

namespace restoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArqueoCajasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ArqueoCajasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArqueoCaja>> Get()
        {
            return context.ArqueoCajas.Include(x=>x.Detalles).ThenInclude(y=>y.FormaPago).Where(x => x.FechaBaja == null).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerArqueoCajaById")]
        public ActionResult<ArqueoCaja> Get(Int16 id)
        {
            var value = context.ArqueoCajas.Include(x => x.Detalles).ThenInclude(y => y.FormaPago).FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] ArqueoCaja value)
        {
            context.ArqueoCajas.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerArqueoCajaById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] ArqueoCaja value)
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
        public ActionResult<ArqueoCaja> Delete(Int16 id)
        {
            var value = context.ArqueoCajas.Include(x => x.Detalles).ThenInclude(y => y.FormaPago).FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.ArqueoCajas.Remove(value);
            context.SaveChanges();
            return value;

        }


    }
}