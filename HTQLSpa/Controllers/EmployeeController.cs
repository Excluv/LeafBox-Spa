using Microsoft.AspNetCore.Mvc;
using HTQLSpa.Data;
using HTQLSpa.Models;

namespace HTQLSpa.Controllers
{
    public class EmployeeController : Controller
    {
        public ApplicationDbContext _db;
        public static int employeeID = 0;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET
        public IActionResult Index()
        {
            IEnumerable<Employee> EmployeeList = _db.Employees.ToList();
            return View(EmployeeList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Create(Employee obj)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            employeeID = employee.EmployeeID;

            return View(employee);
        }

        // POST
        [HttpPost]
        public IActionResult Edit(Employee obj)
        {
            if (ModelState.IsValid)
            {
                var employee = _db.Employees.Single(e => e.EmployeeID == employeeID);

                employee.EmployeeName = obj.EmployeeName;
                employee.DoB = obj.DoB;
                employee.EmployeeAddress = obj.EmployeeAddress;
                employee.PhoneNumber = obj.PhoneNumber;
                employee.Position = obj.Position;
                employee.AccountID = obj.AccountID;
                employee.Password = obj.Password;
                employee.Role = obj.Role;

                _db.Employees.Update(employee);
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

            var employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            employeeID = employee.EmployeeID;

            return View(employee);
        }

        // POST
        public IActionResult Delete()
        {
            var employee = _db.Employees.Find(employeeID);
            if (employee == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(employee);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
