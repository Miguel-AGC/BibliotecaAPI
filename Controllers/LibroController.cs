using BibliotecaAPI.DTOs;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController(LibroService service) : ControllerBase
    {
        private readonly LibroService _service = service;

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string? titulo,
            [FromQuery] int? autorId,
            [FromQuery] bool? disponibles,
            [FromQuery] string? ordenarPor)
        {
            var result = await _service.ObtenerTodos(titulo, autorId, disponibles, ordenarPor);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.ObtenerPorId(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearLibroDTO dto)
        {
            var result = await _service.Crear(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ActualizarLibroDTO dto)
        {
            var result = await _service.Actualizar(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Eliminar(id);
            return Ok("Libro eliminado correctamente");
        }
    }
}
