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
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext context;
        public ProductosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            ActionResult<IEnumerable<Producto>> accion = await context.Productos.Include(y => y.HistoPrecios).Include(h => h.TipoDeProducto).ToListAsync();
            foreach(Producto p in accion.Value)
            {
                p.PrecioActual = p.HistoPrecios.First(x => x.FechaBaja == null);
            }
            return accion;
        }
        [HttpGet("filtrado")]
        public async Task<ActionResult<IEnumerable<Producto>>> Get([FromQuery]string idTipoProd, [FromQuery] string nombre, [FromQuery]string idEstado)
        {
            //TODO: Luego vamos a ver como lo hacemos de forma asincronica
            
            ActionResult<IEnumerable<Producto>> accion = await context.Productos.Include(y => y.HistoPrecios).Include(h => h.TipoDeProducto).Where
                (x => (string.IsNullOrEmpty(idTipoProd)  || x.TipoDeProducto.Id ==Convert.ToInt32(idTipoProd)) &&
                    (string.IsNullOrEmpty(nombre) || x.Nombre.ToLower().StartsWith(nombre.ToLower())) &&
                        ((string.IsNullOrEmpty(idEstado)) || idEstado=="2" || (idEstado == "1" && x.FechaBaja != null) || (idEstado == "0" && x.FechaBaja == null))).ToListAsync();
            foreach (Producto p in accion.Value)
            {
                p.PrecioActual = p.HistoPrecios.First(x => x.FechaBaja == null);
            }
            return accion;
        }

        [HttpGet("{id}", Name = "ObtenerProductoById")]
        public ActionResult<Producto> Get(Int16 id)
        {
            var value = context.Productos.Include(x=> x.PrecioActual).Include(y=> y.HistoPrecios).Include(h => h.TipoDeProducto).FirstOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Producto value)
        {
            if (value.PrecioActual.Id != 0)
            {
                context.Entry(value.PrecioActual).State = EntityState.Detached;
            }

            /*foreach (Precio p in value.HistoPrecios)
            {
                context.Entry(p).State = EntityState.Detached;
            }*/
            await context.Productos.AddAsync(value);
            context.Entry(value.TipoDeProducto).State = EntityState.Detached;
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerProductoById", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Int16 id, [FromBody] Producto value)
        {
            try
            {
                var prodExistente = await context.Productos.Where(p => p.Id == value.Id)
                    .Include(p => p.TipoDeProducto).Include(p => p.HistoPrecios).SingleOrDefaultAsync();

                context.Entry(prodExistente).CurrentValues.SetValues(value);

                if (id != value.Id)
                {
                    return BadRequest();
                }
                if (value.HistoPrecios.Count != prodExistente.HistoPrecios.Count)
                {
                    int cant = value.HistoPrecios.Count;
                    //Actualizo el precio viejo (agrego la fecha de fin)
                    context.Entry(prodExistente.HistoPrecios[cant - 2]).CurrentValues.SetValues(value.HistoPrecios[cant - 2]);
                    //Agrego el nuevo precio
                    var p = new Precio()
                    {
                        PrecioCosto = value.HistoPrecios[cant - 1].PrecioCosto,
                        PrecioPublico = value.HistoPrecios[cant - 1].PrecioPublico
                    };
                    prodExistente.HistoPrecios.Add((p));
                }
                prodExistente.TipoDeProducto = await context.TiposProducto.Where(p => p.Id == value.TipoDeProducto.Id).FirstAsync();
                //context.Entry(prodExistente.TipoDeProducto).CurrentValues.SetValues(await context.TiposProducto.Where(p => p.Id == value.TipoDeProducto.Id).FirstAsync());
                await context.SaveChangesAsync();
                return Ok(prodExistente);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto>> Delete(Int16 id)
        {
            var value = await context.Productos.FirstOrDefaultAsync(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            context.Productos.Remove(value);
            context.SaveChanges();
            return value;

        }
    }
}
