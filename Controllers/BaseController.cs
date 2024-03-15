using System.Data.Common;
using System.Diagnostics;
using TiendaOnline.Data;
using TiendaOnline.Models;
using TiendaOnline.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayPalCheckoutSdk.Orders;

namespace TiendaOnline.Controllers
{
    public class BaseController : Controller
    {
        public readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override ViewResult View(string? viewName, object? model)
        {
            ViewBag.NumeroProductos = GetCarritoCount();
            return base.View(viewName, model);
        }

        protected int GetCarritoCount()
        {
            int count = 0;

            string? carritoJson = Request.Cookies["carrito"];

            if (!string.IsNullOrEmpty(carritoJson))
            {
                var carrito = JsonConvert.DeserializeObject<List<ProductoIdAndCantidad>>(
                    carritoJson
                );
                if (carrito != null)
                {
                    count = carrito.Count;
                }
            }

            return count;
        }

        public async Task<CarritoViewModel> AgregarProductoAlCarrito(int productoId, int cantidad)
        {
            var producto = await _context.Productos.FindAsync(productoId);

            if (producto != null)
            {
                var carritoViewModel = await GetCarritoViewModelAsync();

                var carritoItem = carritoViewModel.Items.FirstOrDefault(
                    item => item.ProductoId == productoId
                );

                if (carritoItem != null)
                    carritoItem.Cantidad += cantidad;
                else
                {
                    carritoViewModel.Items.Add(
                        new CarritoItemViewModel
                        {
                            ProductoId = producto.ProductoId,
                            Nombre = producto.Nombre,
                            Precio = producto.Precio,
                            Cantidad = cantidad
                        }
                    );
                }
                carritoViewModel.Total = carritoViewModel.Items.Sum(
                    item => item.Cantidad * item.Precio
                );

                await UpdateCarritoViewModelAsync(carritoViewModel);
                return carritoViewModel;
            }
            return new CarritoViewModel();
        }

        public async Task UpdateCarritoViewModelAsync(CarritoViewModel carritoViewModel)
        {
            var productoIds = carritoViewModel.Items
                .Select(
                    item =>
                        new ProductoIdAndCantidad
                        {
                            ProductoId = item.ProductoId,
                            Cantidad = item.Cantidad
                        }
                )
                .ToList();

            var carritoJson = await Task.Run(() => JsonConvert.SerializeObject(productoIds));
            Response.Cookies.Append(
                "carrito",
                carritoJson,
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(7) }
            );
        }

        public async Task<CarritoViewModel> GetCarritoViewModelAsync()
        {
            var carritoJson = Request.Cookies["carrito"];

            if (string.IsNullOrEmpty(carritoJson))
                return new CarritoViewModel();

            var productoIdsAndCantidades = JsonConvert.DeserializeObject<
                List<ProductoIdAndCantidad>
            >(carritoJson);
            var carritoViewModel = new CarritoViewModel();

            if (productoIdsAndCantidades != null)
            {
                foreach (var item in productoIdsAndCantidades)
                {
                    var producto = await _context.Productos.FindAsync(item.ProductoId);
                    
                    if (producto != null)
                    {
                        carritoViewModel.Items.Add(
                            new CarritoItemViewModel
                            {
                                ProductoId = producto.ProductoId,
                                Nombre = producto.Nombre,
                                Precio = producto.Precio,
                                Cantidad = item.Cantidad
                            }
                        );
                    }
                }
            }
            carritoViewModel.Total = carritoViewModel.Items.Sum(item => item.Subtotal);
            return carritoViewModel;
        }

        protected IActionResult HandleError(Exception e)
        {
            return View(
                "Error",
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }

        protected IActionResult HandleDbError(DbException dbException)
        {
            var ViewModel = new DbErrorViewModel
            {
                ErrorMessage = "Error de base de datos",
                Details = dbException.Message
            };
            return View("DbError", ViewModel);
        }

        protected IActionResult HandleDbUpdateError(DbUpdateException dbUpdateException)
        {
            var ViewModel = new DbErrorViewModel
            {
                ErrorMessage = "Error de actualización de base de datos",
                Details = dbUpdateException.Message
            };
            return View("DbError", ViewModel);
        }
    }
}
