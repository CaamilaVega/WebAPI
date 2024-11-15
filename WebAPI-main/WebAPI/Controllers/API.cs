using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Negocio.Modelos;
using Negocio;
using System.Reflection;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Google.Protobuf.WellKnownTypes;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API : ControllerBase
    {
        private readonly ProductosAPI prodApi = new ProductosAPI();
        private readonly ILogger<API> _logger;

        public API(ILogger<API> logger)
        {
            _logger = logger;
        }

        [HttpGet("products")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetProducto()
        {
            try
            {
                _logger.LogInformation("Todos los productos.");
                List<Datos> lisdatos = prodApi.GetProduct();
                return Ok(lisdatos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos.");
                return StatusCode(500, "Error en el servidor al obtener los productos.");
            }
        }

        [HttpGet("Category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetCategories()
        {
            _logger.LogInformation("Iniciando la busqueda de todas las categorías.");
            try
            {
                List<string> lisdatos = prodApi.GetCategories();
                return Ok(lisdatos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las categorías.");
                return StatusCode(500, "Error en el servidor al obtener las categorías.");
            }
        }

        [HttpGet("products/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult IdGet(int id)
        {
            _logger.LogInformation("Iniciando la búsqueda del producto con ID: {Id}", id);
            try
            {
                var producto = prodApi.GetId(id);

                if (producto == null)
                {
                    _logger.LogWarning("No se encontró el producto con ID: {Id}", id);
                    return NotFound();
                }
                return Ok(producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el producto con ID: {Id}", id);
                return StatusCode(500, "Error en el servidor al obtener el producto.");
            }
        }

        [HttpGet("products/category/{category}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCat(string category)
        {
            _logger.LogInformation("Iniciando la búsqueda de productos en la categoría: {Category}", category);
            try
            {
                var categoria = prodApi.GetCategory(category);

                if (categoria == null)
                {
                    _logger.LogWarning("No se encontraron productos en la categoría: {Category}", category);
                    return NotFound();
                }

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos en la categoría: {Category}", category);
                return StatusCode(500, "Error en el servidor al obtener los productos por categoría.");
            }
        }

        [HttpPost("products")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult DatoPost([FromBody] Datos datosPost)
        {
            _logger.LogInformation("Iniciando la creación de un nuevo producto.");

            try
            {
                if (datosPost == null)
                {
                    _logger.LogWarning("No se recibieron datos por eso no se puede guardar la información.");
                    return BadRequest("No se recibieron datos por eso no se puede guardar la información.");
                }

                if (datosPost.Id > 0)
                {
                    _logger.LogWarning("El ID del producto debe ser 0 al crear un nuevo producto. ID recibido: {Id}", datosPost.Id);
                    return BadRequest("El ID del producto debe ser 0 al crear un nuevo producto.");
                }

                Datos datoNuevo = prodApi.PostDat(datosPost);
                return Ok(datoNuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el nuevo producto.");
                return StatusCode(500, "Error en el servidor al guardar el nuevo producto.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult PutDatos(int id, [FromBody] Datos datos)
        {
            _logger.LogInformation("Iniciando la actualización del producto con ID: {Id}", id);

            if (id == 0 || datos == null || datos.Id != id)
            {
                _logger.LogError("Error en la validación de los datos para actualizar el producto. ID ingresado: {Id}", id, datos?.Id);
                return BadRequest("Debe completar el ID correctamente");
            }

            try
            {
                prodApi.PutId(datos);
                _logger.LogInformation("Producto con ID: {Id} actualizado exitosamente.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el producto con ID: {Id}", id);
                return StatusCode(500, "Error en el servidor al actualizar el producto.");
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDatos(int id)
        {
            _logger.LogInformation("Iniciando la eliminación del producto con ID: {Id}", id);

            if (id == 0)
            {
                _logger.LogWarning("El ID proporcionado para eliminar es inválido: {Id}", id);
                return BadRequest("ID inválido para eliminación.");
            }

            try
            {
                prodApi.DeleteID(id);
                _logger.LogInformation("Producto con ID: {Id} eliminado exitosamente.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el producto con ID: {Id}", id);
                return StatusCode(500, "Error en el servidor al eliminar el producto.");
            }

            return NoContent();
        }

        [HttpDelete("products/{category}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCat(string category)
        {
            _logger.LogInformation("Iniciando la eliminación de la categoría: {Category}", category);

            if (string.IsNullOrEmpty(category))
            {
                _logger.LogWarning("La categoría proporcionada para eliminar es nula o vacía.");
                return BadRequest("La categoría no puede ser nula o vacía.");
            }

            try
            {
                prodApi.DeleteCategory(category);
                _logger.LogInformation("Categoría: {Category} eliminada exitosamente.", category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la categoría: {Category}", category);
                return StatusCode(500, "Error en el servidor al eliminar la categoría.");
            }

            return NoContent();
        }
    }
}

