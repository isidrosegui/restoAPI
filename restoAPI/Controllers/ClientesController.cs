﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restoAPI.Context;
using restoAPI.Entities;

namespace restoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ClientesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Clientes.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerClienteById")]
        public async Task<ActionResult<Cliente>> Get(Int16 id)
        {
            var cliente = await context.Clientes.Include(x=>x.Direcciones).FirstOrDefaultAsync(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Cliente cliente)
        {
            await context.Clientes.AddAsync(cliente);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerProductoById", new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Cliente value)
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
        public ActionResult<Cliente> Delete(Int16 id)
        {
            var cliente = context.Clientes.FirstOrDefault(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            context.Clientes.Remove(cliente);
            context.SaveChanges();
            return cliente;

        }
    }
}
