<H1>Make Family Search Window </H1>

<H3> Topic APIs </H3>
<ul>
  <li/>FilteredElementCollector
  <li/>LookupParameter()
  <li/>IExternalEventHandler
  <li/>And link your viewmodel to your ExternalEvent
</ul>

---

Sometimes, we need to create functions to search items.
In this example, you will create Family search window by Category, Parameter, Parameter value.<br/>
Architecture based on MVVM for using WPF in comfortable way.<br/><br/>

[![Video Label](http://img.youtube.com/vi/FEKfmP9JXF4/0.jpg)](https://youtu.be/FEKfmP9JXF4?si=7KqFyfNuPAalvYCr)<br/>
Click to watch on Youtube.

---

Actually, you can link your viewmodel on your ExternalEvent instance.<br/>
![image](https://github.com/JakkeLab/RevitAPI_Examples/assets/73068969/ddc8a19a-5bba-4549-958e-187b58c13ccb) <br/>

Explain of code is written on .cs scripts.
Just set debug as running external application and debug on Revit.

---

PS..<br/>
In fact, I didn't use command for this example. So that it would be not fullfilled MVVM. Just seperated model from view and linked methods of viewmodel to event handlers of view.
For time saving.
<br/>
<br/>
