using System.Collections.Generic;

namespace Sorted.IDal.Models
{
    // My design is to have a list of Product SKU as a keys, if all defined are found in the Shopping list, the discount gets applied
    // example
    //      2 of product "sku1" would have a SKUKey of ("sku1","sku1")
    //      1 of product "sku1" and 1 of "sku2" would have a SKUKey of ("sku1","sku2") 
    // This should cover single item discounts and a combination of items 


    public class ProductDiscount
    {
        public List<string> SKUKeys { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
