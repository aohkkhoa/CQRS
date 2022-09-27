using Application.Interfaces;
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

        public async Task<List<Book>> GetBooks()
        {
            var productList =  await _context.Books.ToListAsync();
            return productList;
        }
        /*public Async List<Book> GetBooks()
{
   var productList = await _context.Books.ToListAsync();
   return  await _context.Books.ToList();
}*/
    }
}
