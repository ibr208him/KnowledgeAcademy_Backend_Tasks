using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Project_BookStore.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace MVC_Project_BookStore.ViewModel
{
    public class BookFormViewModel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; } = null!;
        [Display(Name ="Author")]    // so that the name attribute will be author and label also
        public int AuthorId { get; set; }
        public List<SelectListItem>? Authors { get; set; } // we have to put ? 
        [Display(Name = "Publish Date")]
        public string Publisher { get; set; } = null!;
        public DateTime PublishDate { get; set; }= DateTime.Now; // the default date other wise will be 01/01/0001
        [Display(Name = "Please choose image")]
        public IFormFile? ImageUrl { get; set; }
        public string Description { get; set; } = null!;

        public List<int> SelectedCategories { get; set; } = new List<int>(); // list of categories to be sent to databse
        public List<SelectListItem>? Categories { get; set; } // to view the list of categories also we have to put ? 














    }
}
