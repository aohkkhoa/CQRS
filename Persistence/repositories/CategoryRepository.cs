﻿using Application.Exceptions;
using Application.Interfaces;
using Domain.Models.Entities;
using Persistence.Context;
using Shared.Wrapper;

namespace Persistence.repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<Result<int>> DeleteCategory(int categoryId)
        {
            var books = _context.Books.Where(x => x.CategoryId == categoryId).ToList();
            if(books.Count != 0)
            {
                return await Result<int>.FailAsync("msg1");
            }
            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category==null)
            {
                return await Result<int>.FailAsync();
            }
            _context.Remove(category);
            await _context.SaveChanges();
            return await Result<int>.SuccessAsync();  
        }
        /*public int DeleteCategory(int categoryId)
        {
            var books = _context.Books.Where(x => x.CategoryId == categoryId).ToList();
            if(books.Count != 0)
            {
                return 1;
            }
            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category==null)
            {
                return -2;
            }
            _context.Remove(category);
            return 0;
        }*/

        public  IEnumerable<Category> GetAllCategories()
        {
            var categories =  _context.Categories;
            if (!categories.Any())
            {
                throw new ApiException("category null");
            }
            return categories;
        }
    }
}
