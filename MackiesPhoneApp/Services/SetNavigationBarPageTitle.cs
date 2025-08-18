using MackiesPhoneApp.Pages.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MackiesPhoneApp.Services
{
    public static class SetNavigationBarPageTitle
    {
        public static View SetContentLogoAndTotal(Page page)
        {
            var grid = new Grid
            {
                ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto }, // logo
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // spacer
                        new ColumnDefinition { Width = GridLength.Auto } , // total
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // spacer
                        new ColumnDefinition { Width = GridLength.Auto }  // Basket Icon button
                    },
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var logo = new Image
            {
                Source = "jjfisk_logo.png",
                HeightRequest = 60,
                VerticalOptions = LayoutOptions.Center
            };

            var orderBasketService = ServiceHelper.GetService<OrderBasket>();

            var totalLabel = new Label
            {
                Text = "order totalis",
                FontSize = 18,
                TextColor = Color.FromArgb("#ffac2c"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            };

            totalLabel.SetBinding(Label.TextProperty, new Binding("OrderTotal", stringFormat: "Total: {0:C}"));
            totalLabel.BindingContext = orderBasketService;


            var goToBasketButton = new ImageButton
            {
                Margin = 10,
                Source = "shopping.png",    // your cart icon (svg/png in Resources/Images)
                BackgroundColor = Colors.Transparent,
                HeightRequest = 20,
                WidthRequest = 20,
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.Center,                
            };

            goToBasketButton.Clicked += (s, e) =>
            {
                // e.g. navigate to basket page
                page.Navigation.PushAsync(new OrderBasketPage());
            };


            grid.Children.Add(logo);
            Grid.SetColumn(logo, 0);

            grid.Children.Add(totalLabel);
            Grid.SetColumn(totalLabel, 2);

            grid.Children.Add(goToBasketButton);
            Grid.SetColumn(goToBasketButton, 4);

            return grid;
        }


        public static Grid setContentLogo()
        {
            var grid = new Grid
            {
                ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto }, // logo
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // spacer
                        new ColumnDefinition { Width = GridLength.Auto }  // total
                    },
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var logo = new Image
            {
                Source = "jjfisk_logo.png",
                HeightRequest = 60,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,                
            };
            
            grid.Children.Add(logo);
            Grid.SetColumn(logo, 0);
            return grid;
        }

        public static Grid setContentTotal()
        {
            var grid = new Grid
            {
                ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto }, // logo
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // spacer
                        new ColumnDefinition { Width = GridLength.Auto }  // total
                    },
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var orderBasketService = ServiceHelper.GetService<OrderBasket>();

            var totalLabel = new Label
            {
                Text = $"Total: {orderBasketService.OrderTotal:C}",
                FontSize = 18,
                TextColor = Color.FromArgb("#ffac2c"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            };

            totalLabel.SetBinding(Label.TextProperty, new Binding("OrderTotal", stringFormat: "Total: {0:C}"));
            totalLabel.BindingContext = orderBasketService;

            grid.Children.Add(totalLabel);
            Grid.SetColumn(totalLabel, 2);
            return grid;

        }
    }
}
