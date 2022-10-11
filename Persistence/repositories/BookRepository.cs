using Application.Exceptions;
using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using Persistence.Context;
using Shared.Wrapper;

namespace Persistence.repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookInformation> AddBook(Book book, int quantity)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            var category = GetCategoryById(book.CategoryId);
            var storage = new Storage()
            {
                BookId = book.Id,
                Quantity = quantity
            };
            _context.Storages.Add(storage);
            await _context.SaveChangesAsync();
            var storageOfBook = GetStorageByBookId(book.Id);
            var bookInfo = new BookInformation()
            {
                BookId = book.Id,
                Category = category.Result.CategoryName,
                Title = book.Title,
                Quantity = storageOfBook.Result.Quantity
            };
            return bookInfo;
        }

        public Task<Book> GetBookByName(string bookName)
        {
            var book = _context.Books.FirstOrDefault(x => x.Title == bookName);
            if (book == null)
                throw new ApiException("Book not found!");
            return Task.FromResult(book);
        }

        public Task<Category> GetCategoryById(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category == null)
                throw new ApiException("Category not found!");
            return Task.FromResult(category);
        }

        public Task<Storage> GetStorageByBookId(int bookId)
        {
            var storage = _context.Storages.FirstOrDefault(x => x.BookId == bookId);
            if (storage == null)
                throw new ApiException("Storage not found!");
            return Task.FromResult(storage);
        }

        public async Task<BookInformation> AddQuantity(string bookName, int quantity)
        {
            var book = GetBookByName(bookName);
            var category = GetCategoryById(book.Result.CategoryId);
            var storage = GetStorageByBookId(book.Result.Id);
            storage.Result.Quantity = storage.Result.Quantity + quantity;
            await _context.SaveChanges();
            var bookInfo = new BookInformation()
            {
                BookId = book.Id,
                Category = category.Result.CategoryName,
                Title = book.Result.Title,
                Quantity = storage.Result.Quantity,
                Author = book.Result.Author
            };
            return bookInfo;
        }
        public async Task<IResult<Book>> GetBookByAuthor(string author)
        {
            var book = _context.Books.FirstOrDefault(a => a.Author == author);
            if (book != null)
                return await Result<Book>.SuccessAsync(book);
            return await Result<Book>.FailAsync();
        }
        /*public int FindBookByAuthor(string author)
        {
            var book = GetBookByAuthor(author);
            
            return -1;
        }*/

        public int FindBookByName(string bookName)
        {
            var book = _context.Books.Where(a => a.Title == bookName).ToList();
            return book.Count != 0 ? 0 : 1;
        }

        public Task<List<BookInformation>> GetBooks()
        {
            var books = (from b in _context.Books
                join c in _context.Categories on b.CategoryId equals c.CategoryId
                join s in _context.Storages on b.Id equals s.BookId
                orderby b.Id descending
                select new BookInformation()
                {
                    BookId = b.Id,
                    Category = c.CategoryName,
                    Title = b.Title,
                    Author = b.Author,
                    Quantity = s.Quantity
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