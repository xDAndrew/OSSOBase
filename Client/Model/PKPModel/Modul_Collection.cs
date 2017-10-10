using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.PKPModel
{
    class Modul_Collection
    {
        ObservableCollection<Modul_Item> items = new ObservableCollection<Modul_Item>();

        public void SetUpdateStatus(Modul_Item.Update method)
        {
            foreach (var item in items)
            {
                item.UpdateCount += method;
            }
        }

        public ObservableCollection<Modul_Item> Items
        {
            get
            {
                return items;
            }
        }

        public void Save(int ID)
        {
            var temp = Model.EF.EntityInstance.DBContext.PKP_ModulesSet.Where(p => p.PKP_ID == ID).ToList();

            while (temp.Count > 0)
            {
                Model.EF.EntityInstance.DBContext.PKP_ModulesSet.Remove(temp[0]);
                temp.RemoveAt(0);
            }

            foreach (var item in items)
            {
                item.Save(ID);
            }
        }

        public void Load(int ID)
        {
            var temp = EF.EntityInstance.DBContext.PKP_ModulesSet.Where(p => p.PKP_ID == ID).ToList();
            foreach (var item in temp)
            {
                var tempItem = new Modul_Item(EF.EntityInstance.DBContext.ModulesSet.First(p => p.Modules_ID == item.Modules_ID));
                tempItem.Count = item.Count.ToString();
                items.Add(tempItem);
            }
        }

        public double FullSumm
        {
            get
            {
                double summ = 0.0;
                foreach (var item in items)
                {
                    summ += item.UUSumm;
                }
                summ += 0.3;
                return summ;
            }
        }
    }
}
