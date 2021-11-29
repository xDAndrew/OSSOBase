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
            "Текущая выборка", "Старше 5 лет (ТО)", "Старше 10 лет (Кап.ремонт)"
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
                    SaveToFile(GetCardToTechService(5).ToList());
                    break;
                case 2:
                    SaveToFile(GetCardToTechService(10).ToList());
                    break;
            }
        });

        private IEnumerable<M_Card> GetCardToTechService(int years)
        {
            var year = int.Parse(SelectedYear) - years;

            return EntityInstance
                .DBContext
                .PKPSet
                .AsNoTracking()
                .Where(x => x.Date.Year <= year)
                .Include(x => x.Cards)
                .ToList()
                .Select(x => new M_Card(x.Cards));
        }

        private void SaveToFile(IEnumerable<M_Card> cards)
        {
            var mCards = cards.ToList();
            if (!mCards.Any())
            {
                MessageBox.Show("Не найдено объектов для экспорта", "Ошибка экспорта");
                return;
            }

            var excelApp = new Application
            {
                Visible = false,
                DisplayAlerts = false
            };

            var dir = Directory.GetCurrentDirectory();
            var workBook = excelApp.Workbooks.Open($@"{dir}\Documents\Report.xls");

            var fileCount = 0;
            if (Directory.Exists($"{dir}/reports"))
            {
                fileCount = Directory.GetFiles($"{dir}/reports").Count();
            }

            var workSheet = (Worksheet)workBook.ActiveSheet;

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
                index++;
            }

            var filename = $@"{dir}\Reports\{fileCount + 1}.{ReportTypes[TypeIndex]}_отчет.xls";
            workBook.SaveAs(filename);
            workBook.Close();
            excelApp.Quit();

            MessageBox.Show($"Выборка сохранена: {filename}", "Экспорт в файл");
            Process.Start($"{dir}/reports");
        }
    }
}
