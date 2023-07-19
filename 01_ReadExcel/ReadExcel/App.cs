using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Reflection;

namespace ReadExcel
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                // Create a new Ribbon Tab
                string tabName = "ReadExcel";
                application.CreateRibbonTab(tabName);
                // assembly
                Assembly assembly = Assembly.GetExecutingAssembly();
                // assembly path
                string assemblyPath = assembly.Location;

                // Create a new Ribbon Panel
                RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "My Panel");
                // Create a new Push Button
                PushButtonData buttonData = new PushButtonData("GetExcelDataBtn", "GetExcelData", assemblyPath, "ReadExcel.Commands.GetExcelData");
                PushButton button = ribbonPanel.AddItem(buttonData) as PushButton;

                // return result
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                return Result.Failed;
            }
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
