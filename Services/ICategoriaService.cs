using TiendaOnline.Models;

namespace TiendaOnline.Services
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> GetCategorias();
    }
}