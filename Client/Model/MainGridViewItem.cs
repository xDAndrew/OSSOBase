using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    class MainGridViewItem
    {
        EF.Cards data;

        public MainGridViewItem(EF.Cards data)
        {
            this.data = data;
        }
    }
}
