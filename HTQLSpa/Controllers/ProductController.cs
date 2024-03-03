using Microsoft.AspNetCore.Mvc;
using HTQLSpa.Data;
using HTQLSpa.Models;

namespace HTQLSpa.Controllers
{
    public class ProductController : Controller
    {
        public readonly ApplicationDbContext _db;
        public static int productID = 0;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _db.Products.ToList();
            return View(ProductList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET
        [HttpGet]
        public IActionResult Edit(int? id)
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

            productID = product.ProductID;

            return View(product);
        }

        // POST
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                var product = _db.Products.Single(p => p.ProductID == productID);

                product.ProductName = obj.ProductName;
                product.ProductDescription = obj.ProductDescription;
                product.ProductPrice = obj.ProductPrice;
                product.ImagePath = obj.ImagePath;
                product.BasketImagePath = obj.BasketImagePath;
                product.DetailImagePath = obj.DetailImagePath;

                _db.Update(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET
        public IActionResult Delete(int? id)
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

            productID = product.ProductID;

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            var product = _db.Products.Find(productID);
            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
