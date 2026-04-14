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
            return Ok(ApiResponse<object>.Ok(result, "Libros obtenidos correctamente"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.ObtenerPorId(id);
            return Ok(ApiResponse<LibroDTO>.Ok(result, "Libro encontrado"));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearLibroDTO dto)
        {
            var result = await _service.Crear(dto);
            return Ok(ApiResponse<LibroDTO>.Ok(result, "Libro creado correctamente"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ActualizarLibroDTO dto)
        {
            var result = await _service.Actualizar(id, dto);
            return Ok(ApiResponse<LibroDTO>.Ok(result, "Libro actualizado correctamente"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Eliminar(id);
            return Ok(ApiResponse<string>.Ok(null, "Libro eliminado correctamente"));
        }
    }
}
