using Client.Model;
using Client.Model.EF;
using Client.ViewModel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace Client.BindingContexts
{
    public class ReportWindowContext : BaseBindingContext
    {
        private readonly List<M_Card> _cards;

        public ReportWindowContext(IEnumerable<M_Card> currentCards)
        {
            _cards = currentCards.ToList();
        }

        public List<string> ReportTypes => new List<string>
        {
            "Текущая выборка", "Объекты 5 лет (ТО)", "Старше 10 лет (Кап.ремонт)"
        };

        private int _type;

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public int TypeIndex
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged("IsYearVisible");
                OnPropertyChanged("IsFormValid");
            }
        }

        private string _year = DateTime.Now.AddYears(1).Year.ToString();

        [NotifyParentProperty(true)]
        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public string SelectedYear
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged("IsFormValid");
            }
        }

        public bool IsYearVisible => TypeIndex == 0;

        public bool IsFormValid =>
            (TypeIndex == 0) || (TypeIndex > 0 && int.TryParse(SelectedYear, out var y) && y >= 2010);

        public Command OnClickExport => new Command(obj =>
        {
            switch (_type)
            {
                case 0:
                    SaveToFile(_cards);
                    break;
                case 1:
                    SaveToFile(GetCardToTechService(5, true).ToList());
                    break;
                case 2:
                    SaveToFile(GetCardToTechService(10).ToList());
                    break;
            }
        });

        private IEnumerable<M_Card> GetCardToTechService(int years, bool currentYear = false)
        {
            var year = int.Parse(SelectedYear) - years;

            if (currentYear)
            {
                return EntityInstance
                    .DBContext
                    .PKPSet
                    .AsNoTracking()
                    .Where(x => x.Date.Year == year)
                    .Where(x => x.Cards.StatusView == 1)
                    .Include(x => x.Cards)
                    .ToList()
                    .Select(x => new M_Card(x.Cards));
            }

            return EntityInstance
                .DBContext
                .PKPSet
                .AsNoTracking()
                .Where(x => x.Date.Year <= year)
                .Where(x => x.Cards.StatusView == 1)
                .Include(x => x.Cards)
                .ToList()
                .Select(x => new M_Card(x.Cards));
        }

        private void SaveToFile(IEnumerable<M_Card> cards)
        {
            var enumerable = cards.ToList();
            var mCards = enumerable.ToList();
            if (!mCards.Any())
            {
                MessageBox.Show("Не найдено объектов для экспорта", "Ошибка экспорта");
                return;
            }

            var rowCount = enumerable.Count();

            var excelApp = new Application
            {
                Visible = false,
                DisplayAlerts = false
            };

            var dir = Directory.GetCurrentDirectory();
            var workBook = excelApp.Workbooks.Add(Type.Missing);

            var fileCount = 0;
            if (Directory.Exists($"{dir}/reports"))
            {
                var directory = new DirectoryInfo($"{dir}/Reports");
                var files = directory.GetFiles();
                fileCount = files.Count(f => !f.Attributes.HasFlag(FileAttributes.Hidden));
            }
            else
            {
                Directory.CreateDirectory($"{dir}/Reports");
            }

            var workSheet = (Worksheet)workBook.ActiveSheet;

            workSheet.Cells[1, 1] = "п/п";
            workSheet.Cells[1, 2] = "Договор";
            workSheet.Cells[1, 3] = "Заказчик";
            workSheet.Cells[1, 4] = "Объект";
            workSheet.Cells[1, 5] = "Адрес";
            workSheet.Cells[1, 6] = "Кол-во У.У.";
            workSheet.Cells[1, 7] = "Автор";
            workSheet.Cells[1, 8] = "Дата приемки";

            workSheet.Columns[1].ColumnWidth = 11;
            workSheet.Columns[2].ColumnWidth = 11;
            workSheet.Columns[3].ColumnWidth = 60;
            workSheet.Columns[4].ColumnWidth = 60;
            workSheet.Columns[5].ColumnWidth = 30;
            workSheet.Columns[6].ColumnWidth = 30;
            workSheet.Columns[7].ColumnWidth = 30;
            workSheet.Columns[8].ColumnWidth = 30;

            workSheet.Range["A1", "H1"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            workSheet.Range["C2", $"E{rowCount + 1}"].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            workSheet.Range["A1", $"B{rowCount + 1}"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            workSheet.Range["F1", $"F{rowCount + 1}"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            workSheet.Range["H1", $"H{rowCount + 1}"].HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var range = workSheet.UsedRange;
            var cell = range.Range["A1", $"H{rowCount + 1}"];
            var border = cell.Borders;
            border.LineStyle = XlLineStyle.xlContinuous;
            border.Weight = 2d;

            var index = 2;
            foreach (var card in mCards)
            {
                workSheet.Cells[index, 1] = index - 1;
                workSheet.Cells[index, 2] = card.Contract;
                workSheet.Cells[index, 3] = card.Owner;
                workSheet.Cells[index, 4] = card.ObjectView;
                workSheet.Cells[index, 5] = card.Address;
                workSheet.Cells[index, 6] = card.UU;
                workSheet.Cells[index, 7] = card.UserName;
                workSheet.Cells[index, 8] = card.StartService.Year > 2000 ? card.StartService.ToString("dd.MM.yyyy") : "Не установлено";
                index++;
            }

            var filename = $@"{dir}\Reports\{fileCount + 1}.{ReportTypes[TypeIndex]}_отчет.xlsx";
            workBook.SaveAs(filename);
            workBook.Close();
            excelApp.Quit();

            if (MessageBox.Show(
                    $"Выборка сохранена: \"{fileCount + 1}.{ReportTypes[TypeIndex]}_отчет.xlsx\"\nОткрыть папку?",
                    "Экспорт в файл", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Process.Start($"{dir}/reports");
            }
        }
    }
}
