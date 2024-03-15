using TiendaOnline.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Models.ViewModels;

namespace TiendaOnline.Controllers
{
    [Authorize(Policy = "RequireAdminOrStaff")]
    public class DashboardController : BaseController
    {
        public DashboardController(ApplicationDbContext context)
            : base(context) { }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalBanner = _context.Banners.Count(),
                TotalCategorias = _context.Categorias.Count(),
                TotalDetallePedido = _context.DetallePedidos.Count(),
                TotalDirecciones = _context.Direcciones.Count(),
                TotalProductos = _context.Productos.Count(),
                TotalPedidos = _context.Pedidos.Count(),
                TotalRoles = _context.Roles.Count(),
                TotalUsuarios = _context.Usuarios.Count()
            };
            return View(model);
        }
    }
}
