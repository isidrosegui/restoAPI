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
    public class EstadosArqueoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public EstadosArqueoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EstadoArqueo>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.EstadosArqueo.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerEstadoArqueoById")]
        public ActionResult<EstadoArqueo> Get(Int16 id)
        {
            var value = context.EstadosArqueo.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] EstadoArqueo value)
        {
            context.EstadosArqueo.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerEstadoArqueoById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] EstadoArqueo value)
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
        public ActionResult<EstadoArqueo> Delete(Int16 id)
        {
            var value = context.EstadosArqueo.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.EstadosArqueo.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}