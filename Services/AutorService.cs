using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
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

            if (autor == null) return null;

            return new AutorDto
            {
                Id = autor.Id,
                Nombre = autor.Nombre
            };
        }

        // Crear
        public async Task<AutorDto> Create(CrearAutorDto dto)
        {
            var nombre = dto.Nombre.Trim().ToUpper();
            var nacionalidad = dto.Nacionalidad.Trim().ToUpper();

            if (dto.FechaNacimiento > DateTime.Now)
                throw new Exception("La fecha de nacimiento no puede ser futura");

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
        public async Task<bool> Update(int id, CrearAutorDto dto)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null) return false;

            if (dto.FechaNacimiento > DateTime.Now)
                throw new Exception("La fecha de nacimiento no puede ser futura");

            autor.Nombre = dto.Nombre.Trim().ToUpper();
            autor.Nacionalidad = dto.Nacionalidad.Trim().ToUpper();
            autor.FechaNacimiento = dto.FechaNacimiento;

            await _context.SaveChangesAsync();

            return true;
        }

        // Eliminar
        public async Task<bool> Delete(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null) return false;

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
