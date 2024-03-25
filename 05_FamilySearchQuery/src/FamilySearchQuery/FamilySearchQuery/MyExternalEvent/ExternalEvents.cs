using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using FamilySearchQuery.Static;
using FamilySearchQuery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilySearchQuery.MyExternalEvent
{
    
    public class QueryEvent : IExternalEventHandler
    {
        /// <summary>
        /// For allocate your viewmodel.
        /// </summary>
        public SearchViewModel CurrentViewModel;
        public void Execute(UIApplication app)
        {
            var uidoc = app.ActiveUIDocument;
            var doc = uidoc.Document;
            BuiltInCategory cateogry = (BuiltInCategory)Enum.Parse(typeof(BuiltInCategory), CurrentViewModel.CurrentCategoryName);

            IEnumerable<MyFamilyInfo> familyInfos = null;
            switch (CurrentViewModel.CurrentMode)
            {
                case SearchMode.CategoryOnly:
                    familyInfos = FamilyQuery.CollectFamilies(doc, cateogry);
                    break;
                case SearchMode.CategoryAndParameterExist:
                    familyInfos = FamilyQuery.CollectFamilies(doc, cateogry, CurrentViewModel.TargetParameterName);
                    break;
                case SearchMode.CategoryAndParameterValue:
                    familyInfos = FamilyQuery.CollectFamilies(doc, cateogry, CurrentViewModel.TargetParameterName, CurrentViewModel.TargetParameterValue);
                    break;
            }

            if(familyInfos == null)
            {
                return;
            }
            else
            {
                CurrentViewModel.InsertFamilies(familyInfos);
            }
        }

        public string GetName()
        {
            return "FamilyQuery";
        }
    }
}
