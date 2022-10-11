using Application.Exceptions;
using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using Persistence.Context;
using Shared.Wrapper;

namespace Persistence.repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepository;

        public OrderRepository(ApplicationDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        public async Task<Result<Order>> AddOrder(Order order, int quantity)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChanges();
                //var storage = _context.Storages.FirstOrDefault(a => a.BookId == order.BookId);
                var storage = _bookRepository.GetStorageByBookId(order.BookId);
                if (storage.Result.Quantity < quantity)
                {
                    throw new Exception();
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine("quantity error" + ex);
                return await Result<Order>.FailAsync("Quantity is not enough");
            }


            return await Result<Order>.SuccessAsync(order);
        }

        public Task<List<OrderInformation>> getAllOrderInformation(int customerId)
        {
            var order = (from o in _context.Orders
                join b in _context.Books on o.BookId equals b.Id
                join c in _context.Customers on o.CustomerId equals c.CustomerId
                join od in _context.OrderDetails on o.OrderId equals od.OrderId
                where c.CustomerId == customerId
                orderby od.OrderDetailId descending
                select new OrderInformation()
                {
                    OrderDetailId = od.OrderId,
                    BookName = b.Title,
                    BookId = b.Id,
                    CustomerName = c.CustomerName,
                    Price = b.Price,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    CheckPaid = od.CheckPaid
                }).ToList();
            return Task.FromResult(order);
        }

        public Task<List<OrderInformation>> getAllOrderWillPay(int customerId)
        {
            var order = (from o in _context.Orders
                join b in _context.Books on o.BookId equals b.Id
                join c in _context.Customers on o.CustomerId equals c.CustomerId
                join od in _context.OrderDetails on o.OrderId equals od.OrderId
                where c.CustomerId == customerId && od.CheckPaid == 0
                select new OrderInformation()
                {
                    OrderDetailId = od.OrderDetailId,
                    BookId = b.Id,
                    BookName = b.Title,
                    CustomerName = c.CustomerName,
                    Price = b.Price,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    CheckPaid = od.CheckPaid
                }).ToList();
            return Task.FromResult(order);
        }

        public List<OrderInformation> findOrderDetailByBookId(int bookId)
        {
            var order = (from o in _context.Orders
                join b in _context.Books on o.BookId equals b.Id
                join c in _context.Customers on o.CustomerId equals c.CustomerId
                join od in _context.OrderDetails on o.OrderId equals od.OrderId
                where c.CustomerName == "customerA" && od.CheckPaid == 0 && o.BookId == bookId
                select new OrderInformation()
                {
                    OrderDetailId = od.OrderDetailId,
                    BookId = b.Id,
                    BookName = b.Title,
                    CustomerName = c.CustomerName,
                    Price = b.Price,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    CheckPaid = od.CheckPaid
                }).ToList();
            return order;
            /*var orders = _context.Orders.Where(a => a.BookId == bookId).Select(a=>a.BookId).ToList();

            var orderDetail = _context.OrderDetails.Where(a => orders.Contains(a.OrderId) && a.CheckPaid == 0).ToList();

            return orderDetail;*/
        }

        private Book GetBookByOrder(int bookId)
        {
            var book = _context.Books.FirstOrDefault(c => c.Id == bookId);
            if (book == null) throw new ApiException("Book not found!");
            return book;
        }

        private OrderDetail GetOrderDetailById(int orderDetailId)
        {
            var orderDetail = _context.OrderDetails.FirstOrDefault(a => a.OrderDetailId == orderDetailId);
            if (orderDetail == null) throw new ApiException("orderDetail not found!");
            return orderDetail;
        }

        private Order GetOrderByOrderDetail(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(b => b.OrderId == orderId);
            if (order == null) throw new ApiException("Order Not Found!");
            return order;
        }

        public async Task<string> addQuantityOfOrderDetail(List<OrderInformation> orderDetailTest, int quantity)
        {
            foreach (var item in orderDetailTest)
            {
                var orderDetail = GetOrderDetailById(item.OrderDetailId);
                var order = GetOrderByOrderDetail(orderDetail.OrderId);
                var book = GetBookByOrder(order.BookId);
                //var storage = _bookRepository.GetStorageByBookId(book.Id);
                orderDetail.Quantity += quantity;
                orderDetail.UnitPrice = book.Price * orderDetail.Quantity;
            }

            await _context.SaveChanges();
            return "add complete";
        }

        public float getTotalPrice(List<OrderInformation> orderInformation)
        {
            float totalPrice = 0;
            foreach (var order in orderInformation)
            {
                totalPrice += order.UnitPrice;
            }

            return totalPrice;
        }

        public float getPriceByBookId(int bookId)
        {
            var a = (from b in _context.Books
                where b.Id == bookId
                select b.Price);

            // Console.WriteLine(a.First());
            return a.First();
        }

        public async Task<string> paid(List<OrderInformation> orderInformation)
        {
            foreach (var orderDetails in from item in orderInformation
                     let order = _context.OrderDetails.FirstOrDefault(a => a.OrderDetailId == item.OrderDetailId)
                     select order)
            {
                orderDetails.CheckPaid = 1;
                var order = GetOrderByOrderDetail(orderDetails.OrderId);
                var book = GetBookByOrder(order.BookId);
                var storage = _bookRepository.GetStorageByBookId(book.Id);
                storage.Result.Quantity -= orderDetails.Quantity;
            }

            await _context.SaveChanges();
            return "complete";
        }
    }
}