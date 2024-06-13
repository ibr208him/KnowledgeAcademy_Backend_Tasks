using Microsoft.AspNetCore.Mvc;
using WebApplication1._2.Data;

namespace WebApplication1._2.Controllers
{
    public class EmployeeController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var employees=context.employees.ToList();

            return View("Index", employees);
        }
    }
}
