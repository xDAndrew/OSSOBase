using Client.Additional;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Client.Application.EF;

namespace Client.ViewModel
{
    class VM_TSOEditWindow : INotifyPropertyChanged
    {
        View.TSOEditWindow WinLink;

        private ObservableCollection<Model.EF.TSO> TSOList = new ObservableCollection<Model.EF.TSO>();
        public ObservableCollection<Model.EF.TSO> TSO
        {
            get { return TSOList; }
        }

        private ObservableCollection<Model.EF.TSOGroup> groups = new ObservableCollection<Model.EF.TSOGroup>();
        public ObservableCollection<Model.EF.TSOGroup> Groups
        {
            get { return groups; }
        }

        private int groupsIndex = -1;
        public int GroupsIndex
        {
            get { return groupsIndex; }
            set 
            {
                groupsIndex = value;
                LoadTSOList(value);
                if (TSOList.Count > 0)
                {
                    TSOIndex = 0;
                }
            }
        }

        private int TSO_index = -1;
        public int TSOIndex
        {
            get { return TSO_index; }
            set 
            { 
                TSO_index = value;
                OnPropertyChanged("TSOIndex");
                OnPropertyChanged("DelButtonStatus");
                OnPropertyChanged("SetButtonStatus");
            }
        }

        private int activeTSOIndex = -1;
        public int ActiveTSOIndex
        {
            get { return activeTSOIndex; }
            set 
            { 
                activeTSOIndex = value;
                OnPropertyChanged("ActiveTSOIndex");
                OnPropertyChanged("DelButtonStatus");
                OnPropertyChanged("SetButtonStatus");
            }
        }

        private Model.EquipmentModel.TSO_Collection activeTSOListLink;
        private Model.EquipmentModel.TSO_Collection tempActiveTSOList = new Model.EquipmentModel.TSO_Collection();
        public ObservableCollection<Model.EquipmentModel.TSO_Item> ActiveTSOList
        {
            get { return tempActiveTSOList.Items; }
        }

        public bool DelButtonStatus
        {
            get { return activeTSOIndex > -1 ? true : false; }
        }

        public bool SetButtonStatus
        {
            get { return TSO_index > -1 ? true : false; }
        }

        public string Count
        {
            get { return "Элементы: " + tempActiveTSOList.Items.Count.ToString() + "/15"; }
        }

        public VM_TSOEditWindow(View.TSOEditWindow link, Model.EquipmentModel.TSO_Collection activeTSOList)
        {
            this.WinLink = link;
            this.activeTSOListLink = activeTSOList;
            tempActiveTSOList.Items.Clear();
            foreach (var item in activeTSOList.Items)
            {
                tempActiveTSOList.Items.Add(item);
            }

            var temp = EntityInstance.DBContext.TSOGroupSet.Where(p => p.TSOGroup_ID > 0).ToList();
            foreach (var item in temp)
            {
                groups.Add(item);
            }

            if (groups.Count > 0)
            {
                GroupsIndex = 0;
            }
        }

        private void LoadTSOList(int ID)
        {
            TSO.Clear();
            int index = groups[ID].TSOGroup_ID;
            var temp = EntityInstance.DBContext.TSOSet.Where(p => p.Group_ID == index).ToList();
            foreach (var item in temp)
            {
                bool del = false;
                for (int i = 0; i < tempActiveTSOList.Items.Count; i++)
                {
                    if (item.TSO_ID == tempActiveTSOList.Items[i].Id)
                    {
                        del = true;
                        break;
                    }
                }
                if (!del)
                {
                    TSO.Add(item);
                }
            }
        }

        private Command setModul;
        public Command SetModul
        {
            get
            {
                return setModul ?? (setModul = new Command(obj =>
                {
                    if (TSO_index > -1 && tempActiveTSOList.Items.Count < 15)
                    {
                        tempActiveTSOList.Items.Add(new Model.EquipmentModel.TSO_Item(TSOList[TSO_index]));

                        TSO.Remove(TSOList[TSO_index]);
                        if (TSOList.Count > 0)
                        {
                            TSOIndex = 0;
                        }

                        if (ActiveTSOList.Count > 0 && ActiveTSOIndex == -1)
                        {
                            ActiveTSOIndex = 0;
                        }
                        OnPropertyChanged("DelButtonStatus");
                        OnPropertyChanged("SetButtonStatus");
                        OnPropertyChanged("Count");
                    }
                }));
            }
        }

        private Command delModul;
        public Command DelModul
        {
            get
            {
                return delModul ?? (delModul = new Command(obj =>
                {
                    if (activeTSOIndex > -1)
                    {
                        tempActiveTSOList.Items.Remove(tempActiveTSOList.Items[activeTSOIndex]);
                    }
                    LoadTSOList(GroupsIndex);
                    if (TSOList.Count > 0)
                    {
                        TSOIndex = 0;
                    }
                    if (ActiveTSOList.Count > 0)
                    {
                        ActiveTSOIndex = 0;
                    }
                    OnPropertyChanged("DelButtonStatus");
                    OnPropertyChanged("SetButtonStatus");
                    OnPropertyChanged("Count");
                }));
            }
        }

        private Command saveChange;
        public Command SaveChange
        {
            get
            {
                return saveChange ?? (saveChange = new Command(obj =>
                {

                    activeTSOListLink.Items.Clear();
                    for (int i = 0; i < tempActiveTSOList.Items.Count; i++)
                    {
                        activeTSOListLink.Items.Add(tempActiveTSOList.Items[i]);
                    }

                    WinLink.Close();
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
