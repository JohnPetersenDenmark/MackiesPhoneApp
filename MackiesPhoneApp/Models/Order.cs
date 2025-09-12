
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public  class Order  : INotifyPropertyChanged
    {
        private string _customerName;
        private string _customerEmail;
        private string _customerPhoneNumber;
        private ObservableCollection<OrderItem> _orderItemsList;



        public string CustomerName
        {
            get => _customerName;
            set
            {

                _customerName = value;
                OnPropertyChanged(nameof(CustomerName));                
            }
        }

        public string CustomerEmail
        {
            get => _customerEmail;
            set
            {

                _customerEmail = value;
                OnPropertyChanged(nameof(CustomerEmail));
            }
        }


        public string CustomerPhoneNumber
        {
            get => _customerPhoneNumber;
            set
            {

                _customerPhoneNumber = value;
                OnPropertyChanged(nameof(CustomerPhoneNumber));
            }
        }

        public ObservableCollection<OrderItem> OrderItemsList
        {
            get => _orderItemsList;
            set
            {

                _orderItemsList = value;
                OnPropertyChanged(nameof(OrderItemsList));
            }
        }

        
        public TemplateScheduleDto TemplateSchedule { get; set; }
        public FishShopDtoDetailed FishShop { get; set; }

        public int LocationId { get; set; }

        public string Comment { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
