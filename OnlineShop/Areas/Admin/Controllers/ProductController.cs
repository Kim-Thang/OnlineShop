using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.DataAccess.Repository;
using OnlineShop.DataAccess.Repository.IRepository;
using OnlineShop.Models;
using OnlineShop.Models.ViewModels;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;   

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.productRepository.GetAll(includePropeties: "Category").ToList();
         
            return View(products);
        }

        public IActionResult Upsert(int ?id)
        {
            ProductViewModel productViewModel = new()
            {
                Categories = _unitOfWork.categoryRepository
               .GetAll().Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()
               }),
                Product = new Product()
            };

            // Create
            if (id == null || id == 0)
            {
                return View(productViewModel);
            }

            // Update
            productViewModel.Product = _unitOfWork.productRepository.Get(u => u.Id == id);
            return View(productViewModel); 
        }
  
        [HttpPost]
        public IActionResult Upsert(Product product, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootpath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootpath, @"images\product");

                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        // Delete old image
                        var oldImagePath = Path.Combine(wwwRootpath, product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    product.ImageUrl = @"\images\product\" + fileName;
                }

                if (product.Id == 0)
                {
                    _unitOfWork.productRepository.Add(product);
                } 
                else
                {
                    _unitOfWork.productRepository.Update(product);                    
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult getAll()
        {
            List<Product> products = _unitOfWork.productRepository.GetAll(includePropeties: "Category").ToList();

            return Json(new { data = products });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Product product = _unitOfWork.productRepository.Get(u => u.Id == id);

            if (product == null)
            {
                return Json(new { success = false, message = "Error while delete" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.productRepository.Remove(product);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successfully" });
        }


        #endregion
    }
}
