using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.IRepositories
{
    public interface IProductRepository:IGenericRepository<Products>
    {

       public Task< IEnumerable<Products>> GetAllProductsByCategoryId(int categoryId);
        public Task Create(Products model);

    }
}
