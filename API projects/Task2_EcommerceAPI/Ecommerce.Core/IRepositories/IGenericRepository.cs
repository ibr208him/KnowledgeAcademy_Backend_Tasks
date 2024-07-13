using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.IRepositories
{
    public interface IGenericRepository<T> where T :class  // template can be products,categories,orders,..etc
    {
        public Task <IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
       // public Task Create(T model);
        public void Update(T model); // update and delete only work in sync way
        public void Delete(int id);// update and delete only work in sync way
    }
}
