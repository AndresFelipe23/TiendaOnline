using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Data;
using TiendaOnline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using X.PagedList;

namespace TiendaOnline.Controllers
{
    [Authorize(Policy = "RequireAdminOrStaff")]
    public class ProductosController : BaseController
    {
        public ProductosController(ApplicationDbContext context)
            : base(context) { }

        // GET: Productos
        public async Task<IActionResult> Index(string searchString)
        {

            var applicationDbContext = _context.Productos.Include(p => p.Categoria);
            var productos = from p in _context.Productos.Include(p => p.Categoria)
                            select p;
            // Filtrar por término de búsqueda si está presente
            if (!string.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => p.Codigo.Contains(searchString)
                                            || p.Nombre.Contains(searchString));
            }

            return View(await productos.ToListAsync());
        }

        [HttpPost]
        public IActionResult Search(string searchString)
        {
            // Redirigir a la acción Index con el término de búsqueda como parámetro
            return RedirectToAction("Index", new { searchString });
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(
                _context.Categorias,
                "CategoriaId",
                "Descripcion"
            );
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "ProductoId,Codigo,Nombre,Modelo,Descripcion,Precio,Imagen,CategoriaId,Stock,Marca,Activo"
            )]
                Producto producto
        )
        {
            var cat = await _context.Categorias
                .Where(c => c.CategoriaId == producto.CategoriaId)
                .FirstOrDefaultAsync();

            if (cat != null)
            {
                producto.Categoria = cat;
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(
                _context.Categorias,
                "CategoriaId",
                "Nombre",
                producto.CategoriaId
            );
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(
                _context.Categorias,
                "CategoriaId",
                "Descripcion",
                producto.CategoriaId
            );
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind(
                "ProductoId,Codigo,Nombre,Modelo,Descripcion,Precio,Imagen,CategoriaId,Stock,Marca,Activo"
            )]
                Producto producto
        )
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }
            var cat = await _context.Categorias
                .Where(c => c.CategoriaId == producto.CategoriaId)
                .FirstOrDefaultAsync();

            if (cat != null)
            {
                producto.Categoria = cat;
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(
                _context.Categorias,
                "CategoriaId",
                "Descripcion",
                producto.CategoriaId
            );
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
                _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }
    }
}
