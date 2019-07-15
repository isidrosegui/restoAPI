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
                List<DetalleMesa> dets = context.DetallesMesa.
                      Include(x => x.Pedido).ThenInclude(p=>p.ListaComandas.Where(l=>l.FechaBaja ==null)).ThenInclude(d=>d.Detalles.Where(r=>r.FechaBaja ==null)).ThenInclude(f=>f.Producto).ToList();
                return dets;
        }

        [HttpGet("DetalleDeMesaAbierto")]
        public ActionResult<IEnumerable<DetalleMesa>> GetDetAbiertoOfMesa()
        {
            List<DetalleMesa> dets = context.DetallesMesa.Include(x => x.Pedido).ThenInclude(p => p.ListaComandas.Where(l => l.FechaBaja==null)).ThenInclude(d => d.Detalles.Where(r => r.FechaBaja == null)).ThenInclude(f => f.Producto).ToList();

            return dets;
        }

        [HttpGet("{id}", Name = "ObtenerDetalleMesaById")]
        public ActionResult<DetalleMesa> Get(Int16 id)
        {
            var value = context.DetallesMesa.Include(x=>x.Pedido).Include("Pedido.ListaComandas").FirstOrDefault(x => x.Id == id);
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
            DetalleMesa detalle = new DetalleMesa();
            if (id != value.Id)
            {
                return BadRequest();
            }
            detalle = context.DetallesMesa.FirstOrDefault(x => x.Id == value.Id);
            context.Entry(detalle).CurrentValues.SetValues(value);
            
            if (value.Pedido != null)
            {
                int noOfRowUpdated = context.Database.ExecuteSqlCommand("Update DetallesMesa set PedidoId = " + value.Pedido.Id);
            }

            context.Entry(detalle).State = EntityState.Modified;

            context.SaveChanges();
            return Ok();

        }

        [HttpPut("agregarPedido/{id}")]
        public ActionResult<DetalleMesa> PutAgregarPedido(Int16 id, [FromBody] DetalleMesa value)
        {


            if (value.Pedido != null)
            {
                int noOfRowUpdated = context.Database.ExecuteSqlCommand("Update DetallesMesa set PedidoId = " + value.Pedido.Id + "where Id ="+value.Id);
            }
            
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
