using System.Linq;
using System.Collections.ObjectModel;
using Client.Application.EF;

namespace Client.Model.EquipmentModel
{
    class TSO_Collection
    {
        public void Save(int ID)
        {
            //Удалить уже сохраненные
            var temp = EntityInstance.DBContext.Cards_TSOSet.Where(p => p.Cards_ID == ID).ToList();
            foreach (var item in temp)
            {
                EntityInstance.DBContext.Cards_TSOSet.Remove(item);
            }

            //Пересохраниться
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Save(ID, (byte)i);
            }
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