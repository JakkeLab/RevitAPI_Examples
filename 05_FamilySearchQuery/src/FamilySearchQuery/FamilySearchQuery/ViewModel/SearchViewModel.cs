using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using FamilySearchQuery.Static;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Document = Autodesk.Revit.DB.Document;

namespace FamilySearchQuery.ViewModel
{
    public class SearchViewModel
    {
        /// <summary>
        /// 패밀리 쿼리 뷰와 연결시킬 뷰모델
        /// ViewModel for FamilyQuery window.
        /// </summary>
        public SearchViewModel()
        {
            TargetFamilies = new ObservableCollection<MyFamilyInfo>();
            CurrentMode = SearchMode.CategoryOnly;

            var categories = Enum.GetNames(typeof(BuiltInCategory))
                .OrderBy(n => n);
            BuiltInCategoryNames = new ObservableCollection<string>(categories);
        }

        /// <summary>
        /// Revit 내장 카테고리 문자열
        /// Collection of Revit BuiltInCategory
        /// </summary>
        public ObservableCollection<string> BuiltInCategoryNames { get; set; }

        /// <summary>
        /// ExternalEvent 인스턴스 속성
        /// ExternalEvent instance.
        /// </summary>
        public ExternalEvent ExternealEventInstance { get; set; }

        /// <summary>
        /// 검색된 패밀리 정보를 저장하기 위한 속성
        /// Collected families will be added here.
        /// </summary>
        public ObservableCollection<MyFamilyInfo> TargetFamilies { get; protected set; }

        /// <summary>
        /// 검색된 패밀리 삽입 메소드
        /// Insert method to add searched items.
        /// </summary>
        /// <param name="familyInfos"></param>
        public void InsertFamilies(IEnumerable<MyFamilyInfo> familyInfos)
        {
            TargetFamilies.Clear();
            familyInfos
                .ToList()
                .ForEach(i => TargetFamilies.Add(i));
        }

        /// <summary>
        /// 현재 검색 모드
        /// Current search mode.
        /// </summary>
        public SearchMode CurrentMode { get; set; }
        
        /// <summary>
        /// 현재 선택된 카테고리 이름
        /// Selected BuiltInCategory name
        /// </summary>
        public string CurrentCategoryName { get; set; }

        /// <summary>
        /// 찾고자 하는 파라미터의 이름
        /// Parameter name to look up
        /// </summary>
        public string TargetParameterName { get; set; }

        /// <summary>
        /// 찾고자 하는 파라미터의 값
        /// Parameter value to look up
        /// </summary>
        public string TargetParameterValue { get; set; }

    }

    /// <summary>
    /// 검색모드 Enum
    /// Enum of search mode.
    /// </summary>
    public enum SearchMode
    {
        CategoryOnly,               //카테고리에 속한 것을 검색 (Filtering by only BuiltInCategory)
        CategoryAndParameterExist,  //카테고리, 해당 이름의 파라미터를 가지고 있는 것을 검색 (Filtering by BuiltInCategory, whether the family has the named parameter)
        CategoryAndParameterValue   //카테고리, 해당 이름의 파라미터를 가지고 있고 지정한 값인 것을 검색 (Filtering by BuiltInCategory, whether the family has the named parameter and check whether it matches your value)
    }
}
