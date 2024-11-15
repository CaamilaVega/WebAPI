using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Negocio.Modelos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API : ControllerBase
    {
        private readonly ILogger<API> _logger;

        public API (ILogger<API> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult< List<Datos>> Get()
        {
            _logger.LogInformation("SE MUESTRAN TODOS LOS PRODUCTOS");
            return Ok( ProductosAPI.datosList);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult< Datos >GetDatos(int id)
        {
            if(id==0)
            {
                _logger.LogError("ERRO EN EL ID INGRESADO: "+id);
                return BadRequest();
            }
            var dato= ProductosAPI.datosList.FirstOrDefault(d => d.Id == id);
            if(dato==null)
            {
                return NotFound();
            }
            return Ok(dato);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<Datos> PostDatos([FromBody] Datos datosPost)
        {
            if (datosPost == null)
            {
                return BadRequest(datosPost);
            }
            if (datosPost.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            datosPost.Id = ProductosAPI.datosList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            ProductosAPI.datosList.Add(datosPost);
            return Ok(datosPost);

        }

        // PUT api/<API>/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PutDatos(int id, [FromBody] Datos datosPut)
        {
            if(datosPut==null || id != datosPut.Id)
            {
                return BadRequest();
            }
            var datos = ProductosAPI.datosList.FirstOrDefault(d => d.Id == id);
            datos.title = datosPut.title;
            datos.price = datosPut.price;
            datos.category=datosPut.category;

            return NoContent();

        }

        //[HttpPatch("{id}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult PatchDatos(int id, JsonPatchDocument<Datos> datosPatch)
        //{
        //    if (datosPatch == null || id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var dat = ProductosAPI.datosList.FirstOrDefault(d => d.Id == id);
        //    datosPatch.ApplyTo(dat, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    return NoContent();

        //}


        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDatos(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var datos=ProductosAPI.datosList.FirstOrDefault(d=>d.Id == id);
            if (datos == null)
            {
                return NotFound();
            }
            ProductosAPI.datosList.Remove(datos);
            return NoContent();
        }
    }
}
