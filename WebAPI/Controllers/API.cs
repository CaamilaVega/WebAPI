using Microsoft.AspNetCore.Mvc;
using Negocio.Modelos;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API : ControllerBase
    {
        
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult< List<Datos>> Get()
        {
            return Ok(ProductosAPI.datosList);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult< Datos >GetDatos(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var producto= ProductosAPI.datosList.FirstOrDefault(p => p.Id == id);
            if(producto==null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        // POST api/<API>
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
            if(datosPost.Id>0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            datosPost.Id=ProductosAPI.datosList.OrderByDescending(v=>v.Id).FirstOrDefault().Id +1;
            ProductosAPI.datosList.Add(datosPost);
            return Ok(datosPost);
        }

        // PUT api/<API>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<API>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
