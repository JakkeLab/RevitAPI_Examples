using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.DB.Events;
using System.Reflection;

namespace FamilySearchQuery
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                string tabName = "FamilySearch";
                application.CreateRibbonTab(tabName);
                Assembly assembly = Assembly.GetExecutingAssembly();
                string assemblyPath = assembly.Location;

                //Ribbon Panel
                RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Families");
                PushButtonData btnDataFamilySearch = new PushButtonData("btnDataFamilySearch", "Search\nFamily", assemblyPath, "FamilySearchQuery.Command");

                PushButton btnFamilySearch = ribbonPanel.AddItem(btnDataFamilySearch) as PushButton;

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

    public class CommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication app, CategorySet cate)
        {
            if (app.ActiveUIDocument == null)
            {
                //disable register btn
                return true;
            }
            //enable register btn
            return false;
        }
    }
}
