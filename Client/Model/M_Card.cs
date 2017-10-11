using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    class M_Card : INotifyPropertyChanged
    {
        private Model.EF.Cards data;

        public M_Card(Model.EF.Cards card)
        {
            this.data = card;
        }

        public M_Card()
        {
            this.data = new Model.EF.Cards();
            data.Users_ID = Model.EF.EntityInstance.UserID;
            data.MakeDate = DateTime.Now;
            data.Amount = 0.0;
        }

        public void Save()
        {
            Model.EF.EntityInstance.DBContext.CardsSet.Add(data);
            Model.EF.EntityInstance.DBContext.SaveChanges();
            OnPropertyChanged("Owner");
        }

        public int Id
        {
            get { return data.Cards_ID; }
        }

        public string Owner
        {
            get { return data.Cards_ID.ToString(); }
        }

        public string ObjectView
        {
            get { return "Objet"; }
        }

        public string Address
        {
            get { return "Address"; }
        }

        public double UU
        {
            get { return data.Amount; }
        }

        public string PKP
        {
            get { return "Аларм-12"; }
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
