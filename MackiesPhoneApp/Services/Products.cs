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

        public static async Task<List<Product>> getProducts()
        {
            var productList = new List<Product>();
            var partialUrl = "/Home/productlist";
            var response = await MackiesPhoneApp.Services.CustomHttpClient.getRequest(partialUrl, false, null);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseJson = await response.Content.ReadAsStringAsync();

                productList = JsonConvert.DeserializeObject<List<Product>>(responseJson);
            }
         
            return productList;
        }

        public static async Task<List<ProductCategoryDto>> getProductCategories()
        {
            var categoryList = new List<ProductCategoryDto>();
            var partialUrl = "/Home/productcategorylist";
            var response = await MackiesPhoneApp.Services.CustomHttpClient.getRequest(partialUrl, false, null);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseJson = await response.Content.ReadAsStringAsync();

                categoryList = JsonConvert.DeserializeObject<List<ProductCategoryDto>>(responseJson);
            }

            return categoryList;
        }



        public static List<OrderItem>  MakeProductItems(List<Product> productList)
        {
            var productOrderItemList = new List<OrderItem>();

            foreach (var product in productList)
            {
                var orderItem = new OrderItem();
                orderItem.productname = product.Name;
                orderItem.productid = product.Id;
                orderItem.productnumber = product.ProductNumber;
                orderItem.unitprice = product.Price;              
                orderItem.producttype = product.ProductType;
                orderItem.productdescription = product.Description;
                orderItem.details = product.Details;
                orderItem.imageurl = MackiesPhoneApp.Services.Constants.getApiBaseUrl() + product.ImageUrl;
                orderItem.discountedunitprice = product.DiscountPrice;
                orderItem.unitdiscountpercentage = product.DiscountPercentage;
                orderItem.quantity = 1;
                orderItem.selected = false;
                orderItem.orderid = 0;
                orderItem.IsQuantityEditable = false;
                orderItem.IsQuantityVisible = false;
                orderItem.productcategories = product.ProductCategories;

                productOrderItemList.Add(orderItem);
            }

            return productOrderItemList;
        }                   
    }
}
