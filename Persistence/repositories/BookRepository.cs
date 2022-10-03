using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<int> AddBook(Book book, int quantity)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            _context.Storages.Add(new Storage() { BookId =book.Id, Quantity = quantity });
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task<string> AddQuantity(string bookName, int quantity)
        {
            var book = _context.Books.Where(x => x.Title == bookName ).FirstOrDefault();
            var storage = _context.Storages.Where(x => x.BookId == book.Id).FirstOrDefault();
            storage.Quantity = storage.Quantity + quantity;
            await _context.SaveChanges();
            return "just update quantity";
        }

        public int FindBookByName(string bookName)
        {

            var book = _context.Books.Where(a => a.Title==bookName).ToList();
            if(book.Count()!=0) return 0;
            return 1 ;
        } 

        public async Task<List<BookInformation>> GetBooks()
        {
            var books = (from b in _context.Books
                        join c in _context.Categories on b.CategoryId equals c.CategoryId
                         orderby b.Id descending
                         select new BookInformation()
                        {
                            BookId = b.Id,
                            Category = c.CategoryName,
                            Title = b.Title
                        }).ToList();
            //var productList =  await _context.Books.ToListAsync();
            return books;
        }
        /*public Async List<Book> GetBooks()
{
   var productList = await _context.Books.ToListAsync();
   return  await _context.Books.ToList();
}*/
    }
}
