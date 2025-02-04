using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.Models
{
    public class Producto
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }

        // 🔹 Relación con Categoría
        [ForeignKey("Categoria")]
        public int categoriaid { get; set; }
        public Categoria Categoria { get; set; }

        // 🔹 Relación con Proveedor
        [ForeignKey("Proveedor")]
        public int proveedorid { get; set; }
        public Proveedor Proveedor { get; set; }
    }

    public class Categoria
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }

        // 🔹 Relación inversa: Una categoría tiene muchos productos
        public ICollection<Producto> Productos { get; set; }
    }

    public class Proveedor
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

        // 🔹 Relación inversa: Un proveedor tiene muchos productos
        public ICollection<Producto> Productos { get; set; }
    }
}
