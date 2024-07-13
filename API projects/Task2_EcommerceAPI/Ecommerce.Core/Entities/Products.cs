using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        [ForeignKey(nameof(Category))] // [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Categories? Category { get; set; } // this line to tell about the relation between Products and Categoies
                                                  //  ? nullable >>> so that when create an instance of the Products class,
                                                  // this field will not be null>> no errors
        public virtual ICollection<OrderDetails>? OrderDetails { get; set; } = new HashSet<OrderDetails>(); // unique set of products

    }
}
