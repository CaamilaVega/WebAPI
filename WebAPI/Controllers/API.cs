using Microsoft.AspNetCore.Mvc;
using Negocio.Modelos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API : ControllerBase
    {
        
        [HttpGet]
        public ActionResult< List<Datos>> Get()
        {
            return Ok( Datos.datosList);
        }
        
        [HttpGet("{id}")]

        public ActionResult< Datos >GetDatos(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var dato= Datos.datosList.FirstOrDefault(d => d.Id == id);
            if(dato==null)
            {
                return NotFound();
            }
            return Ok(dato);
        }

        // POST api/<API>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
