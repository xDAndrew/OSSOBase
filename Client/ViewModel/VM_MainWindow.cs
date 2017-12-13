﻿using System;
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
            set { myCardsState = value; }
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

            UpdateTimer.Interval = 5000;
            UpdateTimer.Tick += ((o, e) => { UpdateGrid(); });
            UpdateTimer.Start();
            //UpdateGrid();
        }

        public void UpdateGrid()
        {
            if (userName == null)
            {
                var tempUser = Model.EF.EntityInstance.DBContext.UsersSet.AsNoTracking().First(p => p.Users_ID == Model.EF.EntityInstance.UserID);
                userName = tempUser.Place + " " + tempUser.Name;
                OnPropertyChanged("CurrentUser");
            }

            int index = itemIndex;
            Cards.Clear();
            var temp = Model.EF.EntityInstance.DBContext.CardsSet.AsNoTracking().Where(p => (myCardsState ? p.Users_ID == Model.EF.EntityInstance.UserID : true)).ToList();

            foreach (var item in temp)
            {
                Cards.Add(new Model.M_Card(item));
            }

            //var temp = Model.EF.EntityInstance.DBContext.CardsSet.AsNoTracking().Where(p => true).ToList();

            //foreach (var item in temp)
            //{
            //    Cards.Add(new Model.M_Card(item));
            //}

            ItemIndex = index;
            WinLink.MG.Focus();

            OnPropertyChanged("CardsCount");
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

        private double updateUU()
        {
            return 0.0;
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
        public Command CloseApp { get => closeApp ?? (closeApp = new Command(obj => { WinLink.Close(); })); }

        private Command cardsUpdate;
        public Command CardsUpdate { get => cardsUpdate ?? (cardsUpdate = new Command(obj => {
            UpdateTimer.Stop();
            foreach (var item in cardsCollection)
            {
                var VM = new ViewModel.VM_EditWindow(new View.EditWindow(), item.Id);
                VM.UpdateUU();
                OnPropertyChanged("CardsCollection");
            }
            UpdateTimer.Start();
        })); }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}