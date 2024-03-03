using Microsoft.AspNetCore.Mvc;
using HTQLSpa.Data;

namespace HTQLSpa.Controllers
{
    public class AccountController : Controller
    {
        public ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET
        public IActionResult UserAccount()
        {
            return View("UserAccount");
        }

        // GET
        public IActionResult EmployeeAccount()
        {
            return View("EmployeeAccount");
        }

        // GET
        public IActionResult AdminAccount()
        {
            return View("AdminAccount");
        }
    }
}
