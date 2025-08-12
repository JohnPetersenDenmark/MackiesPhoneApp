using MackiesPhoneApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MackiesPhoneApp.Services
{
    public static class Products
    {
        static List<OrderItem> allOrderItems = new List<OrderItem>();

        public static async Task<List<Pizza>> getPizzas()
        {
            var pizzaList = new List<Pizza>();
            var partialUrl = "/Home/pizzalist";
            var response = await MackiesPhoneApp.Services.CustomHttpClient.getRequest(partialUrl, false, null);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseJson = await response.Content.ReadAsStringAsync();

                 pizzaList = JsonConvert.DeserializeObject<List<Pizza>>(responseJson);
            }

         

            return pizzaList;
        }

        public static async Task<List<Topping>> getToppings()
        {
            var toppingList = new List<Topping>();
            var partialUrl = "/Home/toppinglist";
            var response = await MackiesPhoneApp.Services.CustomHttpClient.getRequest(partialUrl, false, null);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseJson = await response.Content.ReadAsStringAsync();

                 toppingList = JsonConvert.DeserializeObject<List<Topping>>(responseJson);
            }

            return toppingList;
        }

        public static List<OrderItem>  MakePizzaOrderItems(List<Pizza> pizzaList)
        {
            var pizzaOrderItemList = new List<OrderItem>();

            foreach (var pizza in pizzaList)
            {
                var orderItem = new OrderItem();
                orderItem.productname = pizza.Name;
                orderItem.productid = pizza.Id;
                orderItem.pizzanumber = pizza.PizzaNumber;
                orderItem.unitprice = pizza.Price;              
                orderItem.producttype = pizza.ProductType;
                orderItem.productdescription = pizza.Description;
                orderItem.imageurl = MackiesPhoneApp.Services.Constants.getApiBaseUrl() + pizza.ImageUrl;
                orderItem.discountedunitprice = pizza.DiscountPrice;
                orderItem.unitdiscountpercentage = pizza.DiscountPercentage;
                orderItem.quantity = 1;
                orderItem.selected = false;
                orderItem.orderid = 0;
                orderItem.IsQuantityVisible = false;
                             
                pizzaOrderItemList.Add(orderItem);
            }

            return pizzaOrderItemList;
        }

        public static List<OrderItem> MakeToppingOrderItems(List<Topping> toppingList)
        {
            var pizzaOrderItemList = new List<OrderItem>();

            foreach (var topping in toppingList)
            {
                var orderItem = new OrderItem();
                orderItem.productname = topping.Name;
                orderItem.productid = topping.Id;
                orderItem.pizzanumber = "";
                orderItem.imageurl = MackiesPhoneApp.Services.Constants.getApiBaseUrl() + topping.ImageUrl; 
                orderItem.unitprice = topping.Price;
                orderItem.producttype = topping.ProductType;
                orderItem.productdescription = topping.Description;
                orderItem.discountedunitprice = 0;
                orderItem.unitprice = topping.Price;
                orderItem.unitdiscountpercentage = 0;
                orderItem.quantity = 1;
                orderItem.IsQuantityVisible = false;
                orderItem.selected = false;
                orderItem.orderid = 0;
               
                pizzaOrderItemList.Add(orderItem);
            }

            return pizzaOrderItemList;
        }

        //public static List<OrderItem>  GetAllOrderItems()
        //{
        //    return allOrderItems;
        //}

        //public static void SetAllOrderItems(List<OrderItem> OrderItemsAll)
        //{
        //    allOrderItems = OrderItemsAll;
        //}

        
    }
}
