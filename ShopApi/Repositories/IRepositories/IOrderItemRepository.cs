using System.Linq.Expressions;

namespace ShopApi.Repositories.IRepositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task AddRangeAsync(List<OrderItem> orderItems);
    }
}
