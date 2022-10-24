using Domain.Models.DTO;
using Domain.Models.Entities;
using Shared.Wrapper;

namespace Application.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        /// <summary>
        /// add order
        /// </summary>
        /// <param name="order"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<Result<Order>> AddOrder(Order order, int quantity);

        /// <summary>
        /// lấy order cần thanh toán (check paid = 0)
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderInformation>> GetAllOrderWillPay(int customerId);
    }
}