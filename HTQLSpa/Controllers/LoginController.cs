using Microsoft.AspNetCore.Mvc;
using HTQLSpa.Data;
using HTQLSpa.Models;

namespace HTQLSpa.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        public static Boolean loggedin = false;
        public static string accountID = "";
        public static string account_type = "";

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult EmployeeLogin()
        {
            return View("EmployeeLogin");
        }

        public IActionResult Logout()
        {
            loggedin = false;
            accountID = "";
            account_type = "";

            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public IActionResult UserVerification(Customer obj)
        {
            try
            {
                var customer = _db.Customers.Single(c => c.AccountID == obj.AccountID && c.Password == obj.Password);
                loggedin = true;
                accountID = customer.AccountID;
                account_type = "User";

                return Redirect("~/Account/UserAccount");
            }
            catch (Exception err)
            {
                return View("Index");
            }
        }

        // POST
        [HttpPost]
        public IActionResult EmployeeVerification(Employee obj)
        {
            try
            {
                var employee = _db.Employees.Single(e => e.AccountID == obj.AccountID && e.Password == obj.Password);
                loggedin = true;
                accountID = employee.AccountID;
                account_type = employee.Role;

                if (String.Equals(employee.Role, "Employee"))
                {
                    return Redirect("~/Account/EmployeeAccount");
                }
                else
                {
                    return Redirect("~/Account/AdminAccount");
                }
            }
            catch (Exception err)
            {
                return View("Index"); 
            }
        }

        // GET
        public IActionResult UserRegister()
        {
            return View("UserRegister");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserRegister(Customer obj)
        {
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
}
