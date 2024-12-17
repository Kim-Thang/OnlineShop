using Microsoft.AspNetCore.Mvc;
using OnlineShop.DataAccess.Data;
using OnlineShop.DataAccess.Repository.IRepository;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> CategoryList = _unitOfWork.categoryRepository.GetAll().ToList();
            return View(CategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            // Check annotation in model
            if (ModelState.IsValid)
            {
                //Presist()
                _unitOfWork.categoryRepository.Add(category);
                //Flush()
                _unitOfWork.Save();
                TempData["success"] = "Create success";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _unitOfWork.categoryRepository.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            // Check annotation in model
            if (ModelState.IsValid)
            {
                //Presist()
                _unitOfWork.categoryRepository.Update(category);
                //Flush()
                _unitOfWork.Save();
                TempData["success"] = "Edit success";
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _unitOfWork.categoryRepository.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? category = _unitOfWork.categoryRepository.Get(u => u.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.categoryRepository.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Delete success";

            return RedirectToAction("Index");
        }
    }
}
