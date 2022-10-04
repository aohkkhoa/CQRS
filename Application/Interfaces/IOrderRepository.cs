using Domain.Models.DTO;
using Domain.Models.Entities;

namespace Application.Interfaces
{
    public interface IOrderRepository
    {

        /// <summary>
        /// thêm order
        /// </summary>
        /// <param name="order"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<int> AddOrder(Order order, int quantity);

        /// <summary>
        /// lấy sách by id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        float getPriceByBookId(int bookId);


        /// <summary>
        /// lấy danh thông tin sách có category
        /// </summary>
        /// <param name="cusName"></param>
        /// <returns></returns>
        Task<List<OrderInformation>> getAllOrderInformation(string cusName);

        /// <summary>
        /// lấy order cần thanh toán (check paid = 0)
        /// </summary>
        /// <param name="cusName">order của khách nào</param>
        /// <returns></returns>
        Task<List<OrderInformation>> getAllOrderWillPay(string cusName);

        /// <summary>
        /// tính tổng tiền cần thanh toán của khách
        /// </summary>
        /// <param name="orderInformation"></param>
        /// <returns></returns>
        float getTotalPrice(List<OrderInformation> orderInformation);


        /// <summary>
        /// thanh toán (checkpaid = 1)
        /// </summary>
        /// <param name="orderInformation"></param>
        /// <returns></returns>
        Task<string> paid(List<OrderInformation> orderInformation);

        /// <summary>
        /// tìm sách trong order để thêm số lượng nếu trùng tên sách trong order
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        List<OrderInformation> findOrderDetailByBookId(int bookId);

        /// <summary>
        /// thêm số lượng nếu trùng tên sách trong order
        /// </summary>
        /// <param name="orderDetailTest"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<string> addQuantityOfOrderDetail(List<OrderInformation> orderDetailTest, int quantity);
    }
}
