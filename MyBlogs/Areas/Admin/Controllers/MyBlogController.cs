using Microsoft.AspNetCore.Mvc;
using MyBlogs.Core;
using MyBlogs.Data.DataAccess;
using MyBlogs.Data.Repository.IRepository;

namespace MyBlogs.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MyBlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MyBlogController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<MyBlog> objMyBlogList = _unitOfWork.MyBlog.GetAll().ToList();
            return View(objMyBlogList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MyBlog obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string myblogPath = Path.Combine(wwwRootPath, @"images\myblog");

                    using (var fileStream = new FileStream(Path.Combine(myblogPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.imgUrl = @"\images\myblog\" + fileName;
                    
                }
                _unitOfWork.MyBlog.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Blog Created Successfully";
                return RedirectToAction("Index", "MyBlog");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
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

        [HttpPost]
        public IActionResult Edit(MyBlog obj, IFormFile? file)
        {
            
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string myblogPath = Path.Combine(wwwRootPath, @"images\myblog");

                    if (!string.IsNullOrEmpty(obj.imgUrl))
                    {
                        //we have to delete the old image 
                        var oldImagePath = Path.Combine(wwwRootPath, obj.imgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(myblogPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.imgUrl = @"\images\myblog\" + fileName;

                }
                _unitOfWork.MyBlog.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Blog Updated Successfully";
                return RedirectToAction("Index", "MyBlog");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
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

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            MyBlog? obj = _unitOfWork.MyBlog.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(wwwRootPath, obj.imgUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.MyBlog.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Blog Deleted Successfully";
            return RedirectToAction("Index", "MyBlog");
        }
    }
}
