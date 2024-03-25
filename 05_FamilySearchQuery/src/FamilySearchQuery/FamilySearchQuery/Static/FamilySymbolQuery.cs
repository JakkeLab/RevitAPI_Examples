using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilySearchQuery.Static
{
    public class FamilyQuery
    {
        /// <summary>
        /// 문서 내에 존재하는 패밀리를 수집합니다.
        /// By Category
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static IEnumerable<MyFamilyInfo> CollectFamilies(Document doc, BuiltInCategory category)
        {
            //Element 수집
            var elems = new FilteredElementCollector(doc)
                .OfCategory(category)
                .ToElements();
            
            //FamilySymbol 인것만 추출
            var symbols = elems
                .Where(elem => elem as FamilySymbol != null)        //FamilySymbol로 캐스팅이 가능한 것만 추출
                .Select(elem => elem as FamilySymbol)               //추출된 것들을 FamilySymbol로 캐스팅하여 추출
                .ToList();

            //결과 저장 후 반환
            var res = new List<MyFamilyInfo>();
            symbols.ForEach(sym => res.Add(new MyFamilyInfo(sym.Id, sym.FamilyName, sym.Name)));
            return res;
        }

        /// <summary>
        /// 문서 내의 패밀리 중 지정한 이름의 매개변수가 있는 패밀리만 추출
        /// By Category, Specific Parameter name
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="category"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static IEnumerable<MyFamilyInfo> CollectFamilies(Document doc, BuiltInCategory category, string parameterName)
        {
            //Element 수집
            var elems = new FilteredElementCollector(doc)
                .OfCategory(category)
                .ToElements();

            //FamlySymbol을 추출한 뒤, 해당 파라미터가 있는 항목만 필터링
            var symbols = elems
                .Where(elem => elem as FamilySymbol != null)
                .Select(elem => elem as FamilySymbol)
                .Where(sym => sym.LookupParameter(parameterName) != null)       //parameterName을 가지고 있는지 검사후 추출
                .ToList();

            //결과 저장 및 반환
            var res = new List<MyFamilyInfo>();
            symbols.ForEach(sym => res.Add(new MyFamilyInfo(sym.Id, sym.FamilyName, sym.Name)));
            return res;
        }

        /// <summary>
        /// 문서 내의 패밀리 중 지정한 이름의 매개변수가 있고, 지정한 값을 가진 패밀리만 추출
        /// By Category, Parameter name, Parameter value
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="category"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public static IEnumerable<MyFamilyInfo> CollectFamilies(Document doc, BuiltInCategory category, string parameterName, string parameterValue)
        {
            //Element 수집
            var elems = new FilteredElementCollector(doc)
                .OfCategory(category)
                .ToElements();

            //FamlySymbol을 추출한 뒤, 해당 파라미터가 있고, 지정된 값과 일치하는 항목만 필터링
            var symbols = elems
                .Where(elem => elem as FamilySymbol != null)
                .Select(elem => elem as FamilySymbol)
                .Where(sym => sym.LookupParameter(parameterName) != null)                               
                .Where(sym => sym.GetParameters(parameterName).First().AsString() == parameterValue)    //이부분은 찾고자 하는 파라미터의 특성에 따라 달라짐
                .ToList();

            //결과 저장 및 반환
            var res = new List<MyFamilyInfo>();
            symbols.ForEach(sym => res.Add(new MyFamilyInfo(sym.Id, sym.FamilyName, sym.Name)));
            return res;
        }
    }

    /// <summary>
    /// 패밀리 정보를 간단하게 저장해두기 위한 클래스
    /// Class for save family information in simple way.
    /// </summary>
    public class MyFamilyInfo
    {
        public MyFamilyInfo(ElementId revitElementId, string symbolName, string typeName)
        {
            RevitElementId = revitElementId;
            SymbolName = symbolName;
            TypeName = typeName;
        }

        public ElementId RevitElementId { get; set; }
        public string SymbolName { get; set; }
        public string TypeName { get; set; }
    }
}
