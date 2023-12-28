using MyBlogs.Core;
using MyBlogs.Data.DataAccess;
using MyBlogs.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogs.Data.Repository
{
    public class MyBlogRepository : Repository<MyBlog>, IMyBlogRepository
    {
        private ApplicationDbContext _db;
        public MyBlogRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(MyBlog obj)
        {
            _db.MyBlogs.Update(obj);
        }
    }

}
