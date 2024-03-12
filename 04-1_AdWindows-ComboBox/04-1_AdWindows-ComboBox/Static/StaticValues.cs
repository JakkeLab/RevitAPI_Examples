using _04_1_AdWindows_ComboBox.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _04_1_AdWindows_ComboBox.Static
{
    public class StaticValues
    {
        public static ObservableCollection<MyModel> Models { get; set; } = new ObservableCollection<MyModel>();

        public static void AddModels()
        {
            Models.Add(new MyModel("Item1"));
            Models.Add(new MyModel("Item2"));
            Models.Add(new MyModel("Item3"));
        }

        public static void ClearModels()
        {
            Models.Clear();
        }

        public static void RemoveModelAt(int index)
        {
            try
            {
                Models.RemoveAt(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void RenameModelAt(int index, string newName)
        {
            try
            {
                Models[index].Name = newName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
