using System.Collections.ObjectModel;
using InventarioApp.Models;
using InventarioApp.Data;

namespace InventarioApp.ViewModels
{
    public class ProveedoresViewModel : ViewModelBase
    {
        private readonly AppDbContext _context;

        public ObservableCollection<Proveedor> Proveedores { get; set; }

        public ProveedoresViewModel(AppDbContext context)
        {
            _context = context;
            LoadProveedores();
        }

        private void LoadProveedores()
        {
            var proveedores = _context.Proveedores.ToList();
            Proveedores = new ObservableCollection<Proveedor>(proveedores);
        }
    }
}
