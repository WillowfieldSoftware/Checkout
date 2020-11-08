using Sorted.IDal;
using Sorted.IDal.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sorted.Checkout
{
    public class ShoppingCart
    {
        private readonly IProductRepository productRepository;
        private readonly IProductDiscountRepository productDiscountRepository;

        private readonly List<ShoppingListItem> shoppingList = new List<ShoppingListItem>();
        private List<ShoppingListDiscountItem> discountShoppingList;
        private List<ProductDiscount> productDiscounts;

        public ShoppingCart (IProductRepository productRepository, IProductDiscountRepository productDiscountRepository)
        {
            this.productRepository = productRepository;
            this.productDiscountRepository = productDiscountRepository;
        }

        public decimal TotalPrice { get { return discountShoppingList.Sum(x => x.Price); } }

        public decimal TotalItems { get { return shoppingList.Count(); } }

        public List<ShoppingListDiscountItem> Items 
        { 
            get 
            { 
                return discountShoppingList
                    .OrderBy(x => x.ShoppingListItems.Min(y => y.Id))
                    .ToList(); 
            } 
        }

        public void Add (string sku)
        {
            var product = productRepository.GetBySku(sku);
            if (product == null)
            {
                return;
            }

            // if product is first of its type in the shopping list, refresh the ProductDiscounts to include any for this new product
            // currently brings all, neeeds to pass array of distinct SKU's so as not to include discounts that are not needed
            if (!shoppingList.Any(x => x.Product.SKU == sku))
            {
                productDiscounts = productDiscountRepository.GetBySkus();
            }

            var id = 1;
            if (shoppingList.Count > 0)
            {
                id = shoppingList.Max(x => x.Id) + 1;
            }

            shoppingList.Add(new ShoppingListItem
            { 
                Id = id,
                Product = product
            });

            ApplyDiscounts();
        }

        private void ApplyDiscounts()
        {
            // copy shopping list;
            List<ShoppingListItem> tempShoppingList = new List<ShoppingListItem>(shoppingList);
            List<ShoppingListDiscountItem> tempDiscountList = new List<ShoppingListDiscountItem>();

            if (productDiscounts != null)
            {
                // iterate through discounts. 
                foreach (var item in productDiscounts)
                {
                    var found = true;
                    do
                    {
                        var discountProducts = new ShoppingListDiscountItem
                        {
                            Name = item.Name,
                            Price = item.Price,
                            ShoppingListItems = new List<ShoppingListItem>()
                        };

                        foreach (var key in item.SKUKeys)
                        {
                            var product = tempShoppingList.FirstOrDefault(x => 
                                x.Product.SKU == key
                                && !discountProducts.ShoppingListItems.Any(y => y.Id == x.Id));

                            if (product == null)
                            {
                                found = false;
                                break;
                            }

                            discountProducts.ShoppingListItems.Add(product);
                        }

                        if (found)
                        {
                            tempDiscountList.Add(discountProducts);

                            foreach (var product in discountProducts.ShoppingListItems)
                            {
                                tempShoppingList.Remove(product);
                            }
                        }

                    } while (found);
                };
            }

            // copy remainder into discountList
            foreach (var item in tempShoppingList)
            {
                var discountProducts = new ShoppingListDiscountItem
                {
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    ShoppingListItems = new List<ShoppingListItem>()
                };
                discountProducts.ShoppingListItems.Add(item);

                tempDiscountList.Add(discountProducts);
            }

            discountShoppingList = tempDiscountList;
        }
    }
}
