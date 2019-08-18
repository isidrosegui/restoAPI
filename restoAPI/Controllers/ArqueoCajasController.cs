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
            return context.ArqueoCajas.Include(x=>x.Detalles.Where(h=>h.FechaBaja==null)).ThenInclude(y=>y.FormaPago).Where(x => x.FechaBaja == null).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerArqueoCajaById")]
        public ActionResult<ArqueoCaja> Get(Int16 id)
        {
            var value = context.ArqueoCajas.Include(x => x.Detalles.Where(h => h.FechaBaja == null)).ThenInclude(y => y.FormaPago).FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromQuery]Int32 IdDetalleCaja , [FromQuery]String CerrarCaja, [FromBody] ArqueoCaja value)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    value.Estado = context.EstadosArqueo.FirstOrDefault(x => x.Id == value.Estado.Id);
                    context.ArqueoCajas.Add(value);
                    context.Entry(value.Estado).State = EntityState.Detached;
                    DetalleCaja det = context.DetallesCaja.FirstOrDefault(x => x.Id == IdDetalleCaja);
                    foreach(DetalleArqueo d in value.Detalles)
                    {
                        context.Entry(d.FormaPago).State = EntityState.Detached;
                    }
                    
                    context.SaveChanges();

                    det.Arqueo = value;
                    
                    Caja caja = new Caja();
                    if (CerrarCaja == "si")
                    {
                        caja = context.Cajas.FirstOrDefault(x => x.Id == det.CajaId);
                        det.HoraCierre = DateTime.Now.TimeOfDay;
                        det.FechaCierre = DateTime.Now.Date;
                        det.MontoCierre = det.Cobros.Where(v=>v.FechaBaja==null).Sum(x => x.Monto);
                        caja.EstaAbierta = false;
                        context.Entry(caja).State = EntityState.Modified;
                    }
                    context.Entry(det).State = EntityState.Modified;
                    
                    context.SaveChanges();
                    transaction.Commit();
                    return new CreatedAtRouteResult("ObtenerArqueoCajaById", new { id = value.Id }, value);
                }catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(Int32 Id, [FromQuery]Int32 IdDetalleCaja, [FromQuery]String CerrarCaja, [FromBody] ArqueoCaja value)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    ArqueoCaja arqEx = context.ArqueoCajas.Include(x => x.Detalles).ThenInclude(y => y.FormaPago)
                        .Include(x => x.Estado).First(x => x.Id == Id);

                    arqEx.Estado = context.EstadosArqueo.FirstOrDefault(x => x.Id == value.Estado.Id);
                    
                    DetalleCaja det = context.DetallesCaja.FirstOrDefault(x => x.Id == IdDetalleCaja);

                    for(int i = 0; i < value.Detalles.Count; i++)
                    {
                        DetalleArqueo d = value.Detalles[i];
                        if(d.Id==0 || d.Id<0 )
                        {
                            arqEx.Detalles.Add(d);
                        }
                        else
                        {
                           DetalleArqueo deta = arqEx.Detalles.FirstOrDefault(x => x.Id == d.Id) ;
                           deta.FechaBaja = d.FechaBaja;
                            deta.HoraBaja = d.HoraBaja;
                           context.Entry(deta).State = EntityState.Modified;
                        }
                        context.Entry(d.FormaPago).State = EntityState.Detached;
                    }
                    context.Entry(value.Estado).State = EntityState.Detached;
                    context.SaveChanges();

                    Caja caja = new Caja();
                    if (CerrarCaja == "si")
                    {
                        caja = context.Cajas.FirstOrDefault(x => x.Id == det.CajaId);
                        det.HoraCierre = DateTime.Now.TimeOfDay;
                        det.FechaCierre = DateTime.Now.Date;
                        det.MontoCierre = det.Cobros.Where(v => v.FechaBaja == null).Sum(x => x.Monto);
                        caja.EstaAbierta = false;
                        context.Entry(caja).State = EntityState.Modified;
                    }
                    context.Entry(det).State = EntityState.Modified;

                    context.SaveChanges();
                    transaction.Commit();
                    return new CreatedAtRouteResult("ObtenerArqueoCajaById", new { id = value.Id }, value);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
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