using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Windows;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;
using Binding = System.Windows.Data.Binding;
using System;
using System.Reflection;
using System.Windows.Data;
using System.ComponentModel;
using _04_RibbonComboBinding.Global;
using _04_RibbonComboBinding.Model;
using System.Windows;

namespace _04_RibbonComboBinding
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                string tabName = "RibbonItemBinding";
                application.CreateRibbonTab(tabName);
                Assembly assembly = Assembly.GetExecutingAssembly();
                string assemblyPath = assembly.Location;


                //RibbonPanel
                RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "BindingTest");
                PushButtonData btnDataAdd = new PushButtonData("btnDataAddItem", "Add\nModel", assemblyPath, "_04_RibbonComboBinding.Commands.AddItem");
                PushButton btnAdd = ribbonPanel.AddItem(btnDataAdd) as PushButton;

                PushButtonData btnDataClear = new PushButtonData("btnDataClear", "Clear\nModels", assemblyPath, "_04_RibbonComboBinding.Commands.ClearModels");
                PushButton btnClear = ribbonPanel.AddItem(btnDataClear) as PushButton;

                PushButtonData btnDataRenameFirst = new PushButtonData("btnDataRenameFirst", "Rename\nFirst", assemblyPath, "_04_RibbonComboBinding.Commands.RenameFirst");
                PushButton btnRenameFirst = ribbonPanel.AddItem(btnDataRenameFirst) as PushButton;

                PushButtonData btnDataRemoveFirst = new PushButtonData("btnDataRemoveFirst", "Remove\nFirst", assemblyPath, "_04_RibbonComboBinding.Commands.RemoveFirst");
                PushButton btnRemoveFirst = ribbonPanel.AddItem(btnDataRemoveFirst) as PushButton;

                //RibbonComboBox
                ComboBoxData cmbDataModelSelector = new ComboBoxData("cmbDataModelSelector");
                ComboBox cmbModelSelector = ribbonPanel.AddItem(cmbDataModelSelector) as ComboBox;
                SetChainBinding();

                //Terminate
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                return Result.Failed;
            }
        }

        /// <summary>
        /// Set Binding of ComboBox
        /// </summary>
        private void SetChainBinding()
        {
            RibbonTabCollection collection = ComponentManager.Ribbon.Tabs;
            foreach (RibbonTab tab in collection)
            {
                if (tab.Name == "RibbonItemBinding")
                {
                    foreach (var panel in tab.Panels)
                    {
                        //Actually You can find the target by value of Id. Find the value by debugging.
                        var panelId = panel.Id;
                        if (panel.Cookie == "Panel=CustomCtrl_%RibbonItemBinding%BindingTest_BindingTest_BindingTest")
                        {
                            foreach (var item in panel.Source.Items)
                            {
                                var id = item.Id;
                                var name = item.Name;
                                if (id == "CustomCtrl_%CustomCtrl_%RibbonItemBinding%BindingTest%cmbDataModelSelector")
                                {
                                    //1. Cast as RibbonCombo (AdWindow);
                                    var cmbBox = item as RibbonCombo;

                                    //2. Create binding instance.
                                    Binding chainBinding = new Binding
                                    {
                                        Source = StaticValues.Models,
                                        Mode = BindingMode.OneWay
                                    };
                                    cmbBox.ItemsSourceBinding = chainBinding;

                                    //3. Set Path of binding instance.
                                    Binding chainNameBinding = new Binding
                                    {
                                        Path = new System.Windows.PropertyPath("Name")
                                    };

                                    //4. Set binding of combobox as the instance.
                                    cmbBox.ItemTemplateTextBinding = chainNameBinding;

                                    //5. Add a eventhandler (Optional, but may be useful)
                                    cmbBox.CurrentChanged += cmbChains_CurrentChanged;
                                }
                            }
                        }
                    }
                }
            }
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// RibbonCombo (AdWindows) Current Changed Handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cmbChains_CurrentChanged(object sender, RibbonPropertyChangedEventArgs e)
        {
            var currentSelection = e.NewValue as MyModel;
            MessageBox.Show($"You Selected {currentSelection.Name}");
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
