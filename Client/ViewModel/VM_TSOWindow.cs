using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data;

namespace Client.ViewModel
{
    class VM_TSOWindow : INotifyPropertyChanged
    {
        View.TSOWindow WinLink;
        Model.PKPModel.Modul_Collection OriginalLink;

        ObservableCollection<Model.EF.Modules> modulsList = new ObservableCollection<Model.EF.Modules>();
        Model.PKPModel.Modul_Collection moduls = new Model.PKPModel.Modul_Collection();
        

        public VM_TSOWindow(Model.PKPModel.Modul_Collection data, View.TSOWindow WinReference)
        {
            this.OriginalLink = data;
            this.WinLink = WinReference;

            //Клонируем оригинальный список и работаем дальше с ним
            for (int i = 0; i < data.Count; i++)
            {
                moduls.Add(OriginalLink[i]);
            }

            moduls.SetUpdateStatus(UpdateSumm);
            LoadModulsList();

            OnPropertyChanged("SetButtonStatus");
            OnPropertyChanged("DelButtonStatus");
        }

        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged("Index");
            }
        }

        public bool DelButtonStatus
        {
            get { return Index > -1 ? true : false; }
        }

        public bool SetButtonStatus
        {
            get { return  selectedItemLB > -1 ? true : false;}
        }

        private Model.PKPModel.Modul_Item selectedItemDG;
        public Model.PKPModel.Modul_Item SelectedItemDG
        {
            get { return selectedItemDG; }
            set 
            {
                selectedItemDG = value;
                OnPropertyChanged("DelButtonStatus");
            }
        }

        private int selectedItemLB;
        public int SelectedItemLB
        {
            get { return selectedItemLB; }
            set 
            { 
                selectedItemLB = value; 
                OnPropertyChanged("SelectedItemLB");
                OnPropertyChanged("DelButtonStatus");
            }
        }

        public ObservableCollection<Model.PKPModel.Modul_Item> Moduls
        {
            get { return moduls.Items; }
        }

        public ObservableCollection<Model.EF.Modules> ModulsList
        {
            get { return modulsList; }
        }

        public string Summ
        {
            get { return "Сумма: " + moduls.FullSumm + " у.у."; }
        }

        public string Count
        {
            get { return moduls.Count + "/11"; }
        }

        #region Commands
        private Command setModul;
        public Command SetModul
        {
            get
            {
                return setModul ?? (setModul = new Command(obj =>
                {
                    if (SelectedItemLB >= 0)
                    {
                        moduls.Add(new Model.PKPModel.Modul_Item(modulsList[selectedItemLB]));

                        modulsList.RemoveAt(selectedItemLB);
                        if (modulsList.Count > 0)
                        {
                            SelectedItemLB = 0;
                        }
                        Index = 0;
                        moduls.SetUpdateStatus(UpdateSumm);
                        OnPropertyChanged("SetButtonStatus");
                        OnPropertyChanged("Summ");
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
                    moduls.Delete(selectedItemDG);
                    if (moduls.Count > 0) Index = 0;
                    LoadModulsList();
                    OnPropertyChanged("SetButtonStatus");
                    OnPropertyChanged("DelButtonStatus");
                    OnPropertyChanged("Summ");
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
                    OriginalLink.Items.Clear();
                    for (int i = 0; i < moduls.Count; i++ )
                    {
                        OriginalLink.Add(moduls[i]);
                    }
                    
                    WinLink.Close();
                }));
            }
        }
        #endregion

        #region Service
        private void UpdateSumm()
        {
            OnPropertyChanged("Summ");
        }

        private void LoadModulsList()
        {
            modulsList.Clear();

            //Заполняет список доступных модулей
            var temp = Model.EF.EntityInstance.DBContext.ModulesSet.Where(p => p.Visible).ToList();
            foreach (var item in temp)
            {
                bool del = false;
                for (int i = 0; i < moduls.Count; i++)
                {
                    if (item.Modules_ID == moduls[i].Id)
                    {
                        del = true;
                        break;
                    }
                }
                if (!del)
                {
                    modulsList.Add(item);
                }
            }
            if (modulsList.Count > 0) SelectedItemLB = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
