using Client.Additional;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

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
            for (int i = 0; i < data.Items.Count; i++)
            {
                moduls.Items.Add(OriginalLink.Items[i]);
            }

            moduls.SetUpdateStatus( () => { OnPropertyChanged("Summ"); });
            LoadModulsList();

            if (Moduls.Count > 0)
            {
                Index = 0;
            }
            else
            {
                Index = -1;
            }

            if (ModulsList.Count > 0)
            {
                SelectedItemLB = 0;
            }
            else
            {
                SelectedItemLB = -1;
            }
            
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
                OnPropertyChanged("SetButtonStatus");
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
            get { return moduls.Items.Count + "/11"; }
        }

        private Command setModul;
        public Command SetModul
        {
            get
            {
                return setModul ?? (setModul = new Command(obj =>
                {
                    if (SelectedItemLB >= 0)
                    {
                        if (moduls.Items.Count < 11)
                        {
                            moduls.Items.Add(new Model.PKPModel.Modul_Item(modulsList[selectedItemLB]));
                            modulsList.RemoveAt(selectedItemLB);
                            if (modulsList.Count > 0)
                            {
                                SelectedItemLB = 0;
                            }
                            Index = 0;
                            moduls.SetUpdateStatus( () => { OnPropertyChanged("Summ"); });
                            OnPropertyChanged("SetButtonStatus");
                            OnPropertyChanged("Summ");
                            OnPropertyChanged("Count");
                        }
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
                    moduls.Items.Remove(selectedItemDG);
                    if (moduls.Items.Count > 0) Index = 0;
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
                    for (int i = 0; i < moduls.Items.Count; i++ )
                    {
                        OriginalLink.Items.Add(moduls.Items[i]);
                    }
                    
                    WinLink.Close();
                }));
            }
        }

        private void LoadModulsList()
        {
            //Заполняет список доступных модулей
            modulsList.Clear();
            var temp = Model.EF.EntityInstance.DBContext.ModulesSet.Where(p => p.Visible).ToList();
            foreach (var item in temp)
            {
                bool del = false;
                for (int i = 0; i < moduls.Items.Count; i++)
                {
                    if (item.Modules_ID == moduls.Items[i].Id)
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
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
