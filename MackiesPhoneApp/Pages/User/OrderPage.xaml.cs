using CommunityToolkit.Maui.Views;

using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;
using Microsoft.Maui.Controls;

using System.Collections.ObjectModel;

namespace MackiesPhoneApp.Pages.User;

public partial class OrderPage : ContentPage
{
    TemplateScheduleDto _selectedLocation;
    private OrderBasket _orderBasketService;
    private ToolbarItem _totalToolbarItem;
    private OrderItem _selectedItem;

    private List<OrderItem> allOrderItems;
    private List<OrderItem> OrderItemsSortedByCategory;
    private List<OrderItem> OrderItemsSortedByProductType;


    public OrderPage(TemplateScheduleDto selectedLocation)
    {
        InitializeComponent();

        _selectedLocation = selectedLocation;

        _orderBasketService = ServiceHelper.GetService<OrderBasket>();

        OrderItemsSortedByCategory = new List<OrderItem>();    
        OrderItemsSortedByProductType = new List<OrderItem>();

        var grid = SetNavigationBarPageTitle.SetContentLogoAndTotal(this);
        NavigationPage.SetTitleView(this, grid);

        BindingContext = ServiceHelper.GetService<OrderBasket>();
        InitializeAsync();

    }

    private async void InitializeAsync()
    {
        var productList = await MackiesPhoneApp.Services.Products.getProducts();

        allOrderItems = MackiesPhoneApp.Services.Products.MakeProductItems(productList);

        _orderBasketService.UpdateAllProductsItems(allOrderItems);

        OrderItemsCollectionView.ItemsSource = allOrderItems;

        var categoryList = await MackiesPhoneApp.Services.Products.getProductCategories();
        _orderBasketService.ProductCategories = new ObservableCollection<ProductCategoryDto>(categoryList);

        var productTypeList = await MackiesPhoneApp.Services.Products.getProductTypes();
        _orderBasketService.ProductTypes = new ObservableCollection<ProductType>(productTypeList);

        var filterList = new List<string>();
        filterList.Add("Kategori");
        filterList.Add("Type");

        _orderBasketService.ProductFilters = new ObservableCollection<string>(filterList);


    }


    private async void OnOrderItemTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is OrderItem selectedOrderItem)
        {
            _selectedItem = selectedOrderItem;

            if (!_orderBasketService.IsProductInBasket( selectedOrderItem.productid))
            {
                foreach (var orderItem in allOrderItems)
                {
                    orderItem.IsQuantityEditable = false;
                }
                selectedOrderItem.IsQuantityEditable = true;
                selectedOrderItem.IsQuantityVisible = true;
            }
            else
            {
                Navigation.PushAsync(new OrderBasketPage());
            }
        }
    }

    private void OnImagePlusTapped(object? sender, EventArgs e)
    {
        if (sender is Image img && img.BindingContext is OrderItem orderItem)
        {
            orderItem.quantity++;
        }
    }

    private void OnAddToBasketClicked(object sender, EventArgs e)
    {
        _orderBasketService.AddOrderItemToBasket(_selectedItem);
        _selectedItem.IsQuantityEditable = false;
        _selectedItem.IsQuantityVisible = true;
        _selectedItem.IsInBasket = true;
    }

    private void OnImageMinusTapped(object? sender, EventArgs e)
    {
        if (sender is Image img && img.BindingContext is OrderItem orderItem)
        {
            if (orderItem.quantity > 1)
            {
                orderItem.quantity--;
            }
            else
            {
                orderItem.quantity = 1;
            }
        }
    }

    private async void OnDetailsTapped(object sender, EventArgs e)
    {
        if (sender is Label detailLabel && detailLabel.BindingContext is OrderItem selectedOrderItem)
        {
            var popup = new PopupDetailPage(selectedOrderItem);
            var result = await this.ShowPopupAsync(popup);
        }

    }

    private void OnCategorySelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
           //  var selectedCategoryList = e.CurrentSelection as List<ProductCategoryDto>;
            var tmpList = new List<OrderItem>();

            foreach (var item in allOrderItems)
            {
                if (item.productcategories != null)
                {
                    foreach (var category in item.productcategories)
                    {
                        foreach (var selectedObject in e.CurrentSelection)
                        {
                            var selectedCategory = selectedObject as ProductCategoryDto;
                            if (selectedCategory != null)
                            {
                                if (category.Id == selectedCategory.Id)
                                {
                                    if (!tmpList.Any(c => c.productid == item.productid) )
                                    {
                                        tmpList.Add(item);
                                    }
                                    
                                }
                                
                            }
                        }
                      
                    }
                }
            }

             OrderItemsSortedByCategory = tmpList;

        }

        else
        {
            OrderItemsSortedByCategory = new List<OrderItem>();
        }

       
        MakeResultOrderItemList();
    }

    //private void OnFilterSelected(object sender, SelectionChangedEventArgs e)
    //{
    //    if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
    //    {
    //        var selectedFilters = e.CurrentSelection;
    //        if (selectedFilters != null)
    //        {
    //            _orderBasketService.ShowProductCategories = false;
    //            _orderBasketService.ShowProductTypes = false;

    //            foreach (var filter in selectedFilters )
    //            {
    //                if (filter is string)
    //                {
    //                    if ( filter == "Kategori")
    //                    {
    //                        _orderBasketService.ShowProductCategories = !_orderBasketService.ShowProductCategories;
    //                    }

    //                    if ( filter == "Type")
    //                    {
    //                        _orderBasketService.ShowProductTypes = !_orderBasketService.ShowProductTypes;
    //                    }
    //                }
    //            }
               
    //        }
    //    }
    //    else
    //    {
    //        _orderBasketService.ShowProductCategories = false;
    //        _orderBasketService.ShowProductTypes = false;
    //    }
    //}


    private void OnTypeSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            var tmpList = new List<OrderItem>();

            foreach (var item in allOrderItems)
            {
                if (item.producttypes != null)
                {
                    foreach (var productType in item.producttypes)
                    {
                        foreach (var selectedObject in e.CurrentSelection)
                        {
                            var selectedProductType = selectedObject as ProductType;
                            if (selectedProductType != null)
                            {
                                if (productType.Id == selectedProductType.Id)
                                {
                                    if (!tmpList.Any(c => c.productid == item.productid))
                                    {
                                        tmpList.Add(item);
                                    }

                                }

                            }
                        }

                    }
                }
            }
            OrderItemsSortedByProductType = tmpList;
            
        }
        else
        {
            OrderItemsSortedByProductType = new List<OrderItem>();

        }

        MakeResultOrderItemList();
    }
    private void OnToggleShowFilters(object sender, EventArgs e)
    {
        _orderBasketService.ShowFilters = !_orderBasketService.ShowFilters; 
    }

    private void MakeResultOrderItemList()
    {
        if (OrderItemsSortedByCategory.Count > 0 && OrderItemsSortedByProductType.Count > 0)
        {
            var intersectList = OrderItemsSortedByCategory.Intersect(OrderItemsSortedByProductType).ToList();
            OrderItemsCollectionView.ItemsSource = intersectList;
        }
        else if (OrderItemsSortedByCategory.Count > 0)
        {
            OrderItemsCollectionView.ItemsSource = OrderItemsSortedByCategory;
        }
        else if (OrderItemsSortedByProductType.Count > 0)
        {
            OrderItemsCollectionView.ItemsSource = OrderItemsSortedByProductType;
        }
        else
        {
            OrderItemsCollectionView.ItemsSource = allOrderItems;
        }
    }

    private void OnRowTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is ProductCategoryDto category)
        {
            // Toggle the selection
            category.IsSelected = !category.IsSelected;
        }
    }

}

    
