using Domain.Models.DTO;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<BookInformation>> GetBooks();
        public Task<BookInformation> AddBook(Book book, int quantity);
        public int FindBookByName(string bookName);
        public Task<BookInformation> AddQuantity(string BookName, int quantity);
    }
}
