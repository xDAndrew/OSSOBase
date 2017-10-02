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
        Model.EF.Cards currentCard;
        Model.EF.Users currentUser;
        Model.EF.Object currentObject;

        ObservableCollection<Model.EF.Limb> limbs = new ObservableCollection<Model.EF.Limb>();
        public ObservableCollection<Model.EF.Limb> Limbs
        {
            get
            {
                return limbs;
            }
        }


        #region ObjectProperties
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

        public int SelectedIndex
        {
            get { return currentObject.Streets_ID; }
            set 
            { 
                currentObject.Streets_ID = value;
                OnPropertyChanged("SelectedIndex");
            }
        }
        #endregion
        #region StatusBarProperties
        public string Date
        {
            get { return currentCard.MakeDate.ToString("dd.MM.yyyy"); }
        }

        public string Maker
        {
            get { return currentUser.Place + " " + currentUser.Name; }
        }
        #endregion

        public VM_EditWindow(View.EditWindow HNDL, int? CurrentCard = null)
        {
            myHNDL = HNDL;

            var sTemp = Model.EF.EntityInstance.DBContext.StreetsSet.Where(p => p.Streets_ID > 0).ToList();
            foreach (var item in sTemp)
            {
                string tmp_str = item.Name + " ";
                switch (item.Type)
                {
                    case 0:
                        tmp_str += "тр.";
                        break;
                    case 1:
                        tmp_str += "пр.";
                        break;
                    case 2:
                        tmp_str += "ул.";
                        break;
                    case 3:
                        tmp_str += "пер.";
                        break;
                    case 4:
                        tmp_str += "пр-д";
                        break;
                    case 5:
                        tmp_str += "т.";
                        break;
                    case 6:
                        tmp_str += "пл.";
                        break;
                }
                streets.Add(tmp_str);
            }

            if (CurrentCard == null)
            {
                var o = new Model.EF.Object();
                o.Owner = "Пуцко";
                o.Name = "Хата";
                o.Home = "10";
                o.Corp = "a";
                o.Room = "50";
                o.Streets_ID = 0;
                CurrentObject = o;

                currentUser = Model.EF.EntityInstance.DBContext.UsersSet.First(p => p.Users_ID == Model.EF.EntityInstance.UserID);
                currentCard = new Model.EF.Cards();
                currentCard.MakeDate = DateTime.Now;
            }
            else
            {

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
