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
            return Ok(autores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var autor = await _service.GetById(id);

            if (autor == null) return NotFound();

            return Ok(autor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CrearAutorDto dto)
        {
            var autor = await _service.Create(dto);
            return Ok(autor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CrearAutorDto dto)
        {
            var updated = await _service.Update(id, dto);

            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);

            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
