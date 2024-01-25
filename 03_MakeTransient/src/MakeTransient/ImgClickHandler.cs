using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExternalService;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeTransient
{
    public class ImgClickHandler : ITemporaryGraphicsHandler
    {
        public string GetDescription()
        {
            return "Jakkelab - img click handler";
        }

        public string GetName()
        {
            return "Img Click Handler";
        }

        public Guid GetServerId()
        {
            return new Guid("b3234f30-ef62-4691-b045-c27689f3f3dc");                //Please set your own GUID
        }

        public ExternalServiceId GetServiceId()
        {
            return ExternalServices.BuiltInExternalServices.TemporaryGraphicsHandlerService;
        }

        public string GetVendorId()
        {
            return "JakkeLab";
        }

        public void OnClick(TemporaryGraphicsCommandData data)
        {
            //Call Temporary graphics manager of the document.
            var graphicsManager = TemporaryGraphicsManager.GetTemporaryGraphicsManager(data.Document);
            
            //Clear all temporary graphics
            graphicsManager.Clear();

            //Your action here
            TaskDialog.Show("Temp Graphics", "Hello");
        }
    }
}
