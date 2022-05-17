using Client.Services.Models;
using System;
using System.IO;
using System.Reflection;
using Client.Services.Extensions;
using Word = Microsoft.Office.Interop.Word;

namespace Client.Services
{
    public class PrintService
    {
        private const string LabelForButton = "по приему сигналов тревоги систем тревожной сигнализации, имеющихся на стационарных объектах юридических либо физических лиц, в том числе индивидуальных предпринимателей, и реагированию на эти сигналы";
        private const string LabelForSignalization = "по охране объектов (имущества) юридических или физических лиц, в том числе индивидуальных предпринимателей, с использованием средств и систем";

        private object _replaceStrObj = LabelForSignalization;
        
        public void PrintSimReport(SimReportData payload)
        {
            Word._Document document = null;

            object missingObj = Missing.Value;
            object falseObj = false;

            object templatePathObj = $@"{Directory.GetCurrentDirectory()}\Documents\SimReport.docx";
            

            Word._Application application = new Word.Application();

            try
            {
                document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception)
            {
                document?.Close(ref falseObj, ref missingObj, ref missingObj);
                application.Quit(ref missingObj, ref missingObj, ref missingObj);
                throw;
            }

            switch (payload.ConnectionType)
            {
                case ConnectionType.Button:
                    _replaceStrObj = LabelForButton;
                    break;
                case ConnectionType.Signalization:
                    _replaceStrObj = LabelForSignalization;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            document.Replace("@@Service", _replaceStrObj);
            document.Replace("@@ContractDate", payload.ContractDate.ToString("dd.MM.yyyy"));
            document.Replace("@@ContractNumber", payload.ContractNumber);

            document.Replace("@@Maker", payload.Maker ?? "");

            document.Replace("@@Operator1", payload.Operator1 ?? "");
            document.Replace("@@Operator2", payload.Operator2 ?? "");

            document.Replace("@@Phone1", payload.Phone1 ?? "");
            document.Replace("@@Phone2", payload.Phone2 ?? "");

            document.SaveAs($"D:\\{DateTime.Now:dd.MM.yyyy.hh.mm.ss}.docx");

            document.Close();
            application.Quit();
        }
    }
}
