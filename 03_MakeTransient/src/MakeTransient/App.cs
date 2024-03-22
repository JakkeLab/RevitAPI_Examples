using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.DB.Events;
using System.Reflection;
using Autodesk.Revit.DB.ExternalService;
using System.Collections.Generic;

namespace MakeTransient
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
                string tabName = "Test";
                application.CreateRibbonTab(tabName);

                // assembly
                Assembly assembly = Assembly.GetExecutingAssembly();
                // assembly path
                string assemblyPath = assembly.Location;

                // Create a new Ribbon Panel
                RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "My Panel");

                // Create a new Push Button
                PushButtonData buttonData = new PushButtonData("btnTemporaryButton", "Test", assemblyPath, "MakeTransient.Command");

                // Add the Push Button to the Ribbon Panel
                PushButton pushButton = ribbosnPanel.AddItem(buttonData) as PushButton;

                #region Register Click Handler
                var clickHandler = new ImgClickHandler();
                var temporaryGrahpicsService = ExternalServiceRegistry.GetService(clickHandler.GetServiceId());
                temporaryGrahpicsService.AddServer(clickHandler);
                ((MultiServerService)temporaryGrahpicsService).SetActiveServers(new List<Guid> { clickHandler.GetServerId() });
                #endregion

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
