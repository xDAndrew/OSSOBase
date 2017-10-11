using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.EquipmentModel
{
    class Equipment : INotifyPropertyChanged
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
                item.Clear();
            }
        }

        public void Save(int ID)
        {
            foreach (var item in branches)
            {
                item.Save(ID);
            }

            for (int i = 0; i < 15; i++ )
            {
                if (i < Models.Items.Count)
                    Models.Items[i].Save(ID, (byte)(i + 1));
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

        public Equipment()
        {
            results = new Branch(0);
            results.SetVisibleSetting(true);

            var b = Model.EF.EntityInstance.DBContext.BranchSet.Where(p => p.Cards_ID == 1).ToList();
            foreach (var item in b)
            {
                Branches.Add(new Model.EquipmentModel.Branch(item));
            }
        }

        #region ServicesMetods
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
