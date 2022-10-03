﻿using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        public int DeleteCategory(int categoryId);
        public IEnumerable<Category> GetAllCategories();
    }
}
