using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    class VM_EditWindow : INotifyPropertyChanged
    {
        private View.EditWindow WinLink;
        
        private Model.M_Object currentObject;
        public Model.M_Object CurrentObject
        { 
            get 
            { 
                return currentObject; 
            } 
        }

        private Model.M_PKP currentPKP;
        public Model.M_PKP CurrentPKP
        { 
            get 
            { 
                return currentPKP; 
            } 
        }

        private Model.EquipmentModel.Equipment currentEquipment;
        public Model.EquipmentModel.Equipment CurrentEquipment 
        { 
            get 
            { 
                return currentEquipment;
            } 
        }

        private Model.M_Card currentCard;
        public Model.M_Card CurrentCard
        {
            get { return currentCard; }
        }
        //private Model.EF.Users currentUser;

        #region StatusBar_Properties
        //public string Date
        //{
        //    get { return currentCard.MakeDate.ToString("dd.MM.yyyy"); }
        //}

        //public string Maker
        //{
        //    get { return currentUser.Place + " " + currentUser.Name; }
        //}
        #endregion

        public VM_EditWindow(View.EditWindow HNDL, int? CurrentCardId = null)
        {
            WinLink = HNDL;

            if (CurrentCardId == null)
            {
                currentCard = new Model.M_Card(new Model.EF.Cards());

                currentObject = new Model.M_Object();
                currentPKP = new Model.M_PKP(WinLink);
                currentEquipment = new Model.EquipmentModel.Equipment();
            }
            else
            {

            }
        }

        public void CountUU()
        {
            for (int i = 0; i < 15; i++)
            {
                currentEquipment.Results[i] = 0;
            }
            currentEquipment.Results.Summ = 0;

            foreach (var item in currentEquipment.Branches)
            {
                item.Summ = 0.0;
                for (int i = 0; i < 15; i++)
                {
                    if (i < currentEquipment.Models.Items.Count)
                    {
                        item.Summ += item[i] * currentEquipment.Models.Items[i].UU;
                    }
                    else
                    {
                        item[i] = 0;
                    }
                    currentEquipment.Results[i] += item[i];
                }

                int iterator = 0;
                int n = 0;
                while (iterator < item.SummTSO)
                {
                    if (iterator % 10 == 0)
                    {
                        n++;
                    }
                    iterator++;
                }

                if (item.SummTSO > 0)
                {
                    double temp = n * 0.05;
                    item.Summ += temp;
                    if (item.Number == 1 && item.Summ >= 0.05) item.Summ -= 0.05;
                }

                currentEquipment.Results.Summ += item.Summ;
            }

            currentEquipment.Results.Summ += currentPKP.Moduls.FullSumm;
            OnPropertyChanged("CurrentEquipment");
        }

        #region Commands
        private Command openTSOEdit;
        public Command OpenTSOEdit
        {
            get
            {
                return openTSOEdit ?? (openTSOEdit = new Command(obj =>
                {
                    var wTemp = new View.TSOEditWindow();
                    var cTemp = new ViewModel.VM_TSOEditWindow(wTemp, currentEquipment.Models);
                    wTemp.Owner = WinLink;
                    wTemp.DataContext = cTemp;
                    wTemp.ShowDialog();
                    CountUU();
                    currentEquipment.Clear();
                }));
            }
        }

        private Command addLimb;
        public Command AddLimb
        {
            get
            {
                return addLimb ?? (addLimb = new Command(obj =>
                {
                    byte t = (byte)currentEquipment.LimbsCount;
                    t++;
                    currentEquipment.Branches.Add(new Model.EquipmentModel.Branch(t));
                    currentEquipment.setUpdateMethod(CountUU);
                    CountUU();
                    OnPropertyChanged("LimbsCount");
                }));
            }
        }

        private Command delLimb;
        public Command DelLimb
        {
            get
            {
                return delLimb ?? (delLimb = new Command(obj =>
                {
                    if (currentEquipment.Branches.Count > 0)
                    {
                        currentEquipment.Branches.RemoveAt(currentEquipment.Branches.Count - 1);
                        CountUU();
                    }
                    OnPropertyChanged("LimbsCount");
                }));
            }
        }
        #endregion

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
