using Microsoft.AspNetCore.Mvc;
using MyBlogs.Core;
using MyBlogs.Data.Repository.IRepository;
using MyBlogs.Models;
using System.Diagnostics;

namespace MyBlogs.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<MyBlog> objMyBlogList = _unitOfWork.MyBlog.GetAll().ToList();
            return View(objMyBlogList);
        }
        public IActionResult Details(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            MyBlog? myblogFromDb = _unitOfWork.MyBlog.Get(u => u.Id == id);
            if (myblogFromDb == null)
            {
                return NotFound();
            }
            return View(myblogFromDb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
