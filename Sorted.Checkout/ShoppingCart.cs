using Sorted.IDal;
using System.Collections.Generic;
using System.Linq;

namespace Sorted.Checkout
{
    public class ShoppingCart
    {
        private readonly IProductRepository productRepository;
 
        private List<ShoppingListItem> shoppingList = new List<ShoppingListItem>();

        public ShoppingCart (IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void Add (string sku)
        {
            var product = productRepository.GetBySku(sku);
            if (product != null)
            {
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
            }
        }

        public decimal TotalPrice { get { return shoppingList.Sum(x => x.Product.Price); } }
        public decimal TotalItems { get { return shoppingList.Count(); } }
    }
}
