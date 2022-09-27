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
        public Task<int> AddOrder(Order order);
        public float getPriceByBookId(int bookId);
        public Task<List<OrderInformation>> getAllOrderInformation(string cusName);
        Task<List<OrderInformation>> getAllOrderWillPay(string cusName);
        public float getTotalPrice(List<OrderInformation> orderInformation);
        public Task<int> paid(List<OrderInformation> orderInformation);
    }
}
