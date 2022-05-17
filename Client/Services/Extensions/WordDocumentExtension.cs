using System.Reflection;

namespace Client.Services.Extensions
{
    public static class WordDocumentExtension
    {
        public static void Replace(this Microsoft.Office.Interop.Word._Document document, object strToFindObj, object replaceStrObj)
        {
            object missingObj = Missing.Value;

            object replaceTypeObj = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

            for (var i = 1; i <= document.Sections.Count; i++)
            {
                var wordRange = document.Sections[i].Range;

                var wordFindObj = wordRange.Find;
                var wordFindParameters = new[] { strToFindObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, replaceStrObj, replaceTypeObj, missingObj, missingObj, missingObj, missingObj };

                wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj, wordFindParameters);
            }
        }
    }
}
