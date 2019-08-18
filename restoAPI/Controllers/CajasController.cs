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
    public class CajasController : Controller
    {
        private readonly ApplicationDbContext context;
        public CajasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Caja>> Get()
        {

            try {
                //TODO: Luego vamos a ver como lo hacemos de forma asincronica
                var lista = context.Cajas.Include(x => x.Detalles).ThenInclude(y => y.Cobros).ThenInclude(h=>h.FormaPago).Where(x => x.FechaBaja == null).ToList();
                foreach(Caja  c in lista)
                {
                    c.DetalleAbierto = c.Detalles.FirstOrDefault(x => x.FechaCierre == null && x.FechaBaja==null);
                    c.Detalles = null;
                }

                return lista;
            } catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("sinArqueo")]
        public ActionResult<IEnumerable<Caja>> GetSinArqueo()
        {
            try
            {
                //TODO: Luego vamos a ver como lo hacemos de forma asincronica
                var listaCaja = context.Cajas.Include(x => x.Detalles).ThenInclude(y => y.Cobros).ThenInclude(h => h.FormaPago)
                    .Include(g=>g.Detalles).ThenInclude(t=>t.Cobros).Where(x => x.FechaBaja == null)
                    .Include(x=>x.Detalles).ThenInclude(a=>a.Arqueo).ThenInclude(r=>r.Detalles)
                    .Include(x => x.Detalles).ThenInclude(a => a.Arqueo).ThenInclude(r => r.Estado).ToList();
                

                return listaCaja;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}", Name = "ObtenerCajaById")]
        public ActionResult<Caja> Get(Int16 id)
        {
            var value = context.Cajas.Include(x => x.DetalleAbierto).ThenInclude(y => y.Cobros).ThenInclude(h => h.FormaPago).FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Caja value)
        {
            context.Cajas.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerCajaById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Caja value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;

            context.SaveChanges();
            return Ok();

        }

        [HttpPut("abrir/{id}")]
        public ActionResult PutAbrir(Int16 id, [FromBody] Caja value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            DetalleCaja det = new DetalleCaja();
            det.MontoApertura = value.DetalleAbierto.MontoApertura;
            det.FechaApertura = DateTime.Now.Date;
            det.HoraApertura = DateTime.Now.TimeOfDay;
            det.CajaId = value.Id;
            context.DetallesCaja.Add(det);
            context.SaveChanges();
            value.DetalleAbierto = det;
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok(value);

        }


        [HttpPut("cerrar/{id}")]
        public ActionResult PutCerrar(Int16 id, [FromBody] Caja value)
        {
           
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (id != value.Id)
                    {
                        return BadRequest();
                    }
                    DetalleCaja det = context.DetallesCaja.FirstOrDefault(x => x.Id == value.DetalleAbierto.Id);
                    det.HoraCierre = value.DetalleAbierto.HoraCierre;
                    det.FechaCierre = value.DetalleAbierto.FechaCierre;
                    det.MontoCierre = value.DetalleAbierto.Cobros.Where(v=>v.FechaBaja==null).Sum(x => x.Monto);
                    context.Entry(det).State = EntityState.Modified;
                    context.Entry(value).State = EntityState.Modified;
                    context.SaveChanges();
                    transaction.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(ex);
                }
                finally
                {
                    transaction.Dispose();
                }
            }
           
        }


        [HttpDelete("{id}")]
        public ActionResult<Caja> Delete(Int16 id)
        {
            var value = context.Cajas.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Cajas.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
