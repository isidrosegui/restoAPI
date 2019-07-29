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
    public class PagosController : Controller
    {

        private readonly ApplicationDbContext context;
        public PagosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pago>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Pagos.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerPagoById")]
        public ActionResult<Pago> Get(Int16 id)
        {
            var value = context.Pagos.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Pago value)
        {
            context.Pagos.Add(value);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerPagoById", new { id = value.Id }, value);
        }

        [HttpPost("cobrar")]
        public ActionResult PostCobrar([FromBody] List<Pago> value,[FromQuery] Int32 idPedido, [FromQuery] Int32 idDetalleCaja)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Pedido pedido = context.Pedidos.Include(g => g.Cobros).ThenInclude(t => t.FormaPago).FirstOrDefault(x => x.Id == idPedido);
                    DetalleCaja detalle = context.DetallesCaja.Include(g => g.Cobros).ThenInclude(t => t.FormaPago).FirstOrDefault(x => x.Id == idDetalleCaja);
                    context.Pagos.AddRange(value);



                    foreach (Pago p in value)
                    {
                        p.FormaPago = context.FormasPago.FirstOrDefault(x => x.Id == p.FormaPago.Id);
                        p.MarcaTarjeta = context.Tarjetas.FirstOrDefault(x => x.Id == p.MarcaTarjeta.Id);
                        p.Banco = context.Bancos.FirstOrDefault(x => x.Id == p.Banco.Id);
                        p.PedidoId = idPedido;
                        p.DetalleCajaId = idDetalleCaja;
                        p.FechaAlta = DateTime.Now.Date;
                        p.HoraAlta = DateTime.Now.TimeOfDay;
                    }
                    context.Pagos.AddRange(value);

                    List<Int32> idsCobros = new List<int>();

                    context.SaveChanges();
                    foreach (Pago p in value)
                    {
                        p.FormaPago = context.FormasPago.FirstOrDefault(x => x.Id == p.FormaPago.Id);
                        context.Entry(p.FormaPago).State = EntityState.Modified;
                    }

                    context.SaveChanges();
                    foreach (Pago p in value)
                    {
                        idsCobros.Add(p.Id);
                    }
                    pedido.Cobros.AddRange(context.Pagos.Where(t => idsCobros.Contains(t.Id)));
                    detalle.Cobros.AddRange(context.Pagos.Where(t => idsCobros.Contains(t.Id)));

                    if (pedido.Cobros.Sum(x => x.Monto) == pedido.MontoTotal)
                    {
                        //TODO:Ver bien cual es el estado que corresponde
                        if (value.Sum(x => x.Monto) == pedido.MontoTotal)
                            pedido.EstadoPedido = context.EstadosPedido.FirstOrDefault(x => x.Id == 4);
                        else if (value.Sum(x => x.Monto) < pedido.MontoTotal)
                            pedido.EstadoPedido = context.EstadosPedido.FirstOrDefault(x => x.Id == 8);
                    }
                    else
                    {
                        //TODO:Ver bien cual es el estado que corresponde
                        pedido.EstadoPedido = context.EstadosPedido.FirstOrDefault(x => x.Id == 8);
                    }
                    context.Entry(pedido).State = EntityState.Modified;
                    context.Entry(detalle).State = EntityState.Modified;

                    context.SaveChanges();
                    List<Pago> lista = context.Pagos.Where(t => idsCobros.Contains(t.Id)).ToList();
                    pedido.Cobros.AddRange(lista);
                    detalle.Cobros.AddRange(lista);
                    context.SaveChanges();
                    transaction.Commit();
                    return Ok(lista);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(ex);
                }
            }
        }



        [HttpPut("{id}")]
        public ActionResult Put(Int16 id, [FromBody] Pago value)
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
        public ActionResult<Pago> Delete(Int16 id)
        {
            var value = context.Pagos.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Pagos.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
