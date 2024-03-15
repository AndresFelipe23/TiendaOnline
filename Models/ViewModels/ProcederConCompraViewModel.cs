namespace TiendaOnline.Models.ViewModels
{
    public class ProcederConCompraViewModel
    {
        public CarritoViewModel Carrito { get; set; } = null!;
        public List<Direccion> Direcciones { get; set; } = null!;
        public int DireccionIdSelecciona { get; set; }
    }
}
