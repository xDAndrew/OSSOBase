using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
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
            Model.EF.EntityInstance.UserID = int.Parse(ConfigurationManager.AppSettings["userId"]);
            this.DataContext = new ViewModel.VM_MainWindow(this);
        }

        private void MG_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            MG.Focus();
        }
    }
}
