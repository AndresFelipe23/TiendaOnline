using TiendaOnline.Data;
using TiendaOnline.Models;
using TiendaOnline.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TiendaOnline.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Producto GetProducto(int id)
        {
            var producto = _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.ProductoId == id);

            if (producto != null)
                return producto;

            return new Producto();
        }

        public async Task<List<Producto>> GetProductosDestacados()
        {
            IQueryable<Producto> productosQuery = _context.Productos;
            productosQuery = productosQuery.Where(p => p.Activo);

            List<Producto> productosDestacados = await productosQuery.Take(9).ToListAsync();
            return productosDestacados;
        }

        public async Task<ProductosPaginadosViewModel> GetProductosPaginados(
            int? categoriaId,
            string? busqueda,
            int pagina,
            int productosPorPagina
        )
        {
            IQueryable<Producto> query = _context.Productos;
            query = query.Where(p => p.Activo);

            if (categoriaId.HasValue)
                query = query.Where(p => p.CategoriaId == categoriaId);

            if (!string.IsNullOrEmpty(busqueda))
                query = query.Where(
                    p => p.Nombre.Contains(busqueda) || p.Descripcion.Contains(busqueda)
                );
            int totalProductos = await query.CountAsync();

            int totalPaginas = (int)Math.Ceiling((double)totalProductos / productosPorPagina);

            if (pagina < 1)
                pagina = 1;
            else if (pagina > totalPaginas)
                pagina = totalPaginas;

            List<Producto> productos = new();
            if (totalProductos > 0)
            {
                productos = await query
                    .OrderBy(p => p.Nombre)
                    .Skip((pagina - 1) * productosPorPagina)
                    .Take(productosPorPagina)
                    .ToListAsync();
            }

            bool mostrarMensajeSinResultados = totalProductos == 0;

            var model = new ProductosPaginadosViewModel
            {
                Productos = productos,
                PaginaActual = pagina,
                TotalPaginas = totalPaginas,
                CategoriaIdSeleccionada = categoriaId,
                Busqueda = busqueda,
                MostrarMensajeSinResultados = mostrarMensajeSinResultados
            };

            return model;
        }
    }
}
