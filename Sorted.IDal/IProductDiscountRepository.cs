using Sorted.IDal.Models;
using System.Collections.Generic;

namespace Sorted.IDal
{
    public interface IProductDiscountRepository
    {
        List<ProductDiscount> GetBySkus();
    }
}
