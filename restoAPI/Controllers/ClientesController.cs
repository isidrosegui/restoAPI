using System;
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
            return context.Clientes.Include("Direcciones.Barrio").Include("Direcciones.TipoDireccion").ToList();
        }

        [HttpGet("filtrado")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get([FromQuery]string telefono, [FromQuery] string nombre, [FromQuery]string apellido)
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            ActionResult<IEnumerable<Cliente>> accion = 
                await context.Clientes.Include("Direcciones.Barrio").Include("Direcciones.TipoDireccion").
                    Include(y => y.TipoCliente).
                        Include(y => y.TipoTelefono).
                        Where(x => (string.IsNullOrEmpty(telefono) || x.Telefono == telefono) &&
                    (string.IsNullOrEmpty(nombre) || x.Nombre.ToLower().StartsWith(nombre.ToLower())) &&
                        (string.IsNullOrEmpty(apellido) || x.Apellido.ToLower().StartsWith(apellido.ToLower()))).ToListAsync();
            return accion;
        }


        [HttpGet("{id}", Name = "ObtenerClienteById")]
        public async Task<ActionResult<Cliente>> Get(Int16 id)
        {
            var cliente = await context.Clientes.Include("Direcciones.Barrio").Include("Direcciones.TipoDireccion").FirstOrDefaultAsync(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Cliente value)
        {
            try
            {
                Cliente cli = new Cliente();
                context.Entry(cli).CurrentValues.SetValues(value);
                cli.Direcciones = new List<Direccion>();

                foreach(Direccion d in value.Direcciones)
                {
                    Direccion di = new Direccion()
                    {
                        Barrio = d.Barrio,
                        TipoDireccion = d.TipoDireccion,
                        Calle = d.Calle,
                        NroPuerta = d.NroPuerta,
                        Piso =d.Piso,
                        Dto = d.Dto,
                        Observacion =d.Observacion,
                        EsDefault = d.EsDefault,
                        FechaAlta =d.FechaAlta
                    };
                    cli.Direcciones.Add(di);
                }
                

                cli.TipoCliente = value.TipoCliente;
                cli.TipoTelefono= value.TipoTelefono;
                await context.Clientes.AddAsync(cli);
                
                context.Entry(cli.TipoCliente).State = EntityState.Detached;
                context.Entry(cli.TipoTelefono).State = EntityState.Detached;
                
                for (int j = 0; j < cli.Direcciones.Count; j++)
                {
                    context.Entry(cli.Direcciones[j]).State = EntityState.Added;
                    context.Entry(cli.Direcciones[j].Barrio).State = EntityState.Detached;
                    context.Entry(cli.Direcciones[j].TipoDireccion).State = EntityState.Detached;
                    
                }
                //context.Entry(value.Direcciones).State = EntityState.Detached;
                context.SaveChanges();
                return new CreatedAtRouteResult("ObtenerClienteById", new { id = value.Id }, value);
            }catch(Exception ex)
            {
               return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Int16 id, [FromBody] Cliente value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            try
            {
                var cliExistente = await context.Clientes.Where(p => p.Id == value.Id)
                    .Include(p => p.TipoCliente).Include(p => p.TipoTelefono).Include("Direcciones.Barrio").Include("Direcciones.TipoDireccion").SingleOrDefaultAsync();

                context.Entry(cliExistente).CurrentValues.SetValues(value);

                if (id != value.Id)
                {
                    return BadRequest();
                }
                //Modifico las direcciones existentes.
                foreach(Direccion d in cliExistente.Direcciones)
                {
                    var dir = value.Direcciones.First(x => x.Id == d.Id);
                    context.Entry(d).CurrentValues.SetValues(dir);
                    d.Barrio = await context.Barrios.Where(p => p.Id == dir.Barrio.Id).FirstAsync();
                    d.TipoDireccion = await context.TiposDireccion.Where(p => p.Id == dir.TipoDireccion.Id).FirstAsync();

                }

                if (value.Direcciones.Count != cliExistente.Direcciones.Count)
                {
                    int cant = value.Direcciones.Count;
                    //Actualizo el precio viejo (agrego la fecha de fin)
                    context.Entry(cliExistente.Direcciones[cant - 2]).CurrentValues.SetValues(value.Direcciones[cant - 2]);

                    //Agrego el nuevo precio
                    var d = new Direccion()
                    {
                        TipoDireccion = value.Direcciones[cant-1].TipoDireccion,
                        Barrio= value.Direcciones[cant - 1].Barrio,
                        Calle= value.Direcciones[cant - 1].Calle,
                        NroPuerta = value.Direcciones[cant - 1].NroPuerta,
                        Piso= value.Direcciones[cant - 1].Piso,
                        Dto= value.Direcciones[cant - 1].Dto,
                        EsDefault = value.Direcciones[cant - 1].EsDefault,
                        Observacion = value.Direcciones[cant - 1].Observacion
                    };
                    cliExistente.Direcciones.Add(d);
                    foreach(Direccion di in cliExistente.Direcciones)
                    {
                        d.TipoDireccion = await context.TiposDireccion.Where(p => p.Id == di.TipoDireccion.Id).FirstAsync();
                        d.Barrio= await context.Barrios.Where(p => p.Id == di.Barrio.Id).FirstAsync();
                    }
                }


                cliExistente.TipoCliente= await context.TiposCliente.Where(p => p.Id == value.TipoCliente.Id).FirstAsync();
                cliExistente.TipoTelefono = await context.TiposTelefono.Where(p => p.Id == value.TipoTelefono.Id).FirstAsync();
                
                await context.SaveChangesAsync();
                return Ok(cliExistente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

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
