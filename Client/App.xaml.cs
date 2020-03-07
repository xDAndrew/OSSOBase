using System.Windows;
using Ninject;

namespace Client
{
    public partial class App
    {
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow?.Show();
        }

        private void ConfigureContainer()
        {
            _container = new StandardKernel();
            //_container.Bind<IWeapon>().To<Sword>().InTransientScope();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainWindow>();
        }
    }
}
