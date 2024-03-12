using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using _04_1_AdWindows_ComboBox.TabUI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.DB.Events;
using System.Reflection;
using Autodesk.Windows;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;
using System.Windows.Data;
using Binding = System.Windows.Data.Binding;

namespace _04_1_AdWindows_ComboBox
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
                string tabName = "My Add-in";
                application.CreateRibbonTab(tabName);
                // assembly
                Assembly assembly = Assembly.GetExecutingAssembly();
                // assembly path
                string assemblyPath = assembly.Location;

                // Create a new Ribbon Panel
                RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "My Panel");

                // Create a new Push Button
                PushButtonData buttonData = new PushButtonData("MyButton", "MyButton", assemblyPath, "_04_1_AdWindows_ComboBox.Command");

                // Add the Push Button to the Ribbon Panel
                PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;

                //Create Show Button
                PushButtonData showButtonData = new PushButtonData("Show Window", "Show", assemblyPath, "_04_1_AdWindows_ComboBox.Show");
                PushButton showButton = ribbonPanel.AddItem(showButtonData) as PushButton;

                //2-2. 바인딩 설정부분
                Binding chainBinding = new Binding
                {
                    Source = SHVMObjects.Chains,
                    Mode = BindingMode.OneWay
                };
                cmbCurrentChain.ItemsSourceBinding = chainBinding;
                cmbCurrentChain.CurrentChanged += CmbCurrentChain_CurrentChanged;

                Binding chainNameBinding = new Binding
                {
                    Path = new PropertyPath("ChainName")
                };

                // return result
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                return Result.Failed;
            }
        }

        private void CmbCurrentChain_CurrentChanged(object sender, RibbonPropertyChangedEventArgs e)
        {

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
