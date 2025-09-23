using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MackiesPhoneApp.Models
{
    public class ProductCategoryDto : INotifyPropertyChanged
    {
      
        public int Id { get; set; }

       
       private string __categoryName;
        public string CategoryName
        {
            get => __categoryName;
            set
            {
                if (__categoryName != value)
                {
                    __categoryName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoryName)));
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
            }
        }

      

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
