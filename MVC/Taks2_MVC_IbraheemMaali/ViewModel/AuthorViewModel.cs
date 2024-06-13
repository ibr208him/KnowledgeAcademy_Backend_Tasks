using System.ComponentModel.DataAnnotations;

namespace MVC_Project_BookStore.ViewModel
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [MaxLength(30, ErrorMessage = "maximun length is 30")]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
