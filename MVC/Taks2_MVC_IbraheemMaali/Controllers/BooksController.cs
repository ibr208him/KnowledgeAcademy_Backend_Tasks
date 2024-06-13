using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project_BookStore.Data;
using MVC_Project_BookStore.Models;
using MVC_Project_BookStore.ViewModel;
using System.Reflection.Metadata.Ecma335;

namespace MVC_Project_BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BooksController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var books = context.books.
                Include(b => b.Author).//This tells Entity Framework to include the Author related data when querying the books table (Join)
                Include(b => b.Categories).
                ThenInclude(b => b.Category). // to include the category inside categories list
                ToList();
            //foreach (var book in books)
            //{
            //    Console.WriteLine($" {book.Author.Name}"); 

            //    foreach(var category in book.Categories)
            //    {
            //        Console.WriteLine($"{category.Category.Name}");
            //    }
            //}


            var bookViewModels = books.Select(book => new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author.Name,
                Publisher = book.Publisher,
                PublishDate = book.PublishDate,
                ImageUrl = book.ImageUrl,
                categories = book.Categories.Select(category => category.Category.Name).ToList()
            }).ToList();


            //var bookViewModels=new List<BookViewModel>();

            //foreach(var book in books)
            //{
            //    var bookViewModel = new BookViewModel()
            //    {
            //        Id = book.Id,
            //        Title = book.Title,
            //        Author = book.Author.Name,
            //        Publisher = book.Publisher,
            //        PublishDate = book.PublishDate,
            //        ImageUrl = book.ImageUrl,
            //        categories = new List<string>()
            //    };

            //    foreach (var category in book.Categories)
            //    {
            //        bookViewModel.categories.Add(category.Category.Name);
            //    }
            //    bookViewModels.Add(bookViewModel);
            //}

            return View(bookViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var authors = context.authors.OrderBy(author => author.Name).ToList();
            var authorList = new List<SelectListItem>();

            foreach (var author in authors)
            {
                authorList.Add(new SelectListItem
                {
                    Value = author.Id.ToString(),
                    Text = author.Name
                }
                );
            }


            var categories = context.categories.OrderBy(category => category.Name).ToList();
            var categoryList = new List<SelectListItem>();

            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                }
                );
            }

            var viewModel = new BookFormViewModel
            {
                Authors = authorList,
                Categories = categoryList
            };
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Create(BookFormViewModel bookFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");

            }

            var imageName = "";
            if (bookFormViewModel != null)
            {
                imageName = Path.GetFileName(bookFormViewModel.ImageUrl.FileName); // to get the name of the uploaded image

                //return Content($"{imageName}");

                //var path = Path.Combine($"{webHostEnvironment.WebRootPath}"); // here the path is up to wwwroot only
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/images/books", imageName);

                //return Content($"{path}");//D:\أكاديمية المعرفة\Full stack\Backend\ASP.net\MVC\myCoding\MVC_Project_BookStore\
                //                          //MVC_Project_BookStore\wwwroot/images/books\myImage.jpeg

                var stream = System.IO.File.Create(path); // the image when uploaded will be stored in temprory location
                                                          // so here we will move it to the path 

                bookFormViewModel.ImageUrl.CopyTo(stream); // move the image from temp location to the specified path (stream)

            }

            var book = new Book()
            {
                ImageUrl = imageName,
                Title = bookFormViewModel.Title,
                AuthorId = bookFormViewModel.AuthorId,
                Publisher = bookFormViewModel.Publisher,
                PublishDate = bookFormViewModel.PublishDate,
                Description = bookFormViewModel.Description,

                Categories = bookFormViewModel.SelectedCategories.Select(id => new BookCategory
                {
                    CategoryId = id,
                }).ToList(),
            };



            context.books.Add(book);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var book = context.books.Find(id);
            if(book == null)
            {
                return NotFound();
            }
           
            var path = Path.Combine($"{webHostEnvironment.WebRootPath}/images/books", book.ImageUrl);

            if(System.IO.File.Exists(path)) // check if the image is exist
            {
                System.IO.File.Delete(path);
            }
               

            context.books.Remove(book);
            context.SaveChanges(true);

            return RedirectToAction("Index");
        }


    }
}
