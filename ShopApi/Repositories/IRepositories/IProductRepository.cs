using System.Linq.Expressions;

namespace ShopApi.Repositories.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddRangeAsync(List<Product> products);
    }
}
