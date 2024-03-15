using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaOnline.Models
{
    public class Detalle_Pedido
    {
        [Key]
        public int DetallePedidoId { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; } = null!;

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; } = null!;

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal Precio { get; set; }
    }
}
