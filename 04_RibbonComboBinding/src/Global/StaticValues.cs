using _04_RibbonComboBinding.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _04_RibbonComboBinding.Global
{
    public class StaticValues
    {
        private static int idx = 0;
        public static ObservableCollection<MyModel> Models { get; set; } = new ObservableCollection<MyModel>();

        /// <summary>
        /// Add Model
        /// </summary>
        public static void AddModel()
        {
            Models.Add(new MyModel($"Model {idx++}"));
        }

        /// <summary>
        /// Clear Models Collection
        /// </summary>
        public static void ClearModels()
        {
            Models.Clear();
        }

        /// <summary>
        /// Remove first item
        /// </summary>
        public static void RemoveFirst()
        {
            if(Models.Count == 0)
            {
                MessageBox.Show("No model in collection.");
            } 
            else
            {
                Models.RemoveAt(0);
            }
        }


        /// <summary>
        /// Rename first item's name property
        /// </summary>
        /// <param name="newName"></param>
        public static void RenameFirstModel(string newName)
        {
            if (Models.Count == 0)
            {
                MessageBox.Show("No model in collection.");
            }
            else
            {
                Models.First().Name = newName;
            }
        }
    }
}
