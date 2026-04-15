using BibliotecaAPI.DTOs;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : Controller
    {
        private readonly AutorService _service;

        public AutoresController(AutorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var autores = await _service.GetAll();
            return Ok(ApiResponse<object>.Ok(autores, "Autores obtenidos correctamente"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var autor = await _service.GetById(id);

            if (autor == null) return NotFound();

            return Ok(ApiResponse<AutorDto>.Ok(autor, "Autor obtenido correctamente"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CrearAutorDto dto)
        {
            var autor = await _service.Create(dto);
            return Ok(ApiResponse<AutorDto>.Ok(autor, "Autor creado correctamente"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CrearAutorDto dto)
        {
            var updated = await _service.Update(id, dto);

            return Ok(ApiResponse<AutorDto>.Ok(updated, "Autor actualizado correctamente"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);
            return Ok(ApiResponse<string>.Ok(null, "Autor eliminado correctamente"));
        }
    }
}
