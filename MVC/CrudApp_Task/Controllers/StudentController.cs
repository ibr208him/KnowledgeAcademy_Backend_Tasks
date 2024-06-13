using Microsoft.AspNetCore.Mvc;
using session2.Data;
using session2.Models;

namespace session2.Controllers
{
    public class StudentController : Controller
    {
        ApplicationDbContext context =new ApplicationDbContext();
       
        public IActionResult Index()
        {
            var stds = context.students.ToList();
            return View("Index",stds);
        }

        public IActionResult Details(int id)
        {
            var std=context.students.Find(id); // find is Linq method to search for an entity by id
            return View("Details",std);
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        public IActionResult Store(Student student)
        {
            context.students.Add(student);
            context.SaveChanges();

            //var stds = context.students.ToList();
            //return View("Index", stds);
            return RedirectToAction("Index"); // equievelent to above two lines
        }


        public IActionResult Delete(int id)
        {
            var std = context.students.Find(id);
            context.students.Remove(std);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        }
}
