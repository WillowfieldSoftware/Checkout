using NUnit.Framework;
using Sorted.Checkout;
using Sorted.IDal.Models;
using Sorted.MockDal;
using System.Collections.Generic;

namespace Checkout.Test
{
    public class Tests
    {
        [Test]
        public void AddSingleItemToCart()
        {
            var sku = "sku1";
            var price = 0.30M;

            var MockProductData = new ProductRepository
            {
                Products = new List<Product>
                {
                    new Product { SKU = sku, Name = "Product", Price = price }
                }
            };

            var cart = new ShoppingCart(MockProductData);
            cart.Add(sku);

            Assert.AreEqual(cart.TotalItems, 1);
            Assert.AreEqual(cart.TotalPrice, price);
        }

        [Test]
        public void AddMultipleItemsToCart()
        {
            var price = 0.30M;

            var MockProductData = new ProductRepository
            {
                Products = new List<Product>
                {
                    new Product { SKU = "sku1", Name = "Product1", Price = price },
                    new Product { SKU = "sku2", Name = "Product2", Price = price },
                    new Product { SKU = "sku3", Name = "Product3", Price = price }
                }
            };

            var cart = new ShoppingCart(MockProductData);
            cart.Add("sku1");
            cart.Add("sku2");
            cart.Add("sku3");
            cart.Add("sku2");

            Assert.AreEqual(cart.TotalItems, 4);
            Assert.AreEqual(cart.TotalPrice, price * 4);
        }
    }
}