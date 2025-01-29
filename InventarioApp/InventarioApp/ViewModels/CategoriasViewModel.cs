using System.Collections.ObjectModel;
using InventarioApp.Models;
using InventarioApp.Data;

namespace InventarioApp.ViewModels
{
    public class CategoriasViewModel : ViewModelBase
    {
        private readonly AppDbContext _context;

        private ObservableCollection<Categoria> _categorias;
        public ObservableCollection<Categoria> Categorias
        {
            get => _categorias;
            set
            {
                _categorias = value;
                OnPropertyChanged(); // Notifica el cambio a la vista
            }
        }

        public CategoriasViewModel(AppDbContext context)
        {
            _context = context;
            LoadCategorias(); // Carga las categorías desde la base de datos
        }

        private void LoadCategorias()
        {
            var categorias = _context.Categorias.ToList(); // Obtén las categorías
            Categorias = new ObservableCollection<Categoria>(categorias);
        }
    }
}
