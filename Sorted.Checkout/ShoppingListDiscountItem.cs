using System.Collections.Generic;

namespace Sorted.Checkout
{
    public class ShoppingListDiscountItem
    {
        public List<ShoppingListItem> ShoppingListItems { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
