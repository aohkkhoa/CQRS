using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderDetailRepository
    {
        public Task<int> AddOrderDetail(OrderDetail orderDetal);
        public Task<int> DeleteOrderDetailById(int orderDeteailId);
        Task<OrderDetail> UpdateQuantity(int orderDetailId, int quantity);
    }
}
