using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services
{
    public class LibroService
    {
        private readonly BibliotecaContext _context;

        public LibroService(BibliotecaContext context)
        {
            _context = context;
        }

        // Crear un nuevo libro
        public async Task<LibroDTO> Crear(CrearLibroDTO dto)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                throw new Exception("El título es requerido");

            if (dto.Ejemplares < 0)
                throw new Exception("Ejemplares no puede ser negativo");

            var autorExiste = await _context.Autores
                .AnyAsync(a => a.Id == dto.AutorId);

            if (!autorExiste)
                throw new Exception("El autor no existe");

            var isbnExiste = await _context.Libros
                .AnyAsync(l => l.ISBN == dto.ISBN);

            if (isbnExiste)
                throw new Exception("El ISBN ya existe");

            var libro = new Libro
            {
                Titulo = dto.Titulo.Trim().ToUpper(),
                ISBN = dto.ISBN.Trim(),
                FechaPublicacion = dto.FechaPublicacion,
                Ejemplares = dto.Ejemplares,
                AutorId = dto.AutorId
            };

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return await ObtenerPorId(libro.Id);
        }

        // Listado de todos los libros (con filtros opcionales)
        public async Task<List<LibroDTO>> ObtenerTodos(
            string? titulo,
            int? autorId,
            bool? disponibles,
            string? ordenarPor)
        {
            var query = _context.Libros
                .Include(l => l.Autor)
                .AsQueryable();

            // Filtros
            if (!string.IsNullOrWhiteSpace(titulo))
                query = query.Where(l => l.Titulo.Contains(titulo.ToUpper()));

            if (autorId.HasValue)
                query = query.Where(l => l.AutorId == autorId.Value);

            if (disponibles.HasValue && disponibles.Value)
                query = query.Where(l => l.Ejemplares > 0);

            // Ordenamiento
            query = ordenarPor switch
            {
                "fecha" => query.OrderBy(l => l.FechaPublicacion),
                "titulo" => query.OrderBy(l => l.Titulo),
                _ => query.OrderBy(l => l.Id)
            };

            return await query
                .Select(l => new LibroDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    ISBN = l.ISBN,
                    FechaPublicacion = l.FechaPublicacion,
                    Ejemplares = l.Ejemplares,
                    Disponible = l.Ejemplares > 0,
                    Autor = l.Autor.Nombre
                })
                .ToListAsync();
        }

        // Obtener un libro por su ID
        public async Task<LibroDTO> ObtenerPorId(int id)
        {
            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (libro == null)
                throw new Exception("Libro no encontrado");

            return new LibroDTO
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                ISBN = libro.ISBN,
                FechaPublicacion = libro.FechaPublicacion,
                Ejemplares = libro.Ejemplares,
                Disponible = libro.Ejemplares > 0,
                Autor = libro.Autor.Nombre
            };
        }

        // Actualizar un libro existente
        public async Task<LibroDTO> Actualizar(int id, ActualizarLibroDTO dto)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
                throw new Exception("Libro no encontrado");

            if (dto.Ejemplares < 0)
                throw new Exception("Ejemplares no puede ser negativo");

            var autorExiste = await _context.Autores
                .AnyAsync(a => a.Id == dto.AutorId);

            if (!autorExiste)
                throw new Exception("El autor no existe");

            var isbnDuplicado = await _context.Libros
                .AnyAsync(l => l.ISBN == dto.ISBN && l.Id != id);

            if (isbnDuplicado)
                throw new Exception("El ISBN ya está en uso");

            libro.Titulo = dto.Titulo.Trim().ToUpper();
            libro.ISBN = dto.ISBN.Trim();
            libro.FechaPublicacion = dto.FechaPublicacion;
            libro.Ejemplares = dto.Ejemplares;
            libro.AutorId = dto.AutorId;

            await _context.SaveChangesAsync();

            return await ObtenerPorId(id);
        }

        // Eliminar un libro
        public async Task Eliminar(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
                throw new Exception("Libro no encontrado");

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
        }
    }
}
