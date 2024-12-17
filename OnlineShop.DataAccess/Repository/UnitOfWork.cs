﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.DataAccess.Data;
using OnlineShop.DataAccess.Repository.IRepository;

namespace OnlineShop.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository categoryRepository { get; private set; }
        public IProductRepository productRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            categoryRepository = new CategoryRepository(_db);
            productRepository = new ProductRepository(_db);
        }
              
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
