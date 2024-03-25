using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using FamilySearchQuery.MyExternalEvent;
using FamilySearchQuery.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilySearchQuery
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Revit 내장 변수
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            //패밀리 쿼리 창 인스턴스 만들기
            WDFamilyQuery familyQuery = new WDFamilyQuery();

            //IExternalEvent 구현체 클래스의 인스턴스 생성하기
            QueryEvent queryEvent = new QueryEvent();
            queryEvent.CurrentViewModel = familyQuery.ViewModel;    //구현체에 뷰모델 속성을 만들어서 연결할 수 있다.

            var exernalEventInstance = ExternalEvent.Create(queryEvent);            //ExternalEvent 인스턴스는 반드시 Revit API 환경 내에서 생성해야 예외가 안뜬다.
            familyQuery.ViewModel.ExternealEventInstance = exernalEventInstance;    //패밀리 쿼리 창의 뷰모델에 ExternalEvent 인스턴스 연결
            familyQuery.Show();

            return Result.Succeeded;
        }
    }
}
