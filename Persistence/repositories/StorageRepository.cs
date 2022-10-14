using Application.Exceptions;
using Application.Interfaces;
using Domain.Models.DTO;
using Persistence.Context;

namespace Persistence.repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepository;

        public StorageRepository(ApplicationDbContext context, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _context = context;
        }

        public IEnumerable<StorageUnit> GetAllStorage()
        {
            var storage = from s in _context.Storages
                join b in _context.Books on s.BookId equals b.Id
                select new StorageUnit()
                {
                    BookName = b.Title,
                    Quantity = s.Quantity,
                    StorageUnitId = s.StorageId
                };
            if (storage == null)
                throw new ApiException("Storage Not Found!");
            return storage;
        }
        /// <summary>
        /// hàm này ko sử dụng nữa
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public string HandleQuantityStorage(int bookId, int quantity)
        {
            var item = _bookRepository.GetStorageByBookId(bookId);
            if (item.Result.Quantity < quantity)
            {
                Console.WriteLine("notOk");
                return "notOk";
            }

            /*item.Quantity = item.Quantity-quantity;
            await _context.SaveChanges();*/
            Console.WriteLine("ok");
            return "Ok";
        }
    }
}