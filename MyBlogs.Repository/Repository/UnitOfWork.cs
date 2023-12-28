using MyBlogs.Data.DataAccess;
using MyBlogs.Data.Repository;
using MyBlogs.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogs.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IMyBlogRepository MyBlog { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            MyBlog = new MyBlogRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
