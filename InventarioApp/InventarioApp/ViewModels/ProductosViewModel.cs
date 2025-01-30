using System.Collections.ObjectModel;
using InventarioApp.Models;
using InventarioApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

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
                OnPropertyChanged(); // Notifica el cambio a la vista
            }
        }

        private Producto _productoSeleccionado;
        public Producto ProductoSeleccionado
        {
            get => _productoSeleccionado;
            set
            {
                _productoSeleccionado = value;
                OnPropertyChanged();
            }
        }

        // Comandos para CRUD
        public ICommand AgregarProductoCommand { get; }
        public ICommand ActualizarProductoCommand { get; }
        public ICommand EliminarProductoCommand { get; }
        public ICommand GuardarCambiosCommand { get; }

        // Constructor que recibe el AppDbContext inyectado
        public ProductosViewModel(AppDbContext context)
        {
            _context = context; // Asigna el contexto inyectado
            LoadProductos(); // Cargar los productos desde la base de datos

            // Comandos CRUD
            AgregarProductoCommand = new RelayCommand(AgregarProducto);
            ActualizarProductoCommand = new RelayCommand(ActualizarProducto, CanExecuteActualizarOEliminar);
            EliminarProductoCommand = new RelayCommand(EliminarProducto, CanExecuteActualizarOEliminar);
            GuardarCambiosCommand = new RelayCommand(GuardarCambios);
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

        // Agregar un nuevo producto
        private void AgregarProducto(object parameter)
        {
            var nuevoProducto = new Producto
            {
                nombre = "Nuevo Producto",
                precio = 0,
                cantidad = 0,
                categoriaid = 1, // Debe asignarse una categoría válida
                proveedorid = 1  // Debe asignarse un proveedor válido
            };

            _context.Productos.Add(nuevoProducto);
            _context.SaveChanges();
            Productos.Add(nuevoProducto);
        }

        // Actualizar un producto existente
        private void ActualizarProducto(object parameter)
        {
            if (ProductoSeleccionado != null)
            {
                _context.Productos.Update(ProductoSeleccionado);
                _context.SaveChanges();
                LoadProductos();
            }
        }

        // Eliminar un producto seleccionado
        private void EliminarProducto(object parameter)
        {
            if (ProductoSeleccionado != null)
            {
                _context.Productos.Remove(ProductoSeleccionado);
                _context.SaveChanges();
                Productos.Remove(ProductoSeleccionado);
            }
        }

        // Guardar cambios en la base de datos
        private void GuardarCambios(object parameter)
        {
            _context.SaveChanges();
            LoadProductos();
        }

        // Verifica si se puede actualizar o eliminar un producto
        private bool CanExecuteActualizarOEliminar(object parameter)
        {
            return ProductoSeleccionado != null;
        }
    }
}
