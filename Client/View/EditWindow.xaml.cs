using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Client.View
{
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((this.DataContext as ViewModel.VM_EditWindow).Changed)
            {
                if (System.Windows.MessageBox.Show("Сохранить внесенные изменения?", "Сохранить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    (this.DataContext as ViewModel.VM_EditWindow).SaveChange.Execute(new object());
                }
            }
        }
    }
}
