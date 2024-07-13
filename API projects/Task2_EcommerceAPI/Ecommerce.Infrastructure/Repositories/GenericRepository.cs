using Ecommerce.Core.Entities;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //public async Task Create(T model)
        //{
        
        //    await dbContext.Set<T>().AddAsync(model);
        //   //await dbContext.SaveChangesAsync();
        
        //}

        public void Delete(int id)
        {
            var model=dbContext.Set<T>().Find(id);
            dbContext.Set<T>().Remove(model);
            dbContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Products))
            {
                var model = await dbContext.Products.Include(x => x.Category).ToListAsync();
                return (IEnumerable<T>)model;
            }
            var models = await dbContext.Set<T>().ToListAsync();
            return models;

        }

        public async Task<T> GetById(int id)
        {
            var model = await dbContext.Set<T>().FindAsync(id);
            return model;
        }
 
    public void Update(T model)
        {
            dbContext.Set<T>().Update(model);
            dbContext.SaveChanges();
        }
  

    }
}
