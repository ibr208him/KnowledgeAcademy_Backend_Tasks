﻿using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.IRepositories
{
    public interface ICategoryRepository
    {
        public Task<Categories> GetCategoryByCategoryId(int categoryId);
    }
}