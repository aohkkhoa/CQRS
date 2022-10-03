using Application.Interfaces;
using Domain.Models.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public int DeleteCategory(int categoryId)
        {
            var books = _context.Books.Where(x => x.CategoryId == categoryId).ToList();
            if(books.Count != 0)
            {
                return 1;
            }
            var category = _context.Categories.Where(x => x.CategoryId == categoryId).FirstOrDefault();
            if (category==null)
            {
                return -2;
            }
            _context.Remove(category);
            return 0;
        }

        public  IEnumerable<Category> GetAllCategories()
        {
            var categories =  _context.Categories;
            return categories;
        }
    }
}
