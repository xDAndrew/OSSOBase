using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using Client.Services;
using Client.Services.Interfaces;
using Excel = Microsoft.Office.Interop.Excel;

namespace Client.ViewModel
{
    class VM_EditWindow : INotifyPropertyChanged
    {
        private View.EditWindow WinLink;
        private readonly IPrintService _printService;

        private bool changed = false;
        public bool Changed => CurrentObject.Changed || CurrentPKP.Changed || changed || CurrentCard.Changed;

        private Model.M_Object currentObject;
        public Model.M_Object CurrentObject => currentObject;

        private Model.M_PKP currentPKP;
        public Model.M_PKP CurrentPKP => currentPKP;

        private Model.EquipmentModel.Equipment currentEquipment;
        public Model.EquipmentModel.Equipment CurrentEquipment => currentEquipment;

        private Model.M_Card currentCard;
        public Model.M_Card CurrentCard => currentCard;

        //Хз что за метод, но пусть будет
        private Model.EF.Users currentUser;
        public Model.EF.Users CurrentUser => currentUser;

        //Свойство для отображения модулей в подсказке
        public string ToolTipView
        {
            get 
            {
                StringBuilder str = new StringBuilder("ПКП - 0,3 у.у. \n");
                for (int i = 0; i < currentPKP.Moduls.Items.Count; i++)
                {
                    var item = currentPKP.Moduls.Items[i];
                    str.Append(string.Format("{0}(x{1}) - {2} у.у.", item.Name, item.Count, item.UUSumm) + "\n");
                }
                str.Append(string.Format("Итого: {0} у.у.", currentPKP.Moduls.FullSumm));
                return str.ToString();
            }
        }

        //Свойство отображает дату создания карточки в StatusBar
        public string Date
        {
            get { return currentCard.MakeDate.ToString("dd MMMMMMMMMM yyyy"); }
        }

        //Свойство отображает имя пользователя, который создал объекта в StatusBar
        public string Maker
        {
            get { return currentCard.UserName; }
        }

        //Конструктор VM_EditWindow
        public VM_EditWindow(View.EditWindow HNDL = null, int? CurrentCardId = null)
        {
            _printService = new PrintService();
            WinLink = HNDL;

            if (CurrentCardId == null)
            {
                currentCard = new Model.M_Card();
                currentObject = new Model.M_Object();
                currentPKP = new Model.M_PKP();
                currentEquipment = new Model.EquipmentModel.Equipment();

                currentUser = Model.EF.EntityInstance.DBContext.UsersSet.First(p => p.Users_ID == Model.EF.EntityInstance.UserID);
                CountUU();
            }
            else
            {
                currentCard = new Model.M_Card(Model.EF.EntityInstance.DBContext.CardsSet.First(p => p.Cards_ID == CurrentCardId.Value));
                currentObject = new Model.M_Object(currentCard.Id);
                currentPKP = new Model.M_PKP(currentCard.Id);
                currentEquipment = new Model.EquipmentModel.Equipment(currentCard.Id);

                currentEquipment.setUpdateMethod(CountUU);

                OnPropertyChanged("LimbsCount");
                OnPropertyChanged("CurrentObject");
                OnPropertyChanged("CurrentPKP");
                OnPropertyChanged("CurrentEquipment");
                CountUU();
            }

            if (WinLink != null)
            {
                WinLink.Closing += (o, e) =>
                {
                    if (Changed)
                    {
                        if (MessageBox.Show("Сохранить внесенные изменения?", "Сохранить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            SaveChange.Execute(new object());
                        }
                    }
                };
            }
        }

        //Функция, которая пересчитывает УУ
        public void CountUU()
        {
            for (int i = 0; i < 15; i++)
            {
                currentEquipment.Results[i] = 0;
            }
            currentEquipment.Results.Summ = 0;

            foreach (var item in currentEquipment.Branches)
            {
                item.Summ = 0.0;
                for (int i = 0; i < 15; i++)
                {
                    if (i < currentEquipment.Models.Items.Count)
                    {
                        item.Summ += item[i] * currentEquipment.Models.Items[i].UU;
                    }
                    else
                    {
                        item[i] = 0;
                    }
                    currentEquipment.Results[i] += item[i];
                }

                int iterator = 0;
                int n = 0;
                while (iterator < item.SummTSO)
                {
                    if (iterator % 10 == 0)
                    {
                        n++;
                    }
                    iterator++;
                }

                if (item.SummTSO > 0)
                {
                    double temp = n * 0.05;
                    item.Summ += temp;
                    if (item.Number == 1 && item.Summ >= 0.05) item.Summ -= 0.05;
                }

                currentEquipment.Results.Summ += item.Summ;
            }

            currentEquipment.Results.Summ += currentPKP.Moduls.FullSumm;
            OnPropertyChanged("CurrentEquipment");
            if (currentCard.UU != currentEquipment.Results.Summ) changed = true;
            currentCard.UU = currentEquipment.Results.Summ;
            
        }

        //Команда для кнопки "Печать"
        private Command print;
        public Command Print
        {
            get
            {
                return print ?? (print = new Command(obj =>
                {
                    _printService.PrintCardDocument(CurrentCard, CurrentPKP, CurrentEquipment);
                    
                    /*
                    var excelApp = new Excel.Application {Visible = false, DisplayAlerts = false};
                    byte type = 0;

                    try
                    {
                        // Выбираем документ
                        var str = Environment.CurrentDirectory;
                        Excel.Workbook workBook;
                        if (CurrentEquipment.LimbsCount <= 8)
                        {
                            workBook = excelApp.Workbooks.Open(str + @"\Data\8.xls");
                        }
                        else
                        {
                            if (CurrentEquipment.LimbsCount <= 16)
                            {
                                workBook = excelApp.Workbooks.Open(str + @"\Data\16.xls");
                                type = 1;
                            }
                            else
                            {
                                if (CurrentEquipment.LimbsCount <= 32)
                                {
                                    workBook = excelApp.Workbooks.Open(str + @"\Data\32.xls");
                                    type = 2;
                                }
                                else
                                {
                                    if (CurrentEquipment.LimbsCount <= 64)
                                    {
                                        workBook = excelApp.Workbooks.Open(str + @"\Data\64.xls");
                                        type = 3;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Печать для " + CurrentEquipment.LimbsCount + " шлейфов - невозможна!", "Ошибка");
                                        excelApp.Quit();
                                        return;
                                    }
                                }
                            }
                        }

                        var workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
                        Excel.Range rng = workSheet.Range["A3"];
                        rng.Value = CurrentCard.Owner + " " + CurrentCard.ObjectView + ", " + CurrentCard.Address;

                        rng = workSheet.Range["B9"];
                        rng.Value = CurrentPkp.PKPIndex.Name;

                        rng = workSheet.Range["D9"];
                        rng.Value = CurrentPkp.Serial;

                        rng = workSheet.Range["G9"];
                        rng.Value = CurrentPkp.Password;

                        rng = workSheet.Range["J9"];
                        rng.Value = CurrentPkp.Phone;

                        rng = workSheet.Range["X9"];
                        rng.Value = CurrentPkp.SelectedDate.ToLongDateString();

                        var sumbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        for (var i = 0; i < CurrentPkp.Moduls.Items.Count; i++)
                        {
                            rng = workSheet.Range[sumbols[i + 1].ToString() + "12"];
                            rng.Value = CurrentPkp.Moduls.Items[i].Name;
                            rng = workSheet.Range[sumbols[i + 1].ToString() + "15"];
                            rng.Value = CurrentPkp.Moduls.Items[i].Count;
                        }
                        rng = workSheet.Range["AC15"];
                        rng.Value = CurrentPkp.Moduls.FullSumm;

                        for (var i = 0; i < CurrentEquipment.Models.Items.Count; i++)
                        {
                            if (i + 12 < 26)
                            {
                                rng = workSheet.Range[sumbols[i + 12].ToString() + "12"];
                            }
                            else
                            {
                                rng = workSheet.Range["AA12"];
                            }
                            rng.Value = CurrentEquipment.Models.Items[i].Name;
                        }

                        for (var i = 0; i < CurrentEquipment.Branches.Count; i++)
                        {
                            rng = workSheet.Range["A" + (i + 16).ToString()];
                            rng.Value = CurrentEquipment.Branches[i].Number;

                            rng = workSheet.Range["B" + (i + 16).ToString()];
                            rng.Value = CurrentEquipment.Branches[i].Name;

                            for (var j = 0; j < CurrentEquipment.Models.Items.Count; j++)
                            {
                                if (j + 12 < 26)
                                {
                                    rng = workSheet.Range[sumbols[j + 12].ToString() + (i + 16).ToString()];
                                }
                                else
                                {
                                    rng = workSheet.Range["AA" + (i + 16).ToString()];
                                }

                                rng.Value = CurrentEquipment.Branches[i][j];
                            }

                            rng = workSheet.Range["AC" + (i + 16).ToString()];
                            rng.Value = CurrentEquipment.Branches[i].Summ;
                        }

                        // Save date and user name
                        switch (type)
                        {
                            case 0:
                                rng = workSheet.Range["B28"];
                                rng.Value = CurrentCard.UserName;
                                rng = workSheet.Range["B30"];
                                rng.Value = CurrentCard.MakeDate.ToLongDateString();
                                break;

                            case 1:
                                rng = workSheet.Range["B36"];
                                rng.Value = CurrentCard.UserName;
                                rng = workSheet.Range["B38"];
                                rng.Value = CurrentCard.MakeDate.ToLongDateString();
                                break;

                            case 2:
                                rng = workSheet.Range["B52"];
                                rng.Value = CurrentCard.UserName;
                                rng = workSheet.Range["B54"];
                                rng.Value = CurrentCard.MakeDate.ToLongDateString();
                                break;

                            case 3:
                                rng = workSheet.Range["B84"];
                                rng.Value = CurrentCard.UserName;
                                rng = workSheet.Range["B86"];
                                rng.Value = CurrentCard.MakeDate.ToLongDateString();
                                break;
                        }

                        workSheet.PrintOut();
                    }
                    finally
                    {
                        excelApp.Quit();
                    }*/
                }));
            }
        }

        //Функция для пересчёт всех объектов
        public void UpdateUU()
        {
            currentCard.Save();
        }

        //Команда для сохранения данных при закрытии карточки
        private Command saveChange;
        public Command SaveChange
        {
            get
            {
                return saveChange ?? (saveChange = new Command(obj =>
                {
                    if (CurrentObject.StreetIndex != null && CurrentPKP.PKPIndex != null && Changed)
                    {
                        currentCard.Owner = currentObject.Owner;
                        currentCard.ObjectView = currentObject.Name;

                        var str = "";
                        str += Model.EF.EntityInstance.GetStreetType(currentObject.StreetIndex.Type) + " ";
                        str += Model.EF.EntityInstance.DBContext.StreetsSet.First(p => p.Streets_ID == currentObject.StreetIndex.Streets_ID).Name + ", ";
                        str += currentObject.Home;
                        if (currentObject.Corp != "") str += "\\" + currentObject.Corp;
                        if (currentObject.Room != "") str += "-" + currentObject.Room;
                        currentCard.Address = str;

                        currentCard.MakeDate = DateTime.Now;
                        currentCard.User = Model.EF.EntityInstance.UserID;
                        currentCard.PKP = Model.EF.EntityInstance.DBContext.UsersSet.First(p => p.Users_ID == currentCard.User).Name;

                        currentCard.Save();
                        currentObject.Save(currentCard.Id);
                        currentPKP.Save(currentCard.Id);
                        currentEquipment.Save(currentCard.Id);

                        try
                        {
                            var temp = BitConverter.GetBytes(1);
                            Model.EF.EntityInstance.socket.Send(temp);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }));
            }
        }

        //Команда для кнопки "Сохранить"
        private Command menuItemSaveChange;
        public Command MenuSaveChange
        {
            get
            {
                return menuItemSaveChange ?? (menuItemSaveChange = new Command(obj =>
                {
                    if (CurrentObject.StreetIndex != null && CurrentPKP.PKPIndex != null && Changed && SaveChange != null)
                    {
                        SaveChange.Execute(new object());
                        MessageBox.Show(WinLink, "Данные сохранены!", "Сохранить");
                        CurrentObject.Changed = false;
                        CurrentPKP.Changed = false;
                        changed = false;
                    }
                }));
            }
        }

        //Команда для кнопки "Выход"
        private Command menuCloseWindow;
        public Command MenuCloseWindow
        {
            get
            {
                return menuCloseWindow ?? (menuCloseWindow = new Command(obj =>
                {
                    if (WinLink != null) WinLink.Close();
                }));
            }
        }

        //Команда для кнопки "Оборудование" в конткстном меню таблицы с датчиками
        private Command openTSOEdit;
        public Command OpenTSOEdit
        {
            get
            {
                return openTSOEdit ?? (openTSOEdit = new Command(obj =>
                {
                    //Перед открытием окна
                    var tempTSO = new Model.EquipmentModel.TSO_Collection();
                    var rebuildTSO = new Model.EquipmentModel.TSO_Collection();
                    foreach (var item in currentEquipment.Models.Items)
                    {
                        tempTSO.Items.Add(item);
                        rebuildTSO.Items.Add(item);
                    }

                    var branchList = new List<int[]>();
                    foreach (var item in currentEquipment.Branches)
                    {
                        var arr = new int[15];
                        for (int i = 0; i < 15; i++)
                        {
                            arr[i] = item[i];
                        }
                        branchList.Add(arr);
                    }

                    var wTemp = new View.TSOEditWindow();
                    var cTemp = new ViewModel.VM_TSOEditWindow(wTemp, tempTSO);
                    wTemp.Owner = WinLink;
                    wTemp.DataContext = cTemp;
                    wTemp.ShowDialog();

                    //После закрытия окна создаем новый список ТСО
                    currentEquipment.Clear();
                    currentEquipment.Models.Items.Clear();
                    foreach (var item in tempTSO.Items)
                    {
                        currentEquipment.Models.Items.Add(item);
                    }

                    for (int i = 0; i < currentEquipment.Models.Items.Count; i++)
                    {
                        for (int j = 0; j < rebuildTSO.Items.Count; j++)
                        {
                            if (currentEquipment.Models.Items[i].Id == rebuildTSO.Items[j].Id)
                            {
                                for (int k = 0; k < currentEquipment.Branches.Count; k++)
                                {
                                    currentEquipment.Branches[k][i] = branchList[k][j];
                                }
                                break;
                            }
                        }
                    }

                    CountUU();
                    OnPropertyChanged("Branches");
                }));
            }
        }

        //Команда для кнопки "Список модулей"
        private ViewModel.Command openTSOList;
        public ViewModel.Command OpenTSOList
        {
            get
            {
                return openTSOList ?? (openTSOList = new ViewModel.Command(obj =>
                {
                    var wTemp = new View.TSOWindow();
                    var cTemp = new ViewModel.VM_TSOWindow(currentPKP.Moduls, wTemp);
                    wTemp.Owner = WinLink;
                    wTemp.DataContext = cTemp;
                    wTemp.ShowDialog();
                    CountUU();
                    OnPropertyChanged("ToolTipView");
                }));
            }
        }

        //Команда для добавления шлейфа "+"
        private Command addLimb;
        public Command AddLimb
        {
            get
            {
                return addLimb ?? (addLimb = new Command(obj =>
                {
                    changed = true;
                    byte t = (byte)currentEquipment.LimbsCount;
                    t++;
                    currentEquipment.Branches.Add(new Model.EquipmentModel.Branch(t));
                    currentEquipment.setUpdateMethod(CountUU);
                    CountUU();
                    OnPropertyChanged("LimbsCount");
                }));
            }
        }

        //Команда для удаления шлейфа "-"
        private Command delLimb;
        public Command DelLimb
        {
            get
            {
                return delLimb ?? (delLimb = new Command(obj =>
                {
                    changed = true;
                    if (currentEquipment.Branches.Count > 0)
                    {
                        currentEquipment.Branches.RemoveAt(currentEquipment.Branches.Count - 1);
                        CountUU();
                    }
                    OnPropertyChanged("LimbsCount");
                }));
            }
        }

        //Свойство для обновления UI при изменении данных
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}