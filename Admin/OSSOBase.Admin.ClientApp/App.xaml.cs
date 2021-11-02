using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OSSOBase.Admin.ClientApp.Views;
using System.IO;
using System.Windows;

namespace OSSOBase.Admin.ClientApp
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public IConfiguration Configuration { get; private set; }

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
