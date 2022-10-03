using Domain.Models.DTO;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderRepository
    {
        public Task<int> AddOrder(Order order, int quantity);
        public float getPriceByBookId(int bookId);
        public Task<List<OrderInformation>> getAllOrderInformation(string cusName);
        Task<List<OrderInformation>> getAllOrderWillPay(string cusName);
        public float getTotalPrice(List<OrderInformation> orderInformation);
        public Task<string> paid(List<OrderInformation> orderInformation);
        public List<OrderInformation> findOrderDetailByBookId(int bookId);
        public Task<string> addQuantityOfOrderDetail(List<OrderInformation> orderDetailTest, int quantity);
    }
}
