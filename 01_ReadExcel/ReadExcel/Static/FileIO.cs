using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows;
using Autodesk.Revit.UI;

namespace ReadExcel.Static
{
    public static class FileIO
    {
        //파일 읽기
        public static void ReadExcelData(string filename, string sheetName, out List<int> elemIdList)
        {
            //Out 변수 초기화
            elemIdList = null;
            if(File.Exists(filename) == false)
            {
                return;
            }

            //엑셀 워크북 변수 null로 초기화
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                //ElementId를 담을 리스트 선언
                List<int> elemIdValues = new List<int>();
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Open(filename);

                //Linq를 활용하여 Sheets를 Excel.Worksheet 열거형으로 변환 후, 시트이름이 sheetName인 시트 고르기
                ws = wb.Sheets.Cast<Excel.Worksheet>().FirstOrDefault(sheet => sheet.Name == sheetName);

                //시트가 반환되지 않은경우(ex, 해당이름의 시트가 없을때)
                if (ws == null) return;

                //1열인 셀 중 1~7행의 셀을 읽어서 값을 elemIdValues에 넣어주기
                for(int i = 1; i < 8; i++)
                {
                    var col = ws.Columns[0];
                    var row = col.Row[i];
                    elemIdValues.Add(Int32.Parse(row.Value.ToString()));
                }
                elemIdList = elemIdValues;

                //엑셀 프로세스 해제 (해주지않으면 백그라운드에서 계속 엑셀이 돌아가서 여러가지 문제 발생시킬 수 있음)
                wb.Close(false, Type.Missing, Type.Missing);
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message, TaskDialogCommonButtons.Ok);
            }
            finally
            {
                //메모리 해제
                ReleaseObject(wb);
                ReleaseObject(ws);
            }
        }

        //Excel 메모리 해제
        private static void ReleaseObject(object obj)
        {
            try
            {
                if(obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw;
            }
            finally
            {
                GC.Collect();
            }
        }
        
        //파일 열기 대화상자
        public static OpenFileDialog OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.ValidateNames = true;

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string filepath = Path.GetDirectoryName(path);

            openFileDialog.InitialDirectory = filepath;
            openFileDialog.DefaultExt = ".xlsx";
            openFileDialog.Filter = "XML (*.xlsx) | *.xlsx";

            return openFileDialog;
        }
    }
}
