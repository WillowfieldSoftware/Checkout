using NUnit.Framework;
using Sorted.Checkout;
using Sorted.IDal.Models;
using Sorted.MockDal;
using System;
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

            var MockProductDiscountData = new ProductDiscountRepository
            {
                ProductDiscounts = new List<ProductDiscount>()
            };

            var cart = new ShoppingCart(MockProductData, MockProductDiscountData);
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

            var MockProductDiscountData = new ProductDiscountRepository
            {
                ProductDiscounts = new List<ProductDiscount>()
            };

            var cart = new ShoppingCart(MockProductData, MockProductDiscountData);
            cart.Add("sku1");
            cart.Add("sku2");
            cart.Add("sku3");
            cart.Add("sku2");

            Assert.AreEqual(cart.TotalItems, 4);
            Assert.AreEqual(cart.TotalPrice, price * 4);
        }

        [Test]
        public void AddMultipleItemsWithDiscount()
        {
            var price1 = 0.80M;
            var price2 = 0.40M;
            var price3 = 0.60M;
            var discountPrice1 = 0.70M;

            var MockProductData = new ProductRepository
            {
                Products = new List<Product>
                {
                    new Product { SKU = "sku1", Name = "Product1", Price = price1 },
                    new Product { SKU = "sku2", Name = "Product2", Price = price2 },
                    new Product { SKU = "sku3", Name = "Product3", Price = price3 }
                }
            };

            var MockProductDiscountData = new ProductDiscountRepository
            {
                ProductDiscounts = new List<ProductDiscount>
                {
                    new ProductDiscount
                    {
                        SKUKeys = new List<string> { "sku2", "sku2" },
                        Name = "Discount1",
                        Price = discountPrice1
                    }
                }
            };

            var cart = new ShoppingCart(MockProductData, MockProductDiscountData);
            cart.Add("sku1");
            cart.Add("sku2");
            cart.Add("sku3");
            cart.Add("sku2");

            Assert.AreEqual(cart.TotalItems, 4);
            Assert.AreEqual(cart.TotalPrice, price1 + price3 + discountPrice1);
        }

        [Test]
        public void AddMultipleDiscounts()
        {
            var price1 = 0.80M;
            var price2 = 0.40M;
            var price3 = 0.60M;
            var discountPrice1 = 0.70M;

            var MockProductData = new ProductRepository
            {
                Products = new List<Product>
                {
                    new Product { SKU = "sku1", Name = "Product1", Price = price1 },
                    new Product { SKU = "sku2", Name = "Product2", Price = price2 },
                    new Product { SKU = "sku3", Name = "Product3", Price = price3 }
                }
            };

            var MockProductDiscountData = new ProductDiscountRepository
            {
                ProductDiscounts = new List<ProductDiscount>
                {
                    new ProductDiscount
                    {
                        SKUKeys = new List<string> { "sku2", "sku2" },
                        Name = "Discount1",
                        Price = discountPrice1
                    }
                }
            };

            var cart = new ShoppingCart(MockProductData, MockProductDiscountData);
            cart.Add("sku1");
            cart.Add("sku2");
            cart.Add("sku3");
            cart.Add("sku2");
            cart.Add("sku2");
            cart.Add("sku2");

            Assert.AreEqual(cart.TotalItems, 6);
            Assert.AreEqual(cart.TotalPrice, price1 + price3 + (discountPrice1 * 2));
        }

        [Test]
        public void AddMultipleDifferentDiscounts()
        {
            var price1 = 0.80M;
            var price2 = 0.40M;
            var price3 = 0.60M;
            var discountPrice1 = 0.75M;
            var discountPrice2 = 0.70M;

            var MockProductData = new ProductRepository
            {
                Products = new List<Product>
                {
                    new Product { SKU = "sku1", Name = "Product1", Price = price1 },
                    new Product { SKU = "sku2", Name = "Product2", Price = price2 },
                    new Product { SKU = "sku3", Name = "Product3", Price = price3 }
                }
            };

            var MockProductDiscountData = new ProductDiscountRepository
            {
                ProductDiscounts = new List<ProductDiscount>
                {
                    new ProductDiscount
                    {
                        SKUKeys = new List<string> { "sku2", "sku3" },
                        Name = "Discount1",
                        Price = discountPrice1
                    },
                    new ProductDiscount
                    {
                        SKUKeys = new List<string> { "sku2", "sku2" },
                        Name = "Discount2",
                        Price = discountPrice2
                    }
                }
            };

            var cart = new ShoppingCart(MockProductData, MockProductDiscountData);
            cart.Add("sku1");
            cart.Add("sku2");
            cart.Add("sku3");
            cart.Add("sku2");
            cart.Add("sku2");
            cart.Add("sku2");

            foreach (var item in cart.Items)
            {
                Console.WriteLine($"{item.Name}  {item.Price}");
            }

            Assert.AreEqual(cart.TotalItems, 6);
            Assert.AreEqual(cart.TotalPrice, price1 + price2 + discountPrice1 + discountPrice2);
        }
    }
}