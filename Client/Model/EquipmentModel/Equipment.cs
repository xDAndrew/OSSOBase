using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Client.Model.EquipmentModel
{
    public class Equipment : INotifyPropertyChanged
    {
        private TSO_Collection TSOModels = new TSO_Collection();
        public TSO_Collection Models
        {
            get { return TSOModels; }
        }

        private ObservableCollection<Branch> branches = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> Branches
        {
            get { return branches; }
        }

        private Branch results;
        public Branch Results
        {
            get { return results; }
        }

        public void Clear()
        {
            foreach (var item in branches)
            {
               for (int i = 0; i < 15; i++)
               {
                    item[i] = 0;
               }
            }
        }

        public void Save(int ID)
        {
            Models.Save(ID);

            var temp = Model.EF.EntityInstance.DBContext.BranchSet.Where(p => p.Cards_ID == ID).ToList();
            while (temp.Count > 0)
            {
                Model.EF.EntityInstance.DBContext.BranchSet.Remove(temp[0]);
                temp.RemoveAt(0);
            }
            Model.EF.EntityInstance.DBContext.SaveChanges();

            foreach (var item in branches)
            {
                item.Save(ID);
            }
            Model.EF.EntityInstance.DBContext.SaveChanges();
        }

        public int LimbsCount
        {
            get { return Branches.Count; }
            set { }
        }

        public void setUpdateMethod(Branch.Update method)
        {
            foreach (var item in branches)
            {
                item.UpdateStatus = method;
            }
        }

        public Equipment(int? ID = null)
        {
            if (ID != null)
            {
                var bTemp = Model.EF.EntityInstance.DBContext.BranchSet.OrderBy(p => p.Number).Where(p => p.Cards_ID == ID.Value).ToList();
                foreach (var item in bTemp)
                {
                    Branches.Add(new Model.EquipmentModel.Branch(item));
                }

                var temp = Model.EF.EntityInstance.DBContext.Cards_TSOSet.OrderBy(p => p.Number).Where(p => p.Cards_ID == ID.Value).ToList();
                foreach (var item in temp)
                {
                    TSOModels.Items.Add(new Model.EquipmentModel.TSO_Item(Model.EF.EntityInstance.DBContext.TSOSet.First(p => p.TSO_ID == item.TSO_ID)));                    
                }
            }

            results = new Branch(0);
            results.SetVisibleSetting(true);

            OnPropertyChanged("LimbsCount");
            OnPropertyChanged("Branches");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
