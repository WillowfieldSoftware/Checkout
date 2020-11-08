using Sorted.IDal;
using Sorted.IDal.Models;
using System.Collections.Generic;

namespace Sorted.MockDal
{
    // My design is to return a list of ProductDiscount sorted by best discount first
    // I will then iterate over the discounts looking for matches in the Shopping list
    // Assumpions: 
    //      The list is created and sorted by a sub system not part of this test
    //      All discounts are better value than the Product.Price


    public class ProductDiscountRepository : IProductDiscountRepository
    {
        public List<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();

        // Return all to start with. 
        // TODO : only get discounts for items in Shopping List 
        public List<ProductDiscount> GetBySkus()
        {
            return ProductDiscounts;
        }
    }
}
