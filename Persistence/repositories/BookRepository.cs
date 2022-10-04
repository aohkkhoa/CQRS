using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using Persistence.Context;

namespace Persistence.repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<BookInformation> AddBook(Book book, int quantity)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            var category = _context.Categories.Where(x => x.CategoryId == book.CategoryId).FirstOrDefault();
            _context.Storages.Add(new Storage() { BookId =book.Id, Quantity = quantity });
            await _context.SaveChangesAsync();
            var storage = _context.Storages.Where(x => x.BookId == book.Id).FirstOrDefault();
            BookInformation bookInfo = new BookInformation()
            {
                BookId = book.Id,
                Category = category.CategoryName,
                Title = book.Title,
                Quantity = storage.Quantity
            };
            return bookInfo;
        }

        public async Task<BookInformation> AddQuantity(string bookName, int quantity)
        {
            var book = _context.Books.Where(x => x.Title == bookName).FirstOrDefault();
            var category = _context.Categories.Where(x => x.CategoryId == book.CategoryId).FirstOrDefault();
            var storage = _context.Storages.Where(x => x.BookId == book.Id).FirstOrDefault();
            storage.Quantity = storage.Quantity + quantity;
            await _context.SaveChanges();
            BookInformation bookInfo = new BookInformation()
            {
                BookId = book.Id,
                Category = category.CategoryName,
                Title = book.Title,
                Quantity = storage.Quantity
            };
            return bookInfo;
        }

        public int FindBookByName(string bookName)
        {

            var book = _context.Books.Where(a => a.Title==bookName).ToList();
            if (book.Count()!=0) return 0;
            return 1;
        }

        public Task<List<BookInformation>> GetBooks()
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
            return Task.FromResult(books);
        }
        /*public Async List<Book> GetBooks()
{
   var productList = await _context.Books.ToListAsync();
   return  await _context.Books.ToList();
}*/
    }
}
