using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    class M_Card
    {
        private Model.EF.Cards data;
        private Model.EF.Object obj;
        private Model.EF.PKPModels pkp;
        private Model.EF.Streets street;

        public M_Card(Model.EF.Cards data = null)
        {
            if (data == null)
            {
                this.data = new Model.EF.Cards();
                this.data.Users_ID = Model.EF.EntityInstance.UserID;
                this.data.MakeDate = DateTime.Now;
                this.data.Amount = 0.0;
            }
            else
            {
                this.data = data;
                obj = Model.EF.EntityInstance.DBContext.ObjectSet.First(p => p.Cards_ID == this.data.Cards_ID);
                var temp = Model.EF.EntityInstance.DBContext.PKPSet.First(p => p.Cards_ID == this.data.Cards_ID);
                pkp = Model.EF.EntityInstance.DBContext.PKPModelsSet.First(p => p.PKPModels_ID == temp.PKPModels_ID);
                street = Model.EF.EntityInstance.DBContext.StreetsSet.First(p => p.Streets_ID == obj.Streets_ID);
            }

        }

        public void Save()
        {
            if (data.Cards_ID == 0)
            {
                Model.EF.EntityInstance.DBContext.CardsSet.Add(data);
            }
            Model.EF.EntityInstance.DBContext.SaveChanges();
        }

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

        public int Id
        {
            get { return data.Cards_ID; }
        }

        public string Owner
        {
            get { return obj.Owner; }
        }

        public string ObjectView
        {
            get { return obj.Name; }
        }

        public string Address
        {
            get 
            {
                string str = GetStreetType(street.Type) +  " " + street.Name + ", ";
                if (obj.Home != "") str += obj.Home;
                if (obj.Corp != "") str += "\\" + obj.Corp + " ";
                if (obj.Room != "") str += "- " + obj.Room;
                return str; 
            }
        }

        public double UU
        {
            get { return data.Amount; }
            set { data.Amount = value; }
        }

        public string PKP
        {
            get { return pkp.Name; }
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
