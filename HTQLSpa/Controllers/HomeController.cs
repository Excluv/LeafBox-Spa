using HTQLSpa.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HTQLSpa.Data;

namespace HTQLSpa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop()
        {
            IEnumerable<Product> ProductList = _db.Products.ToList();
            return View(ProductList);
        }
        public IActionResult Contact()
        {
            return View("Contact");
        }

        public IActionResult About()
        {
            return View("About");
        }

        // GET
        public IActionResult Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}