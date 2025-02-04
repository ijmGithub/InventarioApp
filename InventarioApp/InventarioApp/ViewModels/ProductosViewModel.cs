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
                OnPropertyChanged(nameof(Productos)); // Forzar actualización en UI
            }
        }

        private ObservableCollection<Categoria> _categorias;
        public ObservableCollection<Categoria> Categorias
        {
            get => _categorias;
            set
            {
                _categorias = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Proveedor> _proveedores;
        public ObservableCollection<Proveedor> Proveedores
        {
            get => _proveedores;
            set
            {
                _proveedores = value;
                OnPropertyChanged();
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

        private Categoria _categoriaSeleccionada;
        public Categoria CategoriaSeleccionada
        {
            get => _categoriaSeleccionada;
            set
            {
                _categoriaSeleccionada = value;
                OnPropertyChanged();
            }
        }


        private Proveedor _proveedorSeleccionado;
        public Proveedor ProveedorSeleccionado
        {
            get => _proveedorSeleccionado;
            set
            {
                _proveedorSeleccionado = value;
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
            LoadCategoriasYProveedores();

            // Comandos CRUD
            AgregarProductoCommand = new RelayCommand(AgregarProducto, CanExecuteAgregar);
            ActualizarProductoCommand = new RelayCommand(ActualizarProducto, CanExecuteActualizarOEliminar);
            EliminarProductoCommand = new RelayCommand(EliminarProducto, CanExecuteActualizarOEliminar);
            GuardarCambiosCommand = new RelayCommand(GuardarCambios);
        }

        // 🔹 Cargar productos con Categoría y Proveedor
        private void LoadProductos()
        {
            var productos = _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .ToList();

            Productos = new ObservableCollection<Producto>(productos);
            OnPropertyChanged(nameof(Productos)); // 🔹 Notificar cambios a la UI
        }

        // 🔹 Cargar Categorías y Proveedores
        private void LoadCategoriasYProveedores()
        {
            Categorias = new ObservableCollection<Categoria>(_context.Categorias.ToList());
            Proveedores = new ObservableCollection<Proveedor>(_context.Proveedores.ToList());

            // 🔹 Notificar cambios a la UI
            OnPropertyChanged(nameof(Categorias));
            OnPropertyChanged(nameof(Proveedores));
        }
        
        // 🔹 Agregar un nuevo producto
        private void AgregarProducto(object parameter)
        {
            if (CategoriaSeleccionada == null || ProveedorSeleccionado == null)
            {
                System.Windows.MessageBox.Show("Seleccione una categoría y un proveedor.");
                return;
            }

            var nuevoProducto = new Producto
            {
                nombre = "Nuevo Producto",
                precio = 0,
                cantidad = 0,
                categoriaid = CategoriaSeleccionada.id,
                proveedorid = ProveedorSeleccionado.id,
                Categoria = CategoriaSeleccionada,
                Proveedor = ProveedorSeleccionado
            };

            _context.Productos.Add(nuevoProducto);
            _context.SaveChanges();
            Productos.Add(nuevoProducto);
        }

        // 🔹 Actualizar un producto existente
        private void ActualizarProducto(object parameter)
        {
            if (ProductoSeleccionado != null)
            {
                _context.Productos.Update(ProductoSeleccionado);
                _context.SaveChanges();
                LoadProductos();
            }
        }

        // 🔹 Eliminar un producto seleccionado
        private void EliminarProducto(object parameter)
        {
            if (ProductoSeleccionado != null)
            {
                _context.Productos.Remove(ProductoSeleccionado);
                _context.SaveChanges();
                Productos.Remove(ProductoSeleccionado);
            }
        }

        // 🔹 Guardar cambios en la base de datos
        private void GuardarCambios(object parameter)
        {
            _context.SaveChanges();
            LoadProductos();
        }

        // 🔹 Verifica si se puede actualizar o eliminar un producto
        private bool CanExecuteActualizarOEliminar(object parameter)
        {
            return ProductoSeleccionado != null;
        }

        // 🔹 Verifica si se puede agregar un producto
        private bool CanExecuteAgregar(object parameter)
        {
            return CategoriaSeleccionada != null && ProveedorSeleccionado != null;
        }
    }
}