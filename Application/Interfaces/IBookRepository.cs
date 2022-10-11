using Domain.Models.DTO;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Wrapper;

namespace Application.Interfaces
{
    public interface IBookRepository
    {

        /// <summary>
        /// xem story bang bookid
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Task<Storage> GetStorageByBookId(int bookId);
        
        
        /// <summary>
        /// liệt kê danh sách Book
        /// </summary>
        /// <returns></returns>
        Task<List<BookInformation>> GetBooks();


        /// <summary>
        /// thêm sách
        /// </summary>
        /// <param name="book"> input</param>
        /// <param name="quantity">input quantity</param>
        /// <returns></returns>
        Task<BookInformation> AddBook(Book book, int quantity);


        /// <summary>
        /// tìm sách theo tên
        /// </summary>
        /// <param name="bookName">tên sách</param>
        /// <returns></returns>
        int FindBookByName(string bookName);


        /// <summary>
        /// thêm số lượng sách trong kho
        /// </summary>
        /// <param name="bookName">tên sách</param>
        /// <param name="quantity">số lượng sách cần thêm</param>
        /// <returns></returns>
        Task<BookInformation> AddQuantity(string bookName, int quantity);

        /*int FindBookByAuthor(string author);*/
        /// <summary>
        /// tìm sách theo tên tác giả
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        Task<IResult<Book>> GetBookByAuthor(string author);
    }
}
