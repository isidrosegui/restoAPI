﻿using System;
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
    public class EstadosPedidoController : Controller
    {
        private readonly ApplicationDbContext context;
        public EstadosPedidoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EstadoPedido>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.EstadosPedido.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerEstadoPedidoById")]
        public ActionResult<EstadoPedido> Get(Int16 id)
        {
            var value = context.EstadosPedido.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] EstadoPedido value)
        {
            context.EstadosPedido.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerEstadoPedidoById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] EstadoPedido value)
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
        public ActionResult<EstadoPedido> Delete(Int16 id)
        {
            var value = context.EstadosPedido.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.EstadosPedido.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
