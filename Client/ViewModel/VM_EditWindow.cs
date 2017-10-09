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
            get { return currentObject; }
            set { currentObject = value; }
        }

        private Model.M_PKP currentPKP;
        public Model.M_PKP CurrentPKP
        {
            get { return currentPKP; }
            set { currentPKP = value; }
        }

        private Model.EquipmentModel.Equipment currentEquipment;
        public Model.EquipmentModel.Equipment CurrentEquipment
        {
            get { return currentEquipment; }
        }

        //Переменные карточки
        private Model.EF.Cards currentCard;
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
                currentObject = new Model.M_Object();
                currentPKP = new Model.M_PKP(WinLink);
                currentEquipment = new Model.EquipmentModel.Equipment();

                currentCard = new Model.EF.Cards();
            }
            else
            {

            }
        }

        #region Commands
        //private Command addLimb;
        //public Command AddLimb
        //{
        //    get
        //    {
        //        return addLimb ?? (addLimb = new Command(obj =>
        //        {
        //            limbs.Add(new Model.Limb(new Model.EF.Limb()));
        //            OnPropertyChanged("LimbsCount");
        //        }));
        //    }
        //}

        //private Command delLimb;
        //public Command DelLimb
        //{
        //    get
        //    {
        //        return delLimb ?? (delLimb = new Command(obj =>
        //        {
        //            if (limbs.Count > 0)
        //            {
        //                limbs.RemoveAt(limbs.Count - 1);
        //            }
        //            OnPropertyChanged("LimbsCount");
        //        }));
        //    }
        //}
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
