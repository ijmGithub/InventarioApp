using System.Collections.ObjectModel;
using InventarioApp.Models;
using InventarioApp.Data;
using Microsoft.EntityFrameworkCore;

namespace InventarioApp.ViewModels
{
    public class ProductosViewModel : ViewModelBase
    {
        private readonly AppDbContext _context;

        private ObservableCollection<Producto> _productos;
        public ObservableCollection<Producto> Productos
        {
            get => _productos;
            set
            {
                _productos = value;
                OnPropertyChanged(); // Notifica el cambio de la propiedad a la vista
            }
        }

        // Constructor que recibe el AppDbContext inyectado
        public ProductosViewModel(AppDbContext context)
        {
            _context = context; // Asigna el contexto inyectado
            LoadProductos(); // Cargar los productos desde la base de datos
        }

        // Método para cargar productos de la base de datos
        private void LoadProductos()
        {
            var productos = _context.Productos
                .Include(p => p.Categoria) // Incluye la relación con Categoría
                .Include(p => p.Proveedor) // Incluye la relación con Proveedor
                .ToList();

            // Verificar si los datos se están cargando
            foreach (var producto in productos)
            {
                Console.WriteLine($"Producto: {producto.nombre}, Precio: {producto.precio}");
            }

            Productos = new ObservableCollection<Producto>(productos);
        }
    }
}
