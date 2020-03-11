using Client.Additional;
using System;
using System.ComponentModel;
using System.Linq;
using Client.Application.EF;

namespace Client.Model
{
    class M_Card
    {
        private Model.EF.Cards data;
        private string userName;

        public M_Card(Model.EF.Cards data = null)
        {
            if (data == null)
            {
                this.data = new Model.EF.Cards();

                var temp = EntityInstance.DBContext.UsersSet.AsNoTracking().First(p => p.Users_ID == EntityInstance.UserID);
                UserName = temp.Place + " " + temp.Name;

                this.data.Users_ID = EntityInstance.UserID;
                this.data.MakeDate = DateTime.Now;
                this.data.Amount = 0.0;
            }
            else
            {
                this.data = data;
                var temp = EntityInstance.DBContext.UsersSet.AsNoTracking().First(p => p.Users_ID == data.Users_ID);
                UserName = temp.Place + " " + temp.Name;
            }
        }

        public void Save()
        {
            if (data.Cards_ID == 0)
            {
                EntityInstance.DBContext.CardsSet.Add(data);
            }
            EntityInstance.DBContext.SaveChanges();
        }

        public int Id
        {
            get { return data.Cards_ID; }
        }

        public string Owner
        {
            get { return data.OwnerView; }
            set 
            { 
                data.OwnerView = value;
                OnPropertyChanged("Owner");
            }
        }

        public string ObjectView
        {
            get { return data.ObjectView; }
            set { data.ObjectView = value; }
        }

        public string Address
        {
            get { return data.AddressView; }
            set { data.AddressView = value; }
        }

        public double UU
        {
            get { return data.Amount; }
            set { data.Amount = value; }
        }

        public string PKP
        {
            get { return data.PKPView; }
            set { data.PKPView = value; }
        }

        public DateTime MakeDate
        {
            get { return data.MakeDate; }
            set { data.MakeDate = value; }
        }

        public int User
        {
            get { return data.Users_ID; }
            set { data.Users_ID = value; }
        }

        public string UserName
        {
            get { return userName; }
            set 
            { 
                this.userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
