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
    public class BancosController : Controller
    {
       private readonly ApplicationDbContext context;
        public BancosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banco>>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return await context.Bancos.Where(x=>x.FechaBaja==null).ToListAsync();
        }

        [HttpGet("{id}", Name = "ObtenerBancosById")]
        public async Task<ActionResult<Banco>> Get(Int16 id)
        {
            var value = await context.Bancos.FirstOrDefaultAsync(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Banco banco)
        {
            context.Bancos.Add(banco);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("ObtenerBancosById", new { id = banco.Id }, banco);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Int16 id, [FromBody] Banco value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult<Banco> Delete(Int16 id)
        {
            var banco = context.Bancos.FirstOrDefault(x => x.Id == id);
            if (banco == null)
            {
                return NotFound();
            }
            context.Bancos.Remove(banco);
            context.SaveChanges();
            return banco;

        }

    }
}
