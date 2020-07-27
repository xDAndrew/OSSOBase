using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Client.Model
{
    class M_Card
    {
        private EF.Cards data;
        private string userName;

        public M_Card(EF.Cards data = null)
        {
            if (data == null)
            {
                this.data = new EF.Cards();

                var temp = EF.EntityInstance.DBContext.UsersSet.AsNoTracking().First(p => p.Users_ID == EF.EntityInstance.UserID);
                UserName = temp.Place + " " + temp.Name;

                this.data.Users_ID = EF.EntityInstance.UserID;
                this.data.MakeDate = DateTime.Now;
                this.data.Amount = 0.0;
                this.data.Contract = string.Empty;
            }
            else
            {
                this.data = data;
                var temp = EF.EntityInstance.DBContext.UsersSet.AsNoTracking().First(p => p.Users_ID == data.Users_ID);
                UserName = temp.Place + " " + temp.Name;
            }
        }

        public bool Changed { get; set; }

        public void Save()
        {
            if (data.Cards_ID == 0)
            {
                EF.EntityInstance.DBContext.CardsSet.Add(data);
            }
            EF.EntityInstance.DBContext.SaveChanges();
        }

        public int Id => data.Cards_ID;

        public string Owner
        {
            get => data.OwnerView;
            set 
            { 
                data.OwnerView = value;
                OnPropertyChanged("Owner");
            }
        }

        public string Contract
        {
            get => data.Contract;
            set
            {
                data.Contract = value;
                Changed = true;
            }
        }

        public string ObjectView
        {
            get => data.ObjectView;
            set => data.ObjectView = value;
        }

        public string Address
        {
            get => data.AddressView;
            set => data.AddressView = value;
        }

        public double UU
        {
            get => data.Amount;
            set => data.Amount = value;
        }

        public string PKP
        {
            get => data.PKPView;
            set => data.PKPView = value;
        }

        public DateTime MakeDate
        {
            get => data.MakeDate;
            set => data.MakeDate = value;
        }

        public int User
        {
            get => data.Users_ID;
            set => data.Users_ID = value;
        }

        public string UserName
        {
            get => userName;
            set 
            { 
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
