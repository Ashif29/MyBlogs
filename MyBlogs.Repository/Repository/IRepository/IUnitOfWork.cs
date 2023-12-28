using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogs.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IMyBlogRepository MyBlog {  get; }
        void Save();
    }
}