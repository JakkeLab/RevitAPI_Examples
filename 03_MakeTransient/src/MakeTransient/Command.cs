using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeTransient
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Default var
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            using (var graphicsManager = TemporaryGraphicsManager.GetTemporaryGraphicsManager(doc))
            {
                var insertPoint = uidoc.Selection.PickPoint("Select insertion point");
                var imgData = new InCanvasControlData(@"C:\plus.bmp", XYZ.Zero);                    //You should update the path as yours
                var index = graphicsManager.AddControl(imgData, ElementId.InvalidElementId);
            }

            //Termination
            return Result.Succeeded;
        }
    }
}
