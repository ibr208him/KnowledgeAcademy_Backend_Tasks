using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MVC_Project_BookStore.Data;
using MVC_Project_BookStore.Models;
using MVC_Project_BookStore.ViewModel;

namespace MVC_Project_BookStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var result = context.categories.ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) // check if the input has errors like null
            {                        //  was you set  before to not accept null
                return View("Create", categoryViewModel);
            }
            var category = new Category
            {
                Name = categoryViewModel.Name,
            };
            try { 
            context.categories.Add(category);
             context.SaveChanges();
             return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name","category name already exists");
                return View("Create", categoryViewModel);
            }
          
            

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            var category = context.categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            var categoryViewModel = new CategoryViewModel
            {
                Id = id,
                Name = category.Name,
            };
            return View("Create", categoryViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) // check if the input has errors like null
            {                        //  was you set  before to not accept null
                return View("Create", categoryViewModel);
            }
            var category = context.categories.Find(categoryViewModel.Id);
            if (category is null)
            {
                return NotFound();
            }
            category.Name = categoryViewModel.Name;
            category.UpdatedOn= DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var category = context.categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            var categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                CreatedOn = category.CreatedOn,
            UpdatedOn= category.UpdatedOn,
            };
            return View("Details",categoryViewModel);
        }

      
        public IActionResult Delete(int id)
        {
            var category = context.categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
          
            context.categories.Remove(category);
            context.SaveChanges();
            return Ok();
            //return RedirectToAction("Index");
        }
           

       public IActionResult checkName(CategoryViewModel categoryViewModel)
        {
            var isExists = context.categories.Any(category => category.Name == categoryViewModel.Name);
            return Json(!isExists);
        }








        }
}
