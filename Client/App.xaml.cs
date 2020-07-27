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
            var errorMessage = $"{DateTime.Now.ToShortDateString()}: An unhandled exception occurred: {e.Exception.Message}\n";
            File.AppendAllText("log.txt", errorMessage);
        }
    }
}
