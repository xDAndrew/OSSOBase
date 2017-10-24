﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

namespace Client.ViewModel
{
    class VM_EditWindow : INotifyPropertyChanged
    {
        private View.EditWindow WinLink;

        private bool changed = false;
        public bool Changed
        {
            get { return CurrentObject.Changed || CurrentPKP.Changed || changed; }
        }

        private Model.M_Object currentObject;
        public Model.M_Object CurrentObject
        {
            get
            {
                return currentObject;
            }
        }

        private Model.M_PKP currentPKP;
        public Model.M_PKP CurrentPKP
        {
            get
            {
                return currentPKP;
            }
        }

        private Model.EquipmentModel.Equipment currentEquipment;
        public Model.EquipmentModel.Equipment CurrentEquipment
        {
            get
            {
                return currentEquipment;
            }
        }

        private Model.M_Card currentCard;
        public Model.M_Card CurrentCard
        {
            get { return currentCard; }
        }

        private Model.EF.Users currentUser;
        public Model.EF.Users CurrentUser
        {
            get { return currentUser; }
        }

        #region StatusBar_Properties
        public string Date
        {
            get { return currentCard.MakeDate.ToString("dd MMMMMMMMMM yyyy"); }
        }

        public string Maker
        {
            get { return currentCard.UserName; }
        }
        #endregion

        public VM_EditWindow(View.EditWindow HNDL, int? CurrentCardId = null)
        {
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
        }

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
            currentCard.UU = currentEquipment.Results.Summ;
        }

        #region Commands
        private Command print;
        public Command Print
        {
            get
            {
                return print ?? (print = new Command(obj =>
                {
                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = false;
                    excelApp.DisplayAlerts = false;

                    Excel.Workbook workBook;
                    Excel.Worksheet workSheet;

                    string str = Environment.CurrentDirectory;
                    if (CurrentEquipment.LimbsCount <= 8)
                    {
                        workBook = excelApp.Workbooks.Open(str + @"\Data\8.xls");
                    }
                    else
                    {
                        if (CurrentEquipment.LimbsCount <= 16)
                        {
                            workBook = excelApp.Workbooks.Open(str + @"\Data\16.xls");
                        }
                        else
                        {
                            if (CurrentEquipment.LimbsCount <= 32)
                            {
                                workBook = excelApp.Workbooks.Open(str + @"\Data\32.xls");
                            }
                            else
                            {
                                if (CurrentEquipment.LimbsCount <= 64)
                                {
                                    workBook = excelApp.Workbooks.Open(str + @"\Data\64.xls");
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Печать для " + CurrentEquipment.LimbsCount + " шлейфов - невозможна!", "Ошибка");
                                    excelApp.Quit();
                                    return;
                                }
                            }
                        }
                    }
                    
                    workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);

                    workSheet.PrintOut();
                    excelApp.Quit();
                }));
            }
        }

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
                        currentCard.PKP = currentPKP.PKPIndex.Name;

                        currentCard.MakeDate = DateTime.Now;
                        currentCard.User = Model.EF.EntityInstance.UserID;

                        currentCard.Save();
                        currentObject.Save(currentCard.Id);
                        currentPKP.Save(currentCard.Id);
                        currentEquipment.Save(currentCard.Id);
                    }
                }));
            }
        }

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
                        System.Windows.MessageBox.Show(WinLink, "Данные сохранены!", "Сохранить");
                        CurrentObject.Changed = false;
                        CurrentPKP.Changed = false;
                        changed = false;
                    }
                }));
            }
        }

        private Command menuCloseWindow;
        public Command MenuCloseWindow
        {
            get
            {
                return menuCloseWindow ?? (menuCloseWindow = new Command(obj =>
                {
                    WinLink.Close();
                }));
            }
        }

        private Command openTSOEdit;
        public Command OpenTSOEdit
        {
            get
            {
                return openTSOEdit ?? (openTSOEdit = new Command(obj =>
                {
                    changed = true;
                    var wTemp = new View.TSOEditWindow();
                    var cTemp = new ViewModel.VM_TSOEditWindow(wTemp, currentEquipment.Models);
                    wTemp.Owner = WinLink;
                    wTemp.DataContext = cTemp;
                    wTemp.ShowDialog();
                    CountUU();
                    currentEquipment.Clear();
                }));
            }
        }

        private ViewModel.Command openTSOList;
        public ViewModel.Command OpenTSOList
        {
            get
            {
                return openTSOList ?? (openTSOList = new ViewModel.Command(obj =>
                {
                    changed = true;
                    var wTemp = new View.TSOWindow();
                    var cTemp = new ViewModel.VM_TSOWindow(currentPKP.Moduls, wTemp);
                    wTemp.Owner = WinLink;
                    wTemp.DataContext = cTemp;
                    wTemp.ShowDialog();
                    CountUU();
                }));
            }
        }

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
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}