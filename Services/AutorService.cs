using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Exceptions;
using BibliotecaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services
{
    public class AutorService
    {
        private readonly BibliotecaContext _context;

        public AutorService(BibliotecaContext context)
        {
            _context = context;
        }

        // Obtener todos
        public async Task<List<AutorDto>> GetAll()
        {
            return await _context.Autores
                .Select(a => new AutorDto
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    FechaNacimiento = a.FechaNacimiento,
                    Nacionalidad = a.Nacionalidad
                })
                .ToListAsync();
        }

        // Obtener por ID
        public async Task<AutorDto> GetById(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null) throw new ApiException("Autor no encontrado", 404);

            return new AutorDto
            {
                Id = autor.Id,
                Nombre = autor.Nombre,
                FechaNacimiento = autor.FechaNacimiento,
                Nacionalidad = autor.Nacionalidad
            };
        }

        // Crear
        public async Task<AutorDto> Create(CrearAutorDto dto)
        {
            var nombre = dto.Nombre.Trim().ToUpper();
            var nacionalidad = dto.Nacionalidad.Trim().ToUpper();

            if (dto.FechaNacimiento > DateTime.Now)
                throw new ApiException("La fecha de nacimiento no puede ser futura", 400);

            var autor = new Autor
            {
                Nombre = nombre,
                FechaNacimiento = dto.FechaNacimiento,
                Nacionalidad = nacionalidad
            };

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return new AutorDto
            {
                Id = autor.Id,
                Nombre = autor.Nombre,
                FechaNacimiento = autor.FechaNacimiento,
                Nacionalidad = autor.Nacionalidad
            };
        }

        // Actualizar
        public async Task<AutorDto> Update(int id, CrearAutorDto dto)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null) throw new ApiException("Autor no encontrado", 404);

            if (dto.FechaNacimiento > DateTime.Now)
                throw new ApiException("La fecha de nacimiento no puede ser futura", 400);

            autor.Nombre = dto.Nombre.Trim().ToUpper();
            autor.Nacionalidad = dto.Nacionalidad.Trim().ToUpper();
            autor.FechaNacimiento = dto.FechaNacimiento;

            await _context.SaveChangesAsync();

            return await GetById(autor.Id);
        }

        // Eliminar
        public async Task<bool> Delete(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null) throw new ApiException("Autor no encontrado", 404);

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
