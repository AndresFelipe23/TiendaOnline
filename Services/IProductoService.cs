using TiendaOnline.Models;
using TiendaOnline.Models.ViewModels;

namespace TiendaOnline.Services
{
    public interface IProductoService
    {
        Producto GetProducto(int id);

        Task<List<Producto>> GetProductosDestacados();

        Task<ProductosPaginadosViewModel> GetProductosPaginados(int? categoriaId, string? busqueda, int pagina, int productosPorPagina);
    }
}