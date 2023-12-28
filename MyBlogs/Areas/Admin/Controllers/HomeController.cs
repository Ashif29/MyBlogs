using Microsoft.AspNetCore.Mvc;
using MyBlogs.Core;
using MyBlogs.Data.Repository.IRepository;
using MyBlogs.Models;
using System.Diagnostics;

namespace MyBlogs.Areas.Admin.Controllers
{
    [Area("Admin")]
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
