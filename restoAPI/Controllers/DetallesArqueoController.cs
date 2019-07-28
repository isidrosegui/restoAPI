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
    public class DetallesArqueoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public DetallesArqueoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DetalleArqueo>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.DetallesArqueo.Where(x => x.FechaBaja == null).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerDetalleArqueoById")]
        public ActionResult<DetalleArqueo> Get(Int16 id)
        {
            var value = context.DetallesArqueo.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] DetalleArqueo value)
        {
            context.DetallesArqueo.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerDetalleArqueoById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] DetalleArqueo value)
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
        public ActionResult<DetalleArqueo> Delete(Int16 id)
        {
            var value = context.DetallesArqueo.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.DetallesArqueo.Remove(value);
            context.SaveChanges();
            return value;

        }

    }
}