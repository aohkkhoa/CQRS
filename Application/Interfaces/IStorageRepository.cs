using Domain.Models.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStorageRepository
    {
        public string HandleQuantityStorage(int bookId, int quantity);
        public IEnumerable<StorageUnit> GetAllStorage();
    }
}
