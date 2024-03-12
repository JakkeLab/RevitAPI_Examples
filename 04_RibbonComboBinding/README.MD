<H1>04_Set Binding on ComboBox of Ribbon</H1>

In Revit API, there is no method to set binding data to ComboBox of Ribbon
(Like the binding of WPF-MVVM Structure).

<img src="img_apidoc.png"><br/>
As reading the document of API, there is only "Adding" method.

Actually, AdWindows.dll is the library for calling API on AutoCAD Environment, not for Revit. But, some classes can be converted to the class of AdWindows. <br/>
<img src="./running.gif">

<H3>Main method of setting Binding</H3>

```cs
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
```

If you are understanding the data binding(mostly used at MVVM Project of WPF), you would be able to apply full CRUD on ComboBox.<br/>



Thanks to Rudolf Honk, who did research of using AdWindows on Revit Environment.
