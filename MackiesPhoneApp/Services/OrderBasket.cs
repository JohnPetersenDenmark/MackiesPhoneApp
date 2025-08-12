using MackiesPhoneApp.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

public class OrderBasket : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public Order order { get; set; } = new();

    private decimal? _orderTotal;
    public decimal? OrderTotal
    {
        get => _orderTotal;
        private set
        {
            if (_orderTotal != value)
            {
                _orderTotal = value;
                OnPropertyChanged(nameof(OrderTotal));
            }
        }
    }

    public OrderBasket()
    {
        order.OrderItemsList = new ObservableCollection<OrderItem>();
        order.OrderItemsList.CollectionChanged +=  OrderItemsList_CollectionChanged;
    }

    public void AddOrderItemToBasket(OrderItem orderItem)
    {
        if (order.OrderItemsList.Any(item => item.productid == orderItem.productid))
            return;

        order.OrderItemsList.Add(orderItem);
    }

    public void RemoveOrderItemFromBasket(OrderItem orderItem)
    {
        order.OrderItemsList.Remove(orderItem);
    }

    public void ClearBasket()
    {
        order.OrderItemsList.Clear();
    }

    private void CalculateOrderTotal()
    {
        decimal? total = 0;
        foreach (var orderItem in order.OrderItemsList)
            total += orderItem.quantity * orderItem.unitprice;

        OrderTotal = total;
    }

    public bool IsProductInBasket(int productType, int productId)
    {
        var x = order.OrderItemsList.Where(product => product.productid == productId && product.producttype == productType);

        if (x.Count() > 0)
        {
            return true;
        }
        return false;
    }
    private void OrderItemsList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (OrderItem newItem in e.NewItems)
                newItem.PropertyChanged += OrderItem_PropertyChanged;
        }
        if (e.OldItems != null)
        {
            foreach (OrderItem oldItem in e.OldItems)
                oldItem.PropertyChanged -= OrderItem_PropertyChanged;
        }

        CalculateOrderTotal();
    }

    private void OrderItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OrderItem.quantity) || e.PropertyName == nameof(OrderItem.unitprice))
        {
            CalculateOrderTotal();
        }
    }

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

