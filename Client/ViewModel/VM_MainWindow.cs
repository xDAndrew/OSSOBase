using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    class VM_MainWindow
    {
        MainWindow WinHANDLE = null;
        //ObservableCollection<> MainGrid = new ObservableCollection<>();

        public VM_MainWindow(MainWindow MW)
        {
            WinHANDLE = MW;
        }
    }
}