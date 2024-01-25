using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Microsoft.Win32;
using ReadExcel.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcel.Commands
{

    [Transaction(TransactionMode.Manual)]
    public class GetExcelData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Revit 내장 변수
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            //명령부
            OpenFileDialog openfileDialog = FileIO.OpenFile();
            if(openfileDialog.ShowDialog() == false) 
                return Result.Cancelled;

            string fileName = openfileDialog.FileName;
            string sheetName = "ElementIds";
            List<int> elementIdValues = new List<int>();
            
            //엑셀 데이터 가져오기
            FileIO.ReadExcelData(fileName, sheetName, out elementIdValues);

            //ICollection 생성
            ICollection<ElementId> elementIdsToSelect = GetElementsWithIds(elementIdValues, doc);

            Selection selection = uidoc.Selection;
            selection.SetElementIds(elementIdsToSelect);

            //Terminate
            return Result.Succeeded;
        }

        public ICollection<ElementId> GetElementsWithIds(List<int> elementIdValues, Document doc)
        {
            ICollection<ElementId> selectedElementIds = new List<ElementId>();

            foreach (int elementIdValue in elementIdValues)
            {
                ElementId elementId = new ElementId(elementIdValue);
                Element element = doc.GetElement(elementId);

                if (element != null)
                {
                    selectedElementIds.Add(elementId);
                }
            }
            return selectedElementIds;
        }
    }
}
