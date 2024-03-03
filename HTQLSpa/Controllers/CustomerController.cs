using Microsoft.AspNetCore.Mvc;
using HTQLSpa.Data;
using HTQLSpa.Models;

namespace HTQLSpa.Controllers
{
    public class CustomerController : Controller
    {
        public ApplicationDbContext _db;
        public static int customerID = 0;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Customer> CustomerList = _db.Customers.ToList();
            return View(CustomerList);
        }

        // GET
        public IActionResult Edit()
        {
            if ((!String.IsNullOrEmpty(LoginController.accountID)) && (String.Equals(LoginController.account_type, "User")))
            {
                var customer = _db.Customers.Single(c => c.AccountID == LoginController.accountID);
                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }
            else
            {
                return Redirect("~/Login/Index");
            }
        }

        // POST
        [HttpPost]
        public IActionResult Edit(Customer obj)
        {
            if (ModelState.IsValid)
            {
                var customer = _db.Customers.Single(c => c.CustomerID == customerID);

                customer.CustomerName = obj.CustomerName;
                customer.DoB = obj.DoB;
                customer.CustomerAddress = obj.CustomerAddress;
                customer.PhoneNumber = obj.PhoneNumber;
                customer.Password = obj.Password;

                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
}
