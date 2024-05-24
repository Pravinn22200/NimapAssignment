using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoryProductCrud.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId_ { get; set; }
        public virtual Category Categories { get; set; }
    }
}
