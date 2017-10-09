using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    class VM_TSOEditWindow
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
            }
        }

        private int TSO_index;
        public int TSOIndex
        {
            get { return TSO_index; }
            set { TSO_index = value; }
        }

        private int activeTSOIndex;
        public int ActiveTSOIndex
        {
            get { return activeTSOIndex; }
            set { activeTSOIndex = value; }
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

            var temp = Model.EF.EntityInstance.DBContext.TSOGroupSet.Where(p => p.TSOGroup_ID > 0).ToList();
            foreach (var item in temp)
            {
                groups.Add(item);
            }

            if (groups.Count > 0)
            {
                GroupsIndex = 0;
            }
        }

        #region Service
        private void LoadTSOList(int ID)
        {
            TSO.Clear();
            int index = groups[ID].TSOGroup_ID;
            var temp = Model.EF.EntityInstance.DBContext.TSOSet.Where(p => p.Group_ID == index).ToList();
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
        #endregion

        #region Commands
        private Command setModul;
        public Command SetModul
        {
            get
            {
                return setModul ?? (setModul = new Command(obj =>
                {
                    if (TSO_index > -1)
                    {

                        tempActiveTSOList.Items.Insert((activeTSOIndex > -1 ? activeTSOIndex : 0), new Model.EquipmentModel.TSO_Item(TSOList[TSO_index]));
                        TSO.Remove(TSOList[TSO_index]);
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
        #endregion
    }
}
