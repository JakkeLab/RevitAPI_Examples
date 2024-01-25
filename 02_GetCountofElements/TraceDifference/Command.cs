using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace TraceDifference
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            //Import Compare RVT File
            var fileName = GetFileName();

            //Check RVT Versions

            Document docToCompare = app.OpenDocumentFile(fileName);
            bool isSameVersion = IsSameVersion(doc, docToCompare);

            if(!isSameVersion)
            {
                //Escape code block
                return Result.Cancelled;
            }

            var allElementsFromCurrent = new FilteredElementCollector(doc);
            var allElementsFromTarget = new FilteredElementCollector(docToCompare);

            //Get only Structural columns
            var colsCurrent = allElementsFromCurrent.OfCategory(BuiltInCategory.OST_StructuralColumns);
            var colsTarget = allElementsFromTarget.OfCategory(BuiltInCategory.OST_StructuralColumns);

            var colCntCurrent = colsCurrent.GetElementCount();
            var colCntTarget = colsTarget.GetElementCount();
            string result = colCntTarget - colCntCurrent > 0 ? "+" + (colCntTarget - colCntCurrent).ToString() : (colCntTarget - colCntCurrent).ToString();
            TaskDialog.Show("Count Column Element",$"Comparer Counts : {result}");

            return Result.Succeeded;
        }

        public string GetFileName()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "RVT Files (*.RVT)|*.rvt";

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }           
        }
        
        public bool IsSameVersion(Document doc1, Document doc2)
        {
            var doc1Version = BasicFileInfo.Extract(doc1.PathName);
            var doc2Version = BasicFileInfo.Extract(doc2.PathName);
            return doc1Version.Format == doc2Version.Format;
        }
    }
}
