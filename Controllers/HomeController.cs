using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Models;
using TiendaOnline.Data;
using TiendaOnline.Services;

namespace TiendaOnline.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductoService _productoService;
    private readonly ICategoriaService _categoriaService;

    public HomeController(
        ILogger<HomeController> logger,
        ApplicationDbContext context,
        IProductoService productoService,
        ICategoriaService categoriaService
    )
        : base(context)
    {
        _logger = logger;
        _productoService = productoService;
        _categoriaService = categoriaService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Categorias = await _categoriaService.GetCategorias();
        ViewBag.Banners=_context.Banners.ToList();

        try
        {
            List<Producto> productosDestacados = await _productoService.GetProductosDestacados();
            return View(productosDestacados);
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }

    public IActionResult DetalleProducto(int id)
    {
        var producto = _productoService.GetProducto(id);
        if (producto == null)
            return NotFound();

        return View(producto);
    }

    public async Task<IActionResult> Productos(int? categoriaId, string? busqueda, int pagina = 1)
    {
        try
        {
            int productosPorPagina = 9;
            var model = await _productoService.GetProductosPaginados(
                categoriaId,
                busqueda,
                pagina,
                productosPorPagina
            );

            ViewBag.Categorias = await _categoriaService.GetCategorias();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductosPartial", model);
            }

            return View(model);
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }

    public async Task<IActionResult> AgregarProducto(
        int id,
        int cantidad,
        int? categoriaId,
        string? busqueda,
        int pagina = 1
    )
    {
        var carritoViewModel = await AgregarProductoAlCarrito(id, cantidad);
        if (carritoViewModel != null)
        {
            return RedirectToAction(
                "Productos",
                new
                {
                    id,
                    categoriaId,
                    busqueda,
                    pagina
                }
            );
        }
        else
            return NotFound();
    }

    public async Task<IActionResult> AgregarProductoIndex(int id, int cantidad)
    {
        var carritoViewModel = await AgregarProductoAlCarrito(id, cantidad);
        if (carritoViewModel != null)
        {
            return RedirectToAction("Index");
        }
        else
            return NotFound();
    }

    public async Task<IActionResult> AgregarProductoDetalle(int id, int cantidad)
    {
        var carritoViewModel = await AgregarProductoAlCarrito(id, cantidad);
        if (carritoViewModel != null)
        {
            return RedirectToAction("DetalleProducto", new { id });
        }
        else
            return NotFound();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
