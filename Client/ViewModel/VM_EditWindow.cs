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
        private View.EditWindow myHNDL;

        //Переменные карточки
        private Model.EF.Cards currentCard;
        private Model.EF.PKP currentPKP;
        private Model.EF.Object currentObject;
        private Model.EF.Users currentUser;

        #region Object_Properties
        public Model.EF.Object CurrentObject
        {
            get { return currentObject; }
            set { currentObject = value; }
        }

        //Наименования улиц
        ObservableCollection<String> streets = new ObservableCollection<String>();
        public ObservableCollection<String> Streets
        {
            get { return streets; }
        }

        //Выбранная улица
        public int SelectedStreetIndex
        {
            get { return currentObject.Streets_ID; }
            set 
            { 
                currentObject.Streets_ID = value;
                OnPropertyChanged("SelectedStreetIndex");
            }
        }
        #endregion

        #region PKP_Properties
        public Model.EF.PKP CurrentPKP
        {
            get { return currentPKP; }
            set { currentPKP = value; }
        }

        //Список приборов
        ObservableCollection<String> pkpList = new ObservableCollection<String>();
        public ObservableCollection<String> PKPList
        {
            get { return pkpList; }
        }

        //Выбранный прибор
        public int SelectedPKPIndex
        {
            get { return currentPKP.Name; }
            set
            {
                currentPKP.Name = value;
                OnPropertyChanged("SelectedPKPIndex");
            }
        }
        #endregion

        #region Equipment_properties
        private ObservableCollection<Model.Limb> limbs = new ObservableCollection<Model.Limb>();
        public ObservableCollection<Model.Limb> Limbs
        {
            get
            {
                return limbs;
            }
        }

        public int LimbsCount
        {
            get
            {
                return limbs.Count;
            }
            set { }
        }

        Model.Limb TSO_Summ;
        public Model.Limb TSOSumm
        {
            get
            {
                return TSO_Summ;
            }
        }
        #endregion

        #region StatusBar_Properties
        public string Date
        {
            get { return currentCard.MakeDate.ToString("dd.MM.yyyy"); }
        }

        public string Maker
        {
            get { return currentUser.Place + " " + currentUser.Name; }
        }
        #endregion

        #region ServicesMetods
        private string GetStreetType(int index)
        {
            switch (index)
            {
                case 0:
                    return "тр.";
                case 1:
                    return "пр.";
                case 2:
                    return "ул.";
                case 3:
                    return "пер.";
                case 4:
                    return "пр-д";
                case 5:
                    return "т.";
                case 6:
                    return "пл.";
                default:
                    return "";
            }
            
        }

        void LoadComboBoxItems()
        {
            var sTemp = Model.EF.EntityInstance.DBContext.StreetsSet.OrderBy(p => p.Name).Where(p => p.Streets_ID > 0).ToList();
            foreach (var item in sTemp)
            {
                streets.Add(item.Name + " " + GetStreetType(item.Type));
            }

            var PKPGroupIndex = Model.EF.EntityInstance.DBContext.TSOGroupSet.First(p => p.Type == 0);
            var pkpTemp = Model.EF.EntityInstance.DBContext.TSOSet.OrderBy(p => p.Name).Where(p => p.TSOGroup_ID == PKPGroupIndex.TSOGroup_ID && p.Visible == true).ToList();
            foreach (var item in pkpTemp)
            {
                pkpList.Add(item.Name);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        public VM_EditWindow(View.EditWindow HNDL, int? CurrentCardId = null)
        {
            myHNDL = HNDL;

            if (CurrentCardId == null)
            {
                currentCard = new Model.EF.Cards();
                currentObject = new Model.EF.Object();
                currentPKP = new Model.EF.PKP();

                LoadComboBoxItems();

                if (Model.EF.EntityInstance.UserID > 0)
                {
                    currentUser = Model.EF.EntityInstance.DBContext.UsersSet.First(p => p.Users_ID == Model.EF.EntityInstance.UserID);
                }
                currentCard.MakeDate = DateTime.Now;

                for (int i = 0; i < 1; i++)
                {
                    limbs.Add(new Model.Limb(new Model.EF.Limb()));
                }

                TSO_Summ = new Model.Limb();
                foreach (var item in limbs)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        TSO_Summ[i] += item[i];
                    }
                }

                currentObject.Owner = "Owner";
                currentObject.Name = "Name";
                currentObject.Home = "Home";
                currentObject.Corp = "Corp";
                currentObject.Room = "Room";

                currentPKP.Name = -1;
                currentPKP.Date = DateTime.Now;
                currentPKP.Serial = "Serial";
                currentPKP.Password = "Password";
                currentPKP.Phone = "Phone";

                currentObject.Streets_ID = -1;
            }
            else
            {

            }
        }

        #region Commands
        private Command addLimb;
        public Command AddLimb
        {
            get
            {
                return addLimb ?? (addLimb = new Command(obj =>
                {
                    limbs.Add(new Model.Limb(new Model.EF.Limb()));
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
                    if (limbs.Count > 0)
                    {
                        limbs.RemoveAt(limbs.Count - 1);
                    }
                    OnPropertyChanged("LimbsCount");
                }));
            }
        }
        #endregion
    }
}
