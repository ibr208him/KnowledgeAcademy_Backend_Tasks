using Ecommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()// make composite primary key
            .HasKey(e =>new{
                e.Id,
                e.OrderId,
                e.ProductId
            });
            modelBuilder.Entity<Categories>().HasData(
                
                new Categories() { Id=1,Name="Electronics",Description="this is electronics"},
                  new Categories() { Id = 2, Name = "Books", Description = "this is Books" },
                   new Categories() { Id = 3, Name = "Clothing", Description = "this is Clothing" }
                );

            modelBuilder.Entity<LocalUser>().HasData(

         new LocalUser() { Id = 1, Name = "Yaman", UserName = "Yaman@gmail.com", Password="12345", Phone ="0599111222",Role="Admin"},
         new LocalUser() { Id = 2, Name = "Kenan", UserName = "Kenan@gmail.com", Password = "12345", Phone = "0599555999", Role = "User" }

         );
            modelBuilder.Entity<Products>().HasData(
            new Products() { Id = 1, Name = "SmartPhone", Price = 299.99m, Image = "SmartPhone.jpg", CategoryId=1 },
            new Products() { Id = 2, Name = "Laptop", Price = 799.99m, Image = "Laptop.jpg", CategoryId = 1 },
            new Products() { Id = 3, Name = "Novel", Price = 19.99m, Image = "Novel.jpg", CategoryId = 2 },
            new Products() { Id = 4, Name = "T-Shirt", Price = 9.99m, Image = "T-Shirt.jpg", CategoryId = 3 },
         new Products() { Id =5, Name = "Jeans", Price = 49.99m, Image = "Jeans.jpg", CategoryId = 3 }
         );


            modelBuilder.Entity<Orders>().HasData(
                      new Orders() { Id = 1, OrderStatus = "Pending", OrderDate = new DateTime(2023, 12, 11), LocalUserId = 1 },
                       new Orders() { Id = 2, OrderStatus = "Completed", OrderDate = new DateTime(2023, 12, 12), LocalUserId = 2 },
                        new Orders() { Id = 3, OrderStatus = "Shipped", OrderDate = new DateTime(2023, 12, 13), LocalUserId = 1 }

                      );
                 modelBuilder.Entity<OrderDetails>().HasData(
                   new OrderDetails() { Id = 1, OrderId = 1, ProductId =1,Price=299.99m,Quantity=1 },
                   new OrderDetails() { Id = 2, OrderId = 1, ProductId = 4, Price = 9.99m, Quantity = 2 },
                   new OrderDetails() { Id = 3, OrderId = 2, ProductId = 3, Price = 19.99m, Quantity = 1 },
                   new OrderDetails() { Id = 4, OrderId = 3, ProductId = 2, Price = 799.99m, Quantity = 1 },
                   new OrderDetails() { Id = 5, OrderId = 3, ProductId = 5, Price = 9.99m, Quantity = 1 }
                );

            base.OnModelCreating(modelBuilder );
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Categories> Categories { get; set; }

    }
}
