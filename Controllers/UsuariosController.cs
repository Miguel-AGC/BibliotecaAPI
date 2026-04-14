using BibliotecaAPI.DTOs;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuariosController(UsuarioService usuarioService)
        {
            _service = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UsuarioFiltroDTO filtro)
        {
            var result = await _service.ObtenerFiltrados(filtro);
            return Ok(ApiResponse<object>.Ok(result, "Usuarios obtenidos"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.ObtenerPorId(id);
            return Ok(ApiResponse<UsuarioDTO>.Ok(result, "Usuario encontrado"));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearUsuarioDTO dto)
        {
            var result = await _service.Crear(dto);
            return Ok(ApiResponse<UsuarioDTO>.Ok(result, "Usuario creado"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ActualizarUsuarioDTO dto)
        {
            var result = await _service.Actualizar(id, dto);
            return Ok(ApiResponse<UsuarioDTO>.Ok(result, "Usuario actualizado"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Eliminar(id);
            return Ok(ApiResponse<string>.Ok(null, "Usuario eliminado"));
        }

        [HttpPut("{id}/cambiar-password")]
        public async Task<IActionResult> CambiarPassword(int id, [FromBody] CambiarPasswordDTO dto)
        {
            await _service.CambiarPassword(id, dto);

            return Ok(ApiResponse<string>.Ok(null, "Contraseña actualizada correctamente"));
        }
    }
}
