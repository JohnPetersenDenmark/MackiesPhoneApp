
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public class OrderItem : INotifyPropertyChanged
    {
        private int _quantity;

        private bool _isInBasket;

        public int orderid { get; set; }

        public int productid { get; set; }

        public string productname { get; set; }

        public string pizzanumber { get; set; }

        public string imageurl { get; set; }

        public string productdescription { get; set; }
    
        public bool selected { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? unitprice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? unitdiscountpercentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? discountedunitprice { get; set; }

        public int producttype { get; set; }

        public bool IsInBasket
        {
            get => _isInBasket;
            set
            {               
                _isInBasket = value;
                OnPropertyChanged(nameof(IsInBasket));               
            }
        }

        private bool _isQuantityVisible;
        public bool IsQuantityVisible
        {
            get => _isQuantityVisible;
            set
            {
                if (_isQuantityVisible != value)
                {
                    _isQuantityVisible = value;
                    OnPropertyChanged(nameof(IsQuantityVisible));
                }
            }
        }

        private bool _isQuantityEditable;
        public bool IsQuantityEditable
        {
            get => _isQuantityEditable;
            set
            {
                if (_isQuantityEditable != value)
                {
                    _isQuantityEditable = value;
                    OnPropertyChanged(nameof(IsQuantityEditable));
                }
            }
        }

        public int quantity
        {
            get => _quantity;
            set
            {
                if (_quantity == value) return;
                _quantity = value;
                OnPropertyChanged(nameof(quantity));
                OnPropertyChanged(nameof(LineTotal));
            }
        }

        public decimal LineTotal => (decimal)(quantity * ( unitprice != null ? unitprice : 0));


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
