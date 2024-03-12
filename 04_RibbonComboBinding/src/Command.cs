using _04_RibbonComboBinding.Global;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_RibbonComboBinding.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class AddItem : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            StaticValues.AddModel();
            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class ClearModels : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            StaticValues.ClearModels();
            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class RemoveFirst : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            StaticValues.RemoveFirst();
            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class RenameFirst : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            StaticValues.RenameFirstModel("Changed");
            return Result.Succeeded;
        }
    }
}
