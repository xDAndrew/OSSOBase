using Client.ViewModel;
using System.Configuration;
using System.Windows;

namespace Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Model.EF.EntityInstance.UserID = int.Parse(ConfigurationManager.AppSettings["userId"]);
            Show();
            DataContext = new VM_MainWindow(this);
        }

        private void RowDoubleClick(object sender, RoutedEventArgs e)
        {
            ((VM_MainWindow)DataContext).EditCard.Execute(new object());
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var context = ((VM_MainWindow)DataContext);
            context.UpdateGrid();
            DataContext = context;
            e.Handled = true;
        }
    }
}
