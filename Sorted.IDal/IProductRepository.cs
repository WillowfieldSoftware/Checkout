using Sorted.IDal.Models;

namespace Sorted.IDal
{
    public interface IProductRepository
    {
        Product GetBySku(string sku);
    }
}
