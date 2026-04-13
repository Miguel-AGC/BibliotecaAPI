using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;

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
                    Nombre = a.Nombre
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
            var autor = new Autor
            {
                Nombre = dto.Nombre
            };

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return new AutorDto
            {
                Id = autor.Id,
                Nombre = autor.Nombre
            };
        }

        // Actualizar
        public async Task<bool> Update(int id, CrearAutorDto dto)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null) return false;

            autor.Nombre = dto.Nombre;

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
