using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //Annotation example
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
