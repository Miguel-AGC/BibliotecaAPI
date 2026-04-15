using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Exceptions;
using BibliotecaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services
{
    public class PrestamoService
    {
        private readonly BibliotecaContext _context;

        public PrestamoService(BibliotecaContext context)
        {
            _context = context;
        }

        //  CREAR PRÉSTAMO
        public async Task<PrestamoDTO> Crear(CrearPrestamoDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null)
                throw new ApiException("Usuario no existe", 404);

            var libro = await _context.Libros.FindAsync(dto.LibroId);
            if (libro == null)
                throw new ApiException("Libro no existe", 404);

            if (libro.Ejemplares <= 0)
                throw new ApiException("No hay ejemplares disponibles");

            //  Crear préstamo
            var prestamo = new Prestamo
            {
                UsuarioId = dto.UsuarioId,
                LibroId = dto.LibroId
            };

            //  Reducir stock
            libro.Ejemplares--;

            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();

            return await ObtenerPorId(prestamo.Id);
        }

        //  DEVOLVER LIBRO
        public async Task Devolver(int id)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestamo == null)
                throw new ApiException("Préstamo no encontrado", 404);

            if (prestamo.FechaDevolucion != null)
                throw new ApiException("El libro ya fue devuelto");

            //  Aumentar stock
            prestamo.Libro.Ejemplares++;

            prestamo.FechaDevolucion = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        // GET TODOS
        public async Task<List<PrestamoDTO>> ObtenerTodos()
        {
            return await _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .Select(p => new PrestamoDTO
                {
                    Id = p.Id,
                    UsuarioNombre = p.Usuario.Nombre,
                    LibroTitulo = p.Libro.Titulo,
                    FechaPrestamo = p.FechaPrestamo,
                    FechaDevolucion = p.FechaDevolucion,
                    Activo = p.FechaDevolucion == null
                })
                .ToListAsync();
        }

        //  OBTENER POR ID
        public async Task<PrestamoDTO> ObtenerPorId(int id)
        {
            var p = await _context.Prestamos
                .Include(x => x.Usuario)
                .Include(x => x.Libro)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (p == null)
                throw new ApiException("Préstamo no encontrado", 404);

            return new PrestamoDTO
            {
                Id = p.Id,
                UsuarioNombre = p.Usuario.Nombre,
                LibroTitulo = p.Libro.Titulo,
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucion = p.FechaDevolucion,
                Activo = p.FechaDevolucion == null
            };
        }
    }
}
