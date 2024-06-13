using Microsoft.AspNetCore.Mvc;
using MVC_Project_BookStore.Data;
using MVC_Project_BookStore.Models;
using MVC_Project_BookStore.ViewModel;

namespace MVC_Project_BookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext context;

        public AuthorsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var result = context.authors.ToList();
            var authorsViewModel = new List<AuthorViewModel>();
            foreach (var author in result)
            {
                var authorViewModel = new AuthorViewModel()
                {
                    Id = author.Id,
                    Name = author.Name,
                    CreatedOn = author.CreatedOn,
                    UpdatedOn = author.UpdatedOn,
                };
                authorsViewModel.Add(authorViewModel);
            }
            return View("Index", authorsViewModel);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View("Form");
        }

        [HttpPost]
        public IActionResult Create(AuthorFormViewModel authorFormViewModel)
        {
            if (!ModelState.IsValid) // check if the input has errors like null
            {                        //  was you set  before to not accept null
                return View("Create", authorFormViewModel);
            }
            var author = new Author()
            {
                Name = authorFormViewModel.Name,
            };
            try
            {
                context.authors.Add(author);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "author name already exists");
                return View("Create", authorFormViewModel);
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            var author = context.authors.Find(id);
            if (author is null)
            {
                return NotFound();
            }
            var authorFormViewModel = new AuthorFormViewModel()
            {
                Id = id,
                Name = author.Name,
            };
            return View("Form", authorFormViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AuthorFormViewModel authorFormViewModel)
        {
            if (!ModelState.IsValid) // check if the input has errors like null
            {                        //  was you set  before to not accept null
                return View("Form", authorFormViewModel);
            }
            var author = context.authors.Find(authorFormViewModel.Id);
            if (author is null)
            {
                return NotFound();
            }
            author.Name = authorFormViewModel.Name;
            author.UpdatedOn = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            var author = context.authors.Find(id);
            if (author is null)
            {
                return NotFound();
            }
            var authorViewModel = new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                CreatedOn = author.CreatedOn,
                UpdatedOn = author.UpdatedOn,
            };
            return View("Details", authorViewModel);
        }


        public IActionResult Delete(int id)
        {
            var author = context.authors.Find(id);
            if (author is null)
            {
                return NotFound();
            }

            context.authors.Remove(author);
            context.SaveChanges();
          
            return RedirectToAction("Index");
        }















    }

}
