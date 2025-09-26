using MackiesPhoneApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace MackiesPhoneApp.Services
{
    public class OrderBasket : INotifyPropertyChanged
    {
        private Order _order = new();
        public Order order
        {
            get => _order;
            set
            {
                if (_order != value)
                {
                    _order = value;
                    OnPropertyChanged(nameof(order));
                }
            }
        }

        private decimal? _orderTotal;
        public decimal? OrderTotal
        {
            get => _orderTotal;
             set
            {
                if (_orderTotal != value)
                {
                    _orderTotal = value;
                    OnPropertyChanged(nameof(OrderTotal));
                }
            }
        }


        private ObservableCollection<OrderItem> _allProductsItems;
        public ObservableCollection<OrderItem> AllProductsItems
        {
            get => _allProductsItems;
            set
            {
                if (_allProductsItems != value)
                {
                    _allProductsItems = value;
                    OnPropertyChanged(nameof(AllProductsItems));
                }
            }
        }

        private bool _showFilters;
        public bool ShowFilters
        {
            get => _showFilters;
            set
            {

                _showFilters = value;
                OnPropertyChanged(nameof(ShowFilters));

            }
        }

        private ObservableCollection<ProductCategoryDto> _productCategories;
        public ObservableCollection<ProductCategoryDto> ProductCategories
        {
            get => _productCategories;
            set
            {
                
                    _productCategories = value;
                    OnPropertyChanged(nameof(ProductCategories));
                
            }
        }

        private ObservableCollection<ProductCategoryDto> _productLabels;
        public ObservableCollection<ProductCategoryDto> ProductLabels
        {
            get => _productLabels;
            set
            {

                _productLabels = value;
                OnPropertyChanged(nameof(ProductLabels));

            }
        }

        private bool _showProductCategories;
        public bool ShowProductCategories
        {
            get => _showProductCategories;
            set
            {

                _showProductCategories = value;
                OnPropertyChanged(nameof(ShowProductCategories));

            }
        }

        private ObservableCollection<ProductType> _productTypes;
        public ObservableCollection<ProductType> ProductTypes
        {
            get => _productTypes;
            set
            {

                _productTypes = value;
                OnPropertyChanged(nameof(ProductTypes));

            }
        }

        private bool _showProductTypes;
        public bool ShowProductTypes
        {
            get => _showProductTypes;
            set
            {

                _showProductTypes = value;
                OnPropertyChanged(nameof(ShowProductTypes));

            }
        }

        private ObservableCollection<string> _productFilters;
        public ObservableCollection<string> ProductFilters
        {
            get => _productFilters;
            set
            {

                _productFilters = value;
                OnPropertyChanged(nameof(ProductFilters));

            }
        }

        public OrderBasket()
        {
            order.OrderItemsList = new ObservableCollection<OrderItem>();
            _allProductsItems = new ObservableCollection<OrderItem>();

            SubscribeToAllProductItemsCollection(_allProductsItems);
        }


        private void SubscribeToAllProductItemsCollection(ObservableCollection<OrderItem> collection)
        {
            if (collection == null) return;

            collection.CollectionChanged += AllProductItemsCollectionChangedHandler;

            foreach (var item in collection)
                if (item is INotifyPropertyChanged npc)
                    npc.PropertyChanged += AllProductItem_PropertyChanged;

            OrderTotal = CalculateOrderTotal();
        }

        private void AllProductItemsCollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (OrderItem oldItem in e.OldItems)
                    if (oldItem is INotifyPropertyChanged npc)
                        npc.PropertyChanged -= AllProductItem_PropertyChanged;
            }

            if (e.NewItems != null)
            {
                foreach (OrderItem newItem in e.NewItems)
                    if (newItem is INotifyPropertyChanged npc)
                        npc.PropertyChanged += AllProductItem_PropertyChanged;
            }

            OrderTotal = CalculateOrderTotal();
        }

        private void AllProductItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderItem.quantity) ||
                e.PropertyName == nameof(OrderItem.unitprice))
            {
                OrderTotal = CalculateOrderTotal();
            }
        }

        public void AddOrderItemToBasket(OrderItem orderItem)
        {
            if (order.OrderItemsList.Any(item => item.productid == orderItem.productid))
                return;
            
            order.OrderItemsList.Add(orderItem);
            orderItem.IsInBasket = true;
            OrderTotal = CalculateOrderTotal();
        }

        public bool IsProductInBasket(int productId)
        {
            // return order.OrderItemsList.Any(product => product.productid == productId && product.producttype == productType);
            return order.OrderItemsList.Any(product => product.productid == productId );
        }

        public void RemoveOrderItemFromBasket(OrderItem orderItem)
        {
            order.OrderItemsList.Remove(orderItem);
            orderItem.IsInBasket = false;
            // orderItem.IsQuantityVisible = false;
            OrderTotal = CalculateOrderTotal();
        }

        public void ClearBasket()
        {
            order.OrderItemsList.Clear();

            order.CustomerName = string.Empty;
            order.CustomerEmail = string.Empty;
            order.CustomerPhoneNumber = string.Empty;
            order.Comment = string.Empty;
            order.LocationId = 0;

            OrderTotal = 0;

            foreach (var productItem in _allProductsItems)
            {
                productItem.IsInBasket = false;
                productItem.IsQuantityEditable = false;
                productItem.IsQuantityVisible = false;
            }
        }

        public decimal? CalculateOrderTotal()
        {
            decimal? orderTotal = 0;
            foreach (var orderItem in order.OrderItemsList)
                orderTotal += orderItem.quantity * orderItem.unitprice;

            return orderTotal;
        }
    
        // Update existing collection (keeps reference, UI still updates)
        public void UpdateAllProductsItems(List<OrderItem> newItems)
        {
            _allProductsItems.Clear();
            foreach (var item in newItems)
                _allProductsItems.Add(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
