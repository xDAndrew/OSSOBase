using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Configuration;

namespace Client.ViewModel
{
    class VM_MainWindow : INotifyPropertyChanged
    {
        MainWindow WinLink;
        private readonly System.Windows.Forms.Timer _updateTimer = new System.Windows.Forms.Timer();

        //Свойство SearchContent хранит данные введеные в строку "Поиск"

        private string _contractSearchContent = "";
        private string _ownerSearchContent = "";
        private string _objectSearchContent = "";
        private string _addressSearchContent = "";

        public string ContractSearchContent
        {
            get => _contractSearchContent;
            set
            {
                _contractSearchContent = value;
                UpdateGrid(true);
            }
        }

        public string OwnerSearchContent
        {
            get => _ownerSearchContent;
            set
            {
                _ownerSearchContent = value;
                UpdateGrid(true);
            }
        }

        public string ObjectSearchContent
        {
            get => _objectSearchContent;
            set
            {
                _objectSearchContent = value;
                UpdateGrid(true);
            }
        }

        public string AddressSearchContent
        {
            get => _addressSearchContent;
            set
            {
                _addressSearchContent = value;
                UpdateGrid(true);
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
        public string CurrentUser { get; }

        //Свойство CardsCount обеспечивает данные для отображения информации о кол-ве найденых объектов в StatusBar
        public int CardsCount => Cards.Count;

        //Свойство ItemIndex хранит № выделенной строки в таблице
        private int itemIndex;
        public int ItemIndex
        {
            get => itemIndex;
            set 
            { 
                itemIndex = value;
                OnPropertyChanged("ItemIndex");   
            }
        }

        //Свойство Cards сдержит отображаемые в главной таблице элементы
        ObservableCollection<Model.M_Card> cardsCollection = new ObservableCollection<Model.M_Card>();
        public ObservableCollection<Model.M_Card> Cards => cardsCollection;

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

            var tempUser = Model.EF.EntityInstance.DBContext.UsersSet.AsNoTracking().First(p => p.Users_ID == Model.EF.EntityInstance.UserID);
            CurrentUser = tempUser.Place + " " + tempUser.Name;
            OnPropertyChanged($"CurrentUser");

            _updateTimer.Interval = 10;
            _updateTimer.Tick += ((o, e) => { UpdateGrid(); });
            _updateTimer.Start();

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
                            var SReading = new Thread(() =>
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

        //Обновляет содержимое главной таблицы
        public void UpdateGrid(bool bStart = false)
        {
            if (Model.EF.EntityInstance.ServerUpdate.CompareTo(Model.EF.EntityInstance.LocalUpdate) > 0 || bStart)
            {
                var focus = WinLink.MG.IsKeyboardFocusWithin;
                var index = itemIndex;

                Cards.Clear();
                var temp = Model.EF.EntityInstance.DBContext.CardsSet.AsNoTracking()
                    .Where(p => ContractSearchContent == "" || p.Contract.Contains(_contractSearchContent))
                    .Where(p => OwnerSearchContent == "" || p.OwnerView.Contains(_ownerSearchContent))
                    .Where(p => ObjectSearchContent == "" || p.ObjectView.Contains(_objectSearchContent))
                    .Where(p => AddressSearchContent == "" || p.AddressView.Contains(_addressSearchContent))
                    .Where(p => !myCardsState || p.Users_ID == Model.EF.EntityInstance.UserID).ToList();

                foreach (var item in temp)
                {
                    Cards.Add(new Model.M_Card(item));
                }

                ItemIndex = index;
                if (focus) WinLink.MG.Focus();

                OnPropertyChanged("CardsCount");
                OnPropertyChanged("Cards");
                Model.EF.EntityInstance.LocalUpdate = DateTime.Now;
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
                    _updateTimer.Stop();
                    var eForm = new View.EditWindow();
                    var eFormVM = new VM_EditWindow(eForm);
                    eForm.Owner = WinLink;
                    eForm.DataContext = eFormVM;
                    eForm.ShowDialog();
                    UpdateGrid();
                    _updateTimer.Start();
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
                        _updateTimer.Stop();
                        var eForm = new View.EditWindow();
                        var eFormVM = new VM_EditWindow(eForm, SelectedItem.Id);
                        eForm.Owner = WinLink;
                        eForm.DataContext = eFormVM;
                        eForm.ShowDialog();
                        UpdateGrid();
                        _updateTimer.Start();
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
                    if (MessageBox.Show("Убедитесь, что другие пользователи не вносят изменения\nПриложение может ненадолго зависнуть",
                        "Сообщение",
                        MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        _updateTimer.Stop();

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
                        MessageBox.Show("Пересчёт выполнен", "Сообщение");
                        _updateTimer.Start();
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