using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    class VM_MainWindow
    {
        MainWindow WinHANDLE = null;

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
            WinHANDLE = MW;
            LoadCards();
        }

        private void LoadCards()
        {
            Cards.Clear();
            var temp = Model.EF.EntityInstance.DBContext.CardsSet.Where(p => true).ToList();
            foreach (var item in temp)
            {
                Cards.Add(new Model.M_Card(item));
            }
        }

        #region Command
        private Command addCard;
        public Command AddCard
        {
            get
            {
                return addCard ?? (addCard = new Command(obj =>
                {
                    var wTemp = new View.LoginWindow();
                    var cTemp = new ViewModel.VM_LoginWindow(wTemp);
                    wTemp.Owner = WinHANDLE;
                    wTemp.DataContext = cTemp;
                    wTemp.ShowDialog();

                    var eForm = new View.EditWindow();
                    var eFormVM = new ViewModel.VM_EditWindow(eForm);
                    eForm.Owner = WinHANDLE;
                    eForm.DataContext = eFormVM;
                    eForm.ShowDialog();
                    LoadCards();
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
                        var eForm = new View.EditWindow();
                        var eFormVM = new ViewModel.VM_EditWindow(eForm, SelectedItem.Id);
                        eForm.Owner = WinHANDLE;
                        eForm.DataContext = eFormVM;
                        eForm.ShowDialog();
                        LoadCards();
                    }
                })); 
            }
        }
        #endregion
    }
}