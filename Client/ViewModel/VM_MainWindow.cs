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
using System.Configuration;

namespace Client.ViewModel
{
    class VM_MainWindow : INotifyPropertyChanged
    {
        MainWindow WinLink = null;
        System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();

        private string searchContent = "";
        public string SearchContent
        {
            get
            {
                return searchContent;
            }
            set
            {
                searchContent = value;
                UpdateGrid(true);
                OnPropertyChanged("Cards");
            }
        }

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
                UpdateGrid(true);
                OnPropertyChanged("Cards");
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

            var tempUser = Model.EF.EntityInstance.DBContext.UsersSet.AsNoTracking().First(p => p.Users_ID == Model.EF.EntityInstance.UserID);
            userName = tempUser.Place + " " + tempUser.Name;
            OnPropertyChanged("CurrentUser");

            updateTimer.Interval = 10;
            updateTimer.Tick += ((o, e) => { UpdateGrid(); });
            updateTimer.Start();

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse((string)ConfigurationManager.AppSettings["ServerHost"]), 8005);
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

                            Model.EF.EntityInstance.ServerUpdate = DateTime.Parse(builder.ToString());
                            Thread.Sleep(100);
                        }
                        catch 
                        {
                            Thread SReading = new Thread(() =>
                            {
                                while (true)
                                {
                                    Model.EF.EntityInstance.ServerUpdate = DateTime.Now;
                                    Thread.Sleep(4000);
                                }
                            });
                            SReading.IsBackground = true;
                            SReading.Start();
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
                        Model.EF.EntityInstance.ServerUpdate = DateTime.Now;
                        Thread.Sleep(4000);
                    }
                });
                SocketReading.IsBackground = true;
                SocketReading.Start();
            }
        }

        public void UpdateGrid(bool bStart = false)
        {
            if (Model.EF.EntityInstance.ServerUpdate.CompareTo(Model.EF.EntityInstance.LocalUpdate) > 0 || bStart)
            {
                bool focus = WinLink.MG.IsKeyboardFocusWithin;
                int index = itemIndex;

                Cards.Clear();
                var temp = Model.EF.EntityInstance.DBContext.CardsSet.AsNoTracking().Where(p => SearchContent == "" ? true : p.AddressView.Contains(searchContent)).
                    Where(p => (myCardsState ? p.Users_ID == Model.EF.EntityInstance.UserID : true)).ToList();
                foreach (var item in temp)
                {
                    Cards.Add(new Model.M_Card(item));
                }

                ItemIndex = index;
                if (focus == true) WinLink.MG.Focus();

                OnPropertyChanged("CardsCount");
                OnPropertyChanged("Cards");
                Model.EF.EntityInstance.LocalUpdate = DateTime.Now;
            }
        }

        private Command addCard;
        public Command AddCard
        {
            get
            {
                return addCard ?? (addCard = new Command(obj =>
                {
                    updateTimer.Stop();
                    var eForm = new View.EditWindow();
                    var eFormVM = new ViewModel.VM_EditWindow(eForm);
                    eForm.Owner = WinLink;
                    eForm.DataContext = eFormVM;
                    eForm.ShowDialog();
                    UpdateGrid();
                    updateTimer.Start();
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
                        updateTimer.Stop();
                        var eForm = new View.EditWindow();
                        var eFormVM = new ViewModel.VM_EditWindow(eForm, SelectedItem.Id);
                        eForm.Owner = WinLink;
                        eForm.DataContext = eFormVM;
                        eForm.ShowDialog();
                        UpdateGrid();
                        updateTimer.Start();
                    }
                })); 
            }
        }

        private Command cardsUpdate;
        public Command CardsUpdate 
        {
            get
            {
                return cardsUpdate ?? (cardsUpdate = new Command(obj =>
                {
                    if (System.Windows.MessageBox.Show("Убедитесь, что другие пользователи не вносят изменения\nПриложение может ненадолго зависнуть",
                        "Сообщение",
                        MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        updateTimer.Stop();

                        var temp = Model.EF.EntityInstance.DBContext.CardsSet.AsNoTracking().Where(p => true).ToList();
                        var cards = new List<Model.M_Card>();
                        foreach (var item in temp)
                        {
                            cards.Add(new Model.M_Card(item));
                        }

                        for (int i = 0; i < cards.Count; i++)
                        {
                            var VM = new VM_EditWindow(null, cards[i].Id);
                            VM.UpdateUU();
                            OnPropertyChanged("CardsCollection");
                        }
                        System.Windows.MessageBox.Show("Пересчёт выполнен", "Сообщение");
                        updateTimer.Start();
                    }
                }));
            }
        }

        private Command closeApp;
        public Command CloseApp
        {
            get { return closeApp ?? (closeApp = new Command(obj => { WinLink.Close(); })); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}