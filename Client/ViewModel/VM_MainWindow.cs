using Client.Additional;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using Client.Application.EF;

namespace Client.ViewModel
{
    class VM_MainWindow : INotifyPropertyChanged
    {
        MainWindow WinLink = null;
        System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();

        //Свойство SearchContent хранит данные введеные в строку "Поиск"
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

        //Свойство MyCardsState состояние кнопки "Мои объекты"
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

        //Свойство CurrentUser обспечивает данные для отображения информации о текущем пользователе в StatusBar
        private string userName;
        public string CurrentUser
        {
            get { return userName; }
        }

        //Свойство CardsCount обеспечивает данные для отображения информации о кол-ве найденых объектов в StatusBar
        public int CardsCount
        {
            get { return Cards.Count; }
        }

        //Свойство ItemIndex хранит № выделенной строки в таблице
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

        //Свойство Cards сдержит отображаемые в главной таблице элементы
        ObservableCollection<Model.M_Card> cardsCollection = new ObservableCollection<Model.M_Card>();
        public ObservableCollection<Model.M_Card> Cards
        {
            get { return cardsCollection; }
        }

        //Свойство SelectedItem хранить ссылку на выделенный элемент
        private Model.M_Card selectedItem = null;
        public Model.M_Card  SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        //Конструктор класса VM_MainWindow
        public VM_MainWindow(MainWindow MW)
        {
            WinLink = MW;

            var tempUser = EntityInstance.DBContext.UsersSet.AsNoTracking().First(p => p.Users_ID == EntityInstance.UserID);
            userName = tempUser.Place + " " + tempUser.Name;
            OnPropertyChanged("CurrentUser");

            updateTimer.Interval = 10;
            updateTimer.Tick += ((o, e) => { UpdateGrid(); });
            updateTimer.Start();

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse((string)ConfigurationManager.AppSettings["ServerHost"]), 8005);
            EntityInstance.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                EntityInstance.socket.Connect(ipPoint);
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
                            EntityInstance.socket.Send(temp);

                            do
                            {
                                bytes = EntityInstance.socket.Receive(data, data.Length, 0);
                                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                            }
                            while (EntityInstance.socket.Available > 0);

                            EntityInstance.ServerUpdate = DateTime.Parse(builder.ToString());
                            Thread.Sleep(100);
                        }
                        catch 
                        {
                            Thread SReading = new Thread(() =>
                            {
                                while (true)
                                {
                                    EntityInstance.ServerUpdate = DateTime.Now;
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
                        EntityInstance.ServerUpdate = DateTime.Now;
                        Thread.Sleep(4000);
                    }
                });
                SocketReading.IsBackground = true;
                SocketReading.Start();
            }
        }

        //Обновляет содержимое главной таблицы
        public void UpdateGrid(bool bStart = false)
        {
            if (EntityInstance.ServerUpdate.CompareTo(EntityInstance.LocalUpdate) > 0 || bStart)
            {
                bool focus = WinLink.MG.IsKeyboardFocusWithin;
                int index = itemIndex;

                Cards.Clear();
                var temp = EntityInstance.DBContext.CardsSet.AsNoTracking().Where(p => SearchContent == "" ? true : p.AddressView.Contains(searchContent)).
                    Where(p => (myCardsState ? p.Users_ID == EntityInstance.UserID : true)).ToList();
                foreach (var item in temp)
                {
                    Cards.Add(new Model.M_Card(item));
                }

                ItemIndex = index;
                if (focus == true) WinLink.MG.Focus();

                OnPropertyChanged("CardsCount");
                OnPropertyChanged("Cards");
                EntityInstance.LocalUpdate = DateTime.Now;
            }
        }

        //Комманда для добавления нового объекта (Открывает соответствующее окно)
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

        //Команда для редактирования объекта (Открывает соотвествуещее окно)
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

        //Команда для пункта меню Дополнительно -> пересчёт УУ
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

                        var temp = EntityInstance.DBContext.CardsSet.AsNoTracking().Where(p => true).ToList();
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

        //Комманда для кнопки Меню -> Выход
        private Command closeApp;
        public Command CloseApp
        {
            get { return closeApp ?? (closeApp = new Command(obj => { WinLink.Close(); })); }
        }

        //Свойство для обновления UI при изменении данных
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}