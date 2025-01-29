using System.Windows.Input;
using InventarioApp.Data;

namespace InventarioApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private readonly AppDbContext _context;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(); // Notifica el cambio de la vista actual
            }
        }

        public ICommand NavigateCommand { get; }

        public MainViewModel(AppDbContext context)
        {
            _context = context; // Recibe el contexto directamente
            NavigateCommand = new RelayCommand(Navigate);
            CurrentViewModel = new ProductosViewModel(_context); // Vista predeterminada
        }

        private void Navigate(object parameter)
        {
            switch (parameter as string)
            {
                case "Productos":
                    CurrentViewModel = new ProductosViewModel(_context);
                    break;
                case "Categorias":
                    CurrentViewModel = new CategoriasViewModel(_context);
                    break;
                case "Proveedores":
                    CurrentViewModel = new ProveedoresViewModel(_context);
                    break;
            }
        }
    }
}
