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
        public List<Datos> Get()
        {
            return Datos.datosList;
        }
        
        [HttpGet("{id}")]
        public Datos GetDatos(int id)
        {

            return Datos.datosList.FirstOrDefault(d=>d.Id==id);
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
