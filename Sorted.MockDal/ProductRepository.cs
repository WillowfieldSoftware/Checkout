using Sorted.IDal;
using Sorted.IDal.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sorted.MockDal
{
    // This code could possibly be run as a seperate project running on a different machine to 
    // the calling program, therefore I would nornally write these as asyncronous method
    public class ProductRepository : IProductRepository
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public Product GetBySku(string sku)
        {
            return Products.FirstOrDefault(x => x.SKU == sku);
        }
    }
}
