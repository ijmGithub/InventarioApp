using System.Windows;
using InventarioApp.Data;
using InventarioApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventarioApp
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Registrar AppDbContext con Npgsql
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("Host=localhost;Database=InventarioDB;Username=postgres;Password=sifo88916717967"));

            // Crear instancia del contexto y pasarlo al MainViewModel
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

            // Crear la ventana principal y pasar el MainViewModel
            var mainWindow = new MainWindow
            {
                DataContext = new MainViewModel(dbContext)
            };

            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }
}