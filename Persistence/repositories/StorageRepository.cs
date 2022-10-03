using Application.Interfaces;
using Domain.Models.DTO;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly ApplicationDbContext _context;

        public StorageRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public IEnumerable<StorageUnit> GetAllStorage()
        {
            var storage = from s in _context.Storages
                          join b in _context.Books on s.BookId equals b.Id
                          select new StorageUnit()
                          {
                              BookName = b.Title,
                              Quantity=s.Quantity,
                              StorageUnitId=s.StorageId
                          };
            if (storage != null)
                return storage;
            return null;
        }

        public string HandleQuantityStorage(int bookId, int quantity)
        {
            var item = _context.Storages.Where(a => a.BookId == bookId).FirstOrDefault();
            if (item.Quantity < quantity) { Console.WriteLine("notok"); return "NotOk"; }
            /*item.Quantity = item.Quantity-quantity;
            await _context.SaveChanges();*/
            Console.WriteLine("ok");
            return "Ok";
        }
    }
}
