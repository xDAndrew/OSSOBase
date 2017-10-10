using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace Client.Model.EquipmentModel
{
    class TSO_Collection
    {
        public TSO_Collection()
        {
            //var temp = Model.EF.EntityInstance.DBContext.TSOSet.Where(p => p.TSO_ID % 2 == 0).ToList();
            //foreach (var item in temp)
            //{
            //    data.Add(new TSO_Item(item));
            //}
        }

        ObservableCollection<TSO_Item> data = new ObservableCollection<TSO_Item>();
        public ObservableCollection<TSO_Item> Items
        {
            get { return data; }
        }

        public string this[int index]
        {
            get
            {
                if (index < data.Count)
                {
                    return data[index].Name;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
