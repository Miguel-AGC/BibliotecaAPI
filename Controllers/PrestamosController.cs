using BibliotecaAPI.DTOs;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : Controller
    {
        private readonly PrestamoService _service;

        public PrestamosController(PrestamoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearPrestamoDTO dto)
        {
            var result = await _service.Crear(dto);
            return Ok(ApiResponse<PrestamoDTO>.Ok(result, "Préstamo creado"));
        }

        [HttpPut("{id}/devolver")]
        public async Task<IActionResult> Devolver(int id)
        {
            await _service.Devolver(id);
            return Ok(ApiResponse<string>.Ok(null, "Libro devuelto correctamente"));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.ObtenerTodos();
            return Ok(ApiResponse<List<PrestamoDTO>>.Ok(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.ObtenerPorId(id);
            return Ok(ApiResponse<PrestamoDTO>.Ok(result));
        }
    }
}
