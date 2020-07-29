using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Client.Model;
using Client.Model.EquipmentModel;
using Client.Services.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Client.Services
{
    public class PrintService : IPrintService
    {
        private readonly string _path;

        public PrintService()
        {
            _path = Directory.GetCurrentDirectory() + "/";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public void PrintCardDocument(M_Card cardInfo, M_PKP pkpInfo, Equipment equipment)
        {
            var templateFileInfo = new FileInfo($"{_path}Template.xlsx");
            using (var eP = new ExcelPackage(templateFileInfo))
            {
                var sheet = eP.Workbook.Worksheets.First();
                sheet.Cells[3, 1].Value = cardInfo.Owner + " " + cardInfo.ObjectView + ", " + cardInfo.Address;

                sheet.Cells[9, 2].Value = pkpInfo.PKPIndex.Name;
                sheet.Cells[9, 4].Value = pkpInfo.Serial;
                sheet.Cells[9, 7].Value = pkpInfo.Password;
                sheet.Cells[9, 10].Value = pkpInfo.Phone;
                sheet.Cells[9, 24].Value = pkpInfo.SelectedDate.ToLongDateString();

                var index = 2;
                foreach (var module in pkpInfo.Moduls.Items)
                {
                    sheet.Cells[12, index].Value = module.Name;
                    sheet.Cells[15, index].Value = module.Count;
                    index++;
                }
                sheet.Cells[15, 29].Value = pkpInfo.Moduls.FullSumm;

                index = 13;
                foreach (var item in equipment.Models.Items)
                {
                    sheet.Cells[12, index].Value = item.Name;
                    index++;
                }

                index = 16;
                sheet.InsertRow(index, equipment.Branches.Count);
                foreach (var branch in equipment.Branches)
                {
                    sheet.Row(index).Height = 25;
                    sheet.Row(index).Style.Font.SetFromFont(new Font("Arial", 14));
                    sheet.Cells[index, 2, index, 12].Merge = true;
                    sheet.Cells[index, 1, index, 29].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[index, 1, index, 29].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[index, 1, index, 29].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[index, 1, index, 29].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    sheet.Cells[index, 1].Value = branch.Number;
                    sheet.Cells[index, 2].Value = branch.Name;

                    for (var x = 0; x < equipment.Models.Items.Count; x++)
                        sheet.Cells[index, x + 13].Value = equipment.Branches[index - 16][x];

                    sheet.Cells[index, 29].Value = branch.Summ;
                    index++;
                }

                for (var i = 0; i < equipment.Models.Items.Count; i++)
                {
                    sheet.Cells[index, i + 13].Value = equipment.Results[i];
                }
                sheet.Cells[index, 29].Value = equipment.Results.Summ;

                index += 2;
                sheet.Cells[index, 2].Value = cardInfo.UserName;
                index += 2;
                sheet.Cells[index, 2].Value = cardInfo.MakeDate.ToLongDateString();

                using (var fileStream = File.Create(DateTime.Now.ToString("MMddyyyy-hhmmss") + ".xlsx"))
                {
                    eP.SaveAs(fileStream);
                }
            }
        }
    }
}
