using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;
using System.Net;
using System.Net.Sockets;

namespace Client.ViewModel
{
    class VM_MainWindow : INotifyPropertyChanged
    {
        MainWindow WinLink = null;
        System.Windows.Forms.Timer UpdateTimer = new System.Windows.Forms.Timer();

        private string userName;
        public string CurrentUser
        {
            get { return userName; }
        }

        private bool myCardsState;
        public bool MyCardsState
        {
            set 
            { 
                myCardsState = value;
                Model.EF.EntityInstance.ServerUpdate = DateTime.Now;
            }
            get { return myCardsState; }
        }

        public int CardsCount
        {
            get { return Cards.Count; }
        }

        private int itemIndex;
        public int ItemIndex
        {
            get { return itemIndex; }
            set 
            { 
                itemIndex = value;
                OnPropertyChanged("ItemIndex");   
            }
        }

        ObservableCollection<Model.M_Card> cardsCollection = new ObservableCollection<Model.M_Card>();
        public ObservableCollection<Model.M_Card> Cards
        {
            get { return cardsCollection; }
        }

        private Model.M_Card selectedItem = null;
        public Model.M_Card  SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        public VM_MainWindow(MainWindow MW)
        {
            WinLink = MW;

            UpdateTimer.Interval = 10;
            UpdateTimer.Tick += ((o, e) => { UpdateGrid(); });
            UpdateTimer.Start();

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8005);
            Model.EF.EntityInstance.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Model.EF.EntityInstance.socket.Connect(ipPoint);
                Thread SocketReading = new Thread(() =>
                {
                    while (true)
                    {
                        var data = new byte[256];
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;

                        try
                        {
                            byte[] temp = BitConverter.GetBytes(0);
                            Model.EF.EntityInstance.socket.Send(temp);

                            do
                            {
                                bytes = Model.EF.EntityInstance.socket.Receive(data, data.Length, 0);
                                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                            }
                            while (Model.EF.EntityInstance.socket.Available > 0);

                            lock (Model.EF.EntityInstance.lokedKey)
                            {
                                Model.EF.EntityInstance.ServerUpdate = DateTime.Parse(builder.ToString());
                            }
                            Thread.Sleep(100);
                        }
                        catch 
                        {
                            //System.Windows.MessageBox.Show("Соединение с сервером было потеряно...");

                            Thread SReading = new Thread(() =>
                            {
                                while (true)
                                {
                                    lock (Model.EF.EntityInstance.lokedKey)
                                    {
                                        Model.EF.EntityInstance.ServerUpdate = DateTime.Now;
                                    }
                                    Thread.Sleep(3000);
                                }
                            });
                            SReading.IsBackground = true;
                            SReading.Start();
                            break;
                        };
                    }
                });
                SocketReading.IsBackground = true;
                SocketReading.Start();
            }
            catch
            {
                System.Windows.MessageBox.Show("Подключение к серверу не удалось!\nПрограмма работает в автономном режиме...");

                Thread SocketReading = new Thread(() =>
                {
                    while (true)
                    {
                        lock (Model.EF.EntityInstance.lokedKey)
                        {
                            Model.EF.EntityInstance.ServerUpdate = DateTime.Now;
                        }
                        Thread.Sleep(3000);
                    }
                });
                SocketReading.IsBackground = true;
                SocketReading.Start();
            }
        }

        public void UpdateGrid()
        {
            if (userName == null)
            {
                var tempUser = Model.EF.EntityInstance.DBContext.UsersSet.AsNoTracking().First(p => p.Users_ID == Model.EF.EntityInstance.UserID);
                userName = tempUser.Place + " " + tempUser.Name;
                OnPropertyChanged("CurrentUser");
            }

            if (Model.EF.EntityInstance.ServerUpdate.CompareTo(Model.EF.EntityInstance.LocalUpdate) > 0)
            {
                int index = itemIndex;
                Cards.Clear();
                var temp = Model.EF.EntityInstance.DBContext.CardsSet.AsNoTracking().Where(p => (myCardsState ? p.Users_ID == Model.EF.EntityInstance.UserID : true)).ToList();
                foreach (var item in temp)
                {
                    Cards.Add(new Model.M_Card(item));
                }
                ItemIndex = index;
                WinLink.MG.Focus();
                OnPropertyChanged("CardsCount");
                lock (Model.EF.EntityInstance.lokedKey)
                {
                    Model.EF.EntityInstance.LocalUpdate = DateTime.Now;
                }
                //System.Windows.MessageBox.Show("Было обнавлено! " + Model.EF.EntityInstance.ServerUpdate + " " + Model.EF.EntityInstance.LocalUpdate);
            }
        }

        private Command addCard;
        public Command AddCard
        {
            get
            {
                return addCard ?? (addCard = new Command(obj =>
                {
                    UpdateTimer.Stop();
                    var eForm = new View.EditWindow();
                    var eFormVM = new ViewModel.VM_EditWindow(eForm);
                    eForm.Owner = WinLink;
                    eForm.DataContext = eFormVM;
                    eForm.ShowDialog();
                    UpdateTimer.Start();
                    UpdateGrid();
                }));
            }
        }

        private Command editCard;
        public Command EditCard
        {
            get
            {
                return editCard ?? (editCard = new Command(obj =>
                {
                    if (selectedItem != null)
                    {
                        UpdateTimer.Stop();
                        var eForm = new View.EditWindow();
                        var eFormVM = new ViewModel.VM_EditWindow(eForm, SelectedItem.Id);
                        //System.Windows.MessageBox.Show(eFormVM.CurrentEquipment.Results.Summ.ToString());
                        eForm.Owner = WinLink;
                        eForm.DataContext = eFormVM;
                        eForm.ShowDialog();
                        UpdateTimer.Start();
                        UpdateGrid();
                    }
                })); 
            }
        }

        private Command closeApp;
        public Command CloseApp
        { 
            get 
            { 
                return closeApp ?? (closeApp = new Command(obj => { WinLink.Close(); })); 
            }
        }

        private Command cardsUpdate;
        public Command CardsUpdate 
        {
            get
            {
                return cardsUpdate ?? (cardsUpdate = new Command(obj =>
                {
                    UpdateTimer.Stop();
                    foreach (var item in cardsCollection)
                    {
                        var VM = new ViewModel.VM_EditWindow(new View.EditWindow(), item.Id);
                        VM.UpdateUU();
                        OnPropertyChanged("CardsCollection");
                    }
                    UpdateTimer.Start();
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}