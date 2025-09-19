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

       
        public string CategoryName { get; set; }

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
