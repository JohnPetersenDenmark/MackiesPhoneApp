using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public  class FishShopDtoDetailed : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? OperationAreaId { get; set; }
        public int? EmployeeId { get; set; }

        public OperationAreaDto Area { get; set; }
        public EmployeeDto? Employee { get; set; }

        private bool _isVisibleTemplateSchedule;

       

        public bool IsVisibleTemplateSchedule
        {
            get => _isVisibleTemplateSchedule;
            set
            {

                _isVisibleTemplateSchedule = value;
                OnPropertyChanged(nameof(IsVisibleTemplateSchedule));
            }
        }

        private bool _isVisibleContactInfo; 
        public bool IsVisibleContactInfo
        {
            get => _isVisibleContactInfo;
            set
            {

                _isVisibleContactInfo = value;
                OnPropertyChanged(nameof(IsVisibleContactInfo));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
