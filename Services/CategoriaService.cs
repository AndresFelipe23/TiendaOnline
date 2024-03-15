using TiendaOnline.Data;
using TiendaOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace TiendaOnline.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }
    }
}
