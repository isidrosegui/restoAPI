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
    public class DetallesMesaController : Controller
    {
        private readonly ApplicationDbContext context;
        public DetallesMesaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DetalleMesa>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.DetallesMesa.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerDetalleMesaById")]
        public ActionResult<DetalleMesa> Get(Int16 id)
        {
            var value = context.DetallesMesa.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] DetalleMesa value)
        {
            context.DetallesMesa.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerDetalleMesaById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] DetalleMesa value)
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
        public ActionResult<DetalleMesa> Delete(Int16 id)
        {
            var value = context.DetallesMesa.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.DetallesMesa.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
