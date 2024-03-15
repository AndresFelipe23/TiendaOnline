using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaOnline.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Pedidos = new List<Pedido>();
        }

        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string Telefono { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Contrasenia { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Correo { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Direccion { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Ciudad { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string CodigoPostal { get; set; } = null!;

        [Required]
        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public Rol Rol { get; set; } = null!;
        public ICollection<Pedido> Pedidos { get; set; }

        [InverseProperty("Usuario")]
        public ICollection<Direccion> Direcciones { get; set; } = null!;
    }
}
