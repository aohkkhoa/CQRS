using Application.Interfaces;
using Domain.Models.Entities;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}