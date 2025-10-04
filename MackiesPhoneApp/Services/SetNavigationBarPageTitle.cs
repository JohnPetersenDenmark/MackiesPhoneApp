using MackiesPhoneApp.Pages.User;
using Microsoft.Maui.Controls.Shapes;
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
          //  new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // spacer
            new ColumnDefinition { Width = GridLength.Auto }, // total
            new ColumnDefinition { Width = GridLength.Auto }  // Basket (with badge)
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
                FontSize = 14,
                TextColor = Color.FromArgb("#ffffff"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            };
            totalLabel.SetBinding(Label.TextProperty, new Binding("OrderTotal", stringFormat: "Total: {0:C}"));
            totalLabel.BindingContext = orderBasketService;

            // --- Shopping bag with badge ---
            var basketIcon = new Image
            {
                Source = "shopping.png",
                HeightRequest = 30,
                WidthRequest = 30,
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.Center
            };

            var badgeLabel = new Label
            {
                TextColor = Colors.Black,
                BackgroundColor = Colors.White,
                FontSize = 12,
                WidthRequest = 20,
                HeightRequest = 20,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.End
            };

            // circular badge
            badgeLabel.Clip = new RoundRectangleGeometry(new CornerRadius(10), new Rect(0, 0, 20, 20));

            // bind number of items
            badgeLabel.SetBinding(Label.TextProperty, new Binding("NumberOfOrderItems"));
            badgeLabel.BindingContext = orderBasketService;

            // overlay badge on icon
            var basketGrid = new Grid
            {
                WidthRequest = 40,
                HeightRequest = 40
            };
            basketGrid.Children.Add(basketIcon);
            basketGrid.Children.Add(badgeLabel);

            // make basket clickable with tap gesture
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                page.Navigation.PushAsync(new OrderBasketPage());
            };
            basketGrid.GestureRecognizers.Add(tapGesture);

            // --- Add everything to main grid ---
            grid.Children.Add(logo);
            Grid.SetColumn(logo, 0);

            grid.Children.Add(totalLabel);
            Grid.SetColumn(totalLabel, 1);

            grid.Children.Add(basketGrid);
            Grid.SetColumn(basketGrid, 2);

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
                TextColor = Color.FromArgb("#ffffff"),
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
