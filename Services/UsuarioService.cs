using BCrypt.Net;
using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Exceptions;
using BibliotecaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services
{
    public class UsuarioService
    {
        private readonly BibliotecaContext _context;

        public UsuarioService(BibliotecaContext context)
        {
            _context = context;
        }

        // Registrar nuevo usuario
        public async Task<UsuarioDTO> Crear(CrearUsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ApiException("El nombre es requerido");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ApiException("El email es requerido");

            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
                throw new ApiException("La contraseña debe tener al menos 6 caracteres");

            var email = dto.Email.Trim().ToLower();

            var existe = await _context.Usuarios
                .AnyAsync(u => u.Email == email);

            if (existe)
                throw new ApiException("El email ya está registrado");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var usuario = new Usuarios
            {
                Username = dto.Username.Trim(),
                Nombre = dto.Nombre.Trim().ToUpper(),
                Email = email,
                Password = passwordHash,
                Telefono = dto.Telefono,
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return MapToDTO(usuario);
        }

        // Obtener usuarios con filtros, ordenamiento y paginación
        public async Task<object> ObtenerFiltrados(UsuarioFiltroDTO filtro)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
                query = query.Where(u => u.Nombre.Contains(filtro.Nombre.ToUpper()));

            if (!string.IsNullOrWhiteSpace(filtro.Email))
                query = query.Where(u => u.Email.Contains(filtro.Email.ToLower()));

            query = filtro.OrdenarPor?.ToLower() switch
            {
                "nombre" => filtro.Desc ? query.OrderByDescending(u => u.Nombre) : query.OrderBy(u => u.Nombre),
                "email" => filtro.Desc ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email),
                _ => query.OrderBy(u => u.Id)
            };

            var total = await query.CountAsync();

            var data = await query
                .Skip((filtro.Page - 1) * filtro.PageSize)
                .Take(filtro.PageSize)
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Telefono = u.Telefono,
                    FechaRegistro = u.FechaRegistro
                })
                .ToListAsync();

            return new
            {
                total,
                page = filtro.Page,
                pageSize = filtro.PageSize,
                data
            };
        }

        // Obtener por ID
        public async Task<UsuarioDTO> ObtenerPorId(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                throw new ApiException("Usuario no encontrado", 404);

            return MapToDTO(usuario);
        }

        // Actualizar 
        public async Task<UsuarioDTO> Actualizar(int id, ActualizarUsuarioDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                throw new ApiException("Usuario no encontrado", 404);

            var email = dto.Email.Trim().ToLower();

            var existe = await _context.Usuarios
                .AnyAsync(u => u.Email == email && u.Id != id);

            if (existe)
                throw new ApiException("El email ya está en uso");

            usuario.Nombre = dto.Nombre.Trim().ToUpper();
            usuario.Email = email;
            usuario.Telefono = dto.Telefono;

            await _context.SaveChangesAsync();

            return MapToDTO(usuario);
        }

        // Eliminar usuario
        public async Task Eliminar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                throw new ApiException("Usuario no encontrado", 404);

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        //  Mapper
        private UsuarioDTO MapToDTO(Usuarios u)
        {
            return new UsuarioDTO
            {
                Id = u.Id,
                Username = u.Username,
                Nombre = u.Nombre,
                Email = u.Email,
                Telefono = u.Telefono,
                FechaRegistro = u.FechaRegistro
            };
        }
    }
}
