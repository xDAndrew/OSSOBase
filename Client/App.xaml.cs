using System;
using System.IO;
using System.Windows;

namespace Client
{
    public partial class App : Application
    {
        public App()
        {
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        private static void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = $"\n{DateTime.Now:dd.MM.yyyy hh:mm:ss}: {e.Exception}\n";
            File.AppendAllText("log.txt", errorMessage);
        }
    }
}
