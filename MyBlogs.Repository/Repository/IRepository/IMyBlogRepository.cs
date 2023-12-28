
using MyBlogs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogs.Data.Repository.IRepository
{
    public interface IMyBlogRepository : IRepository<MyBlog>
    {
        void Update(MyBlog obj);
    }
}
