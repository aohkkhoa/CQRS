using Domain.Models.Entities;

namespace Application.Interfaces
{
    public interface IOrderDetailRepository
    {

        /// <summary>
        /// thêm thông tin order (quantity)
        /// </summary>
        /// <param name="orderDetal"></param>
        /// <returns></returns>
        Task<int> AddOrderDetail(OrderDetail orderDetal);

        /// <summary>
        /// xóa order detail 
        /// </summary>
        /// <param name="orderDeteailId"></param>
        /// <returns></returns>
        Task<int> DeleteOrderDetailById(int orderDeteailId);

        /// <summary>
        /// thêm số lượng vào sách của orderdetail
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<OrderDetail> UpdateQuantity(int orderDetailId, int quantity);
    }
}
