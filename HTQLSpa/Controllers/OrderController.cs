using Microsoft.AspNetCore.Mvc;
using HTQLSpa.Data;
using HTQLSpa.Models;

namespace HTQLSpa.Controllers
{
    public class OrderController : Controller
    {
        public ApplicationDbContext _db;
        public static int orderID = 0;
        public static int order_detailID = 0;
        public static List<Product> ProductList = new List<Product>();
        public static List<int> productID = new List<int>();
        public static int[] amount = null;
        public float Total = 0;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET
        // A list of orders
        public IActionResult Index()
        {
            IEnumerable<Order> OrderList = null;
            if (LoginController.account_type == "Admin")
            {
                OrderList = _db.Orders.ToList();
            }
            else if (LoginController.account_type == "Employee")
            {
                OrderList = _db.Orders.Where(o => o.EmployeeID == 0).ToList();
            }
            else
            {
                var customer = _db.Customers.Single(c => c.AccountID == LoginController.accountID);
                int customerID = customer.CustomerID;
                OrderList = _db.Orders.Where(o => o.CustomerID == customerID).ToList();
            }
            return View(OrderList);
        }

        // GET
        // A list of orders processed by an arbitrary employee
        public IActionResult EmployeeIndex()
        {
            int employeeID = _db.Employees.Single(e => e.AccountID == LoginController.accountID).EmployeeID;
            IEnumerable<Order> OrderList = _db.Orders.Where(o => o.EmployeeID == employeeID).ToList();
            foreach (Order order in OrderList)
            {

            }
            return View(OrderList);
        }

        // GET
        public IActionResult AddToBasket(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Initialize an array of size equals to the total number of products in db
            if (amount == null)
            {
                int max_idx = 0;
                List<Product> CurrentProductList = _db.Products.ToList();
                foreach (var product in CurrentProductList)
                {
                    if (product.ProductID >= max_idx)
                    {
                        max_idx = product.ProductID;
                    }
                }

                amount = new int[max_idx + 1];
            }

            // Increment the amount of products or add a product to basket
            if (productID.Contains(id.Value))
            {
                amount[id.Value] += 1;
            }
            else
            {
                productID.Add(id.Value);
                amount[id.Value] = 1;
            }

            // Add a product to basket if it does not already exist
            bool existed = false;
            foreach (int idx in productID)
            {
                foreach (var prod in ProductList)
                {
                    if (prod.ProductID == idx)
                    {
                        existed = true;
                        break;
                    }
                }
                if (!existed)
                {
                    var product = _db.Products.Find(idx);
                    if (product == null)
                    {
                        continue;
                    }

                    ProductList.Add(product);
                }
                existed = false;
            }

            return Redirect("~/Home/Shop");
        }

        public IActionResult ClearBasket()
        {
            ProductList = new List<Product>();
            productID = new List<int>();
            amount = new int[amount.Length + 1];

            return Redirect("~/Home/Shop");
        }

        public IActionResult Create()
        {
            if (LoginController.accountID == "")
            {
                return Redirect("~/Login/Index");
            }

            if (ProductList.Count == 0)
            {
                return Redirect("~/Home/Shop");
            }
             
            // Get customer ID and add order to db
            var customer = _db.Customers.Single(c => c.AccountID == LoginController.accountID);
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.OrderTotal = 0;
            order.Paid = true;
            order.Completed = false;
            order.CustomerID = customer.CustomerID;
            order.EmployeeID = 0;

            _db.Orders.Add(order);
            _db.SaveChanges();

            // Update total value of the order
            order = _db.Orders.Single(o => o.OrderDate == order.OrderDate);
            if (order == null)
            {
                return NotFound();
            }

            float total = 0;
            foreach (int id in productID)
            {
                if (id == 0)
                {
                    continue;
                }

                var product = _db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }

                total += product.ProductPrice * amount[id];

                Order_Detail detail = new Order_Detail();
                detail.Amount = amount[id];
                detail.OrderID = order.OrderID;
                detail.ProductID = id;

                _db.Order_Details.Add(detail);
            }

            order.OrderTotal = total;
            _db.Orders.Update(order);
            _db.SaveChanges();

            // Discard old basket and create a new one
            ProductList = new List<Product>();
            productID = new List<int>();
            amount = new int[_db.Products.ToList().Count + 1];

            return Redirect("~/Account/UserAccount");
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            orderID = order.OrderID;

            return View(order);
        }

        // POST
        [HttpPost]
        public IActionResult Edit(Order obj)
        {
            if (ModelState.IsValid)
            {
                var order = _db.Orders.Single(o => o.OrderID == orderID);

                order.OrderDate = obj.OrderDate;
                order.OrderTotal = obj.OrderTotal;
                order.Paid = obj.Paid;
                order.Completed = obj.Completed;
                order.CustomerID = obj.CustomerID;
                order.EmployeeID = obj.EmployeeID;

                _db.Orders.Update(obj);
                _db.SaveChanges();

                orderID = 0;

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET
        public IActionResult Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            IEnumerable<Order_Detail> order_detail = _db.Order_Details.Where(o => o.OrderID == id).ToList();
            if (order_detail == null)
            {
                return NotFound();
            }

            return View(order_detail);
        }

        // GET
        public IActionResult EditDetail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var order = _db.Order_Details.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            order_detailID = order.ID;

            return View(order);
        }

        // POST
        [HttpPost]
        public IActionResult EditDetail(Order_Detail obj)
        {
            if (ModelState.IsValid)
            {
                var order_detail = _db.Order_Details.Single(od => od.ID == order_detailID);

                order_detail.Amount = obj.Amount;
                order_detail.OrderID = obj.OrderID;
                order_detail.ProductID = obj.ProductID;

                _db.Order_Details.Update(order_detail);
                _db.SaveChanges();

                order_detailID = 0;

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

            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            orderID = order.OrderID;

            return View(order);
        }

        // POST
        [HttpPost]
        public IActionResult Delete()
        {
            var order = _db.Orders.Find(orderID);
            var order_detail_list = _db.Order_Details.Where(od => od.OrderID == orderID).ToList();
            if (order == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(order);
            foreach (var od in order_detail_list)
            {
                _db.Order_Details.Remove(od);
            }

            _db.SaveChanges();

            orderID = 0;

            return RedirectToAction("Index");
        }

        public IActionResult Receive(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            int employeeID = _db.Employees.Single(e => e.AccountID == LoginController.accountID).EmployeeID;
            order.EmployeeID = employeeID;
            _db.Orders.Update(order);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Process(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Completed = true;
            _db.Orders.Update(order);
            _db.SaveChanges();

            return RedirectToAction("EmployeeIndex");
        }
    }
}
