using System.ComponentModel.DataAnnotations;

namespace MVC_Project_BookStore.ViewModel
{
    public class AuthorFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(30, ErrorMessage = "maximun length is 30")]
        public string Name { get; set; }
    }
}
