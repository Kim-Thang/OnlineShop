using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DataAccess.Repository.IRepository;
using OnlineShop.DataAccess.Repository;
using OnlineShop.Models;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.productRepository.GetAll(includePropeties: "Category");

            return View(products);
        }

        public IActionResult Detail(int productId)
        {
            Product product = _unitOfWork.productRepository.Get(u => u.Id == productId, includePropeties: "Category");

            return View(product);
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
