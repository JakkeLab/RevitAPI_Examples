using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilySearchQuery.Static
{
    public class FamilySymbolQuery
    {
        /// <summary>
        /// 문서 내의 특정 카테고리에 해당하는 Element를 찾아서 IEnumerable로 반환합니다.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static IEnumerable<ElementInfo> CollectElements(Document doc, BuiltInCategory category)
        {
            var res = new List<ElementInfo>();

            //Element 검색
            var elems = new FilteredElementCollector(doc)   //FilteredElementCollect 선언하여 검색 대상을 문서 전체의 Element로 지정함
                .OfCategory(category)                       //FilteredElementCollect의 검색범위를 category에 해당하는 것으로 줄임
                .ToElements()                               //Element들로 바꿈
                .ToList();                                  //이 부분은 본인 필요에 맞게 바꿔 사용

            //결과 저장 후 반환
            elems.ForEach(elem => res.Add(new ElementInfo(elem.Id, elem.Name)));
            return res;
        }

        /// <summary>
        /// 문서 내에 존재하는 패밀리를 수집합니다.
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
    /// 엘리먼트 정보를 간단하게 저장해두기 위한 클래스
    /// </summary>
    public class ElementInfo
    {
        public ElementInfo(ElementId revitElementId, string name)
        {
            RevitElementId = revitElementId;
            Name = name;
        }

        public ElementId RevitElementId { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// 패밀리 정보를 간단하게 저장해두기 위한 클래스
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
