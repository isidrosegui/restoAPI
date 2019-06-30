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
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext context;
        public PedidosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pedido>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return context.Pedidos.ToList();
        }

        [HttpGet("filtrado")]
        public async Task<ActionResult<IEnumerable<Pedido>>> Get([FromQuery]int idEstado)
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            return await context.Pedidos.Include(x=>x.PuntoExpendio).Include(x=>x.Direccion).Include(x=>x.Cliente).Include(x=>x.ListaComandas).Include(x=>x.Cobros).Include(x=>x.EstadoPedido).Where(x=>x.EstadoPedido.Id==idEstado).ToListAsync();

        }

        [HttpGet("modificables")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAbiertos()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            List<int> ids = new List<int>();
            
            List<Pedido> listaPedidos = await context.Pedidos.Include(x => x.PuntoExpendio).Include("Direccion.Barrio").Include("Direccion.TipoDireccion").Include(x=>x.ListaComandas).Include(x => x.Cliente).Include(x => x.Cobros).Include(x => x.EstadoPedido).Where(x => x.EstadoPedido.Id<4).ToListAsync();
            
            foreach(Pedido p in listaPedidos)
            {
                p.DetallesPedido = new List<DetallePedido>();
                for (int i =0; i< p.ListaComandas.Count;i++)
                {
                    p.ListaComandas[i] = await context.Comandas.Include("Detalles.Producto").Include("Detalles.Producto.HistoPrecios").Where(x=>x.Id == p.ListaComandas[i].Id).FirstOrDefaultAsync();
                    foreach(DetallePedido d in p.ListaComandas[i].Detalles)
                    {
                        d.Producto.PrecioActual = d.Producto.HistoPrecios.FirstOrDefault(x => x.FechaBaja == null);

                        p.DetallesPedido.Add(d);

                    }
                }
            }
            
            return listaPedidos;
        }




        [HttpGet("{id}", Name = "ObtenerPedidoById")]
        public ActionResult<Pedido> Get(Int16 id)
        {
            var value = context.Pedidos.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Pedido value)
        {
            try
            {
                Pedido p = new Pedido();
                context.Entry(p).CurrentValues.SetValues(value);
                p.NroPedido = Convert.ToInt16((context.Pedidos.Where(x => x.FechaAlta == DateTime.Now.Date).Count()) + 1);
                p.Direccion = value.Direccion;
                value.Cliente.Direcciones = null;
                
                p.Cliente = value.Cliente;
                
                p.ListaComandas = new List<Comanda>();

                foreach (Comanda c in value.ListaComandas)
                {
                    Comanda co = new Comanda();
                    context.Entry(co).CurrentValues.SetValues(c);
                    co.NroComanda = Convert.ToInt16((context.Comandas.Where(x => x.FechaComanda == DateTime.Now.Date).Count()) + 1);
                    co.Detalles = new List<DetallePedido>();
                    foreach (DetallePedido d in c.Detalles)
                    {
                        d.Producto.HistoPrecios = null;
                        DetallePedido de = new DetallePedido();
                        context.Entry(de).CurrentValues.SetValues(d);
                        de.Producto = d.Producto;
                        co.Detalles.Add(de);
                       
                    }

                    p.ListaComandas.Add(co);
                }
                await context.Pedidos.AddAsync(p);
                context.Entry(p.Direccion).State = EntityState.Detached;
                context.Entry(p.Direccion.Barrio).State = EntityState.Detached;
                context.Entry(p.Direccion.TipoDireccion).State = EntityState.Detached;
                context.Entry(p.Cliente).State = EntityState.Detached;
                context.Entry(p.Cliente.TipoCliente).State = EntityState.Detached;
                context.Entry(p.Cliente.TipoTelefono).State = EntityState.Detached;

                for (int j = 0; j < p.ListaComandas.Count; j++)
                {
                    context.Entry(p.ListaComandas[j]).State = EntityState.Added;
                    for (int i = 0; i< p.ListaComandas[j].Detalles.Count; i++)
                    {
                        context.Entry(p.ListaComandas[j].Detalles[i].Producto).State = EntityState.Detached;
                        context.Entry(p.ListaComandas[j].Detalles[i].Producto.TipoDeProducto).State = EntityState.Detached;
                        context.Entry(p.ListaComandas[j].Detalles[i].Producto.PrecioActual).State = EntityState.Detached;
                        //context.Entry(p.ListaComandas[j].Detalles[i].Producto.HistoPrecios).State = EntityState.Detached;


                    }
                }
                    
                context.SaveChanges();
                return new CreatedAtRouteResult("ObtenerPedidoById", new { id = value.Id }, value);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Int16 id, [FromBody] Pedido value)
        {
            var pedidoExistente = await context.Pedidos.Include("ListaComandas.Detalles").FirstOrDefaultAsync(x => x.Id == value.Id);
            if (id != value.Id)
            {
                return BadRequest();
            }
            //Modifico las comandas existentes
            foreach(Comanda c in pedidoExistente.ListaComandas)
            {
                Comanda comModif = new Comanda();



                comModif = value.ListaComandas.FirstOrDefault(x => x.Id == c.Id);
                context.Entry(c).CurrentValues.SetValues(comModif);
                for (int i=0; i< c.Detalles.Count; i++)
                {
                    c.Detalles[i].Producto = comModif.Detalles[i].Producto;
                    c.Detalles[i].Cantidad = comModif.Detalles[i].Cantidad;
                    c.Detalles[i].Descuento= comModif.Detalles[i].Descuento;
                    c.Detalles[i].Subtotal= comModif.Detalles[i].Subtotal;
                    c.Detalles[i].FechaBaja = comModif.Detalles[i].FechaBaja;
                    c.Detalles[i].HoraBaja= comModif.Detalles[i].HoraBaja;
                    context.Entry(c.Detalles[i].Producto).State = EntityState.Detached;
                    context.Entry(c.Detalles[i]).State = EntityState.Modified;

                }
            }

            //Agrego las nuevas comandas
            List<Comanda> nuevasComandas = new List<Comanda>();
            nuevasComandas =  value.ListaComandas.Where(p => p.Id == 0).ToList();
            foreach(Comanda c in nuevasComandas)
            { 
                pedidoExistente.ListaComandas.Add(c.ShallowCopy());
                context.Entry(pedidoExistente.ListaComandas[pedidoExistente.ListaComandas.Count - 1]).State = EntityState.Added;
                
            }
  
            context.Entry(value).State = EntityState.Modified;
            

            context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult<Pedido> Delete(Int16 id)
        {
            var value = context.Pedidos.FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Pedidos.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
