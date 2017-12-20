using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Model.EF.EntityInstance.UserID = int.Parse((string)ConfigurationManager.AppSettings["userId"]);
            this.Show();
            this.DataContext = new ViewModel.VM_MainWindow(this);
        }

        private void RowDoubleClick(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel.VM_MainWindow).EditCard.Execute(new object());
        }
    }
}
