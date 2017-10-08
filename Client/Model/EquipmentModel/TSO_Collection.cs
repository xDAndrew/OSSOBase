using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Model.EquipmentModel
{
    class TSO_Collection
    {
        List<TSOModel> data = new List<TSOModel>();

        public TSO_Collection()
        {
            for (int i = 0; i < 15; i++)
            {
                var n = new Model.EF.TSO();
                n.Name = "Привет " + i.ToString();
                n.Group_ID = 3;
                data.Add(new TSOModel(n));
            }
        }

        public List<TSOModel> Items
        {
            get { return data; }
        }

        public string this[int index]
        {
            get
            {
                try
                {
                    return data[index].Name;
                }
                catch { }
                return "";
            }
        }
    }
}
