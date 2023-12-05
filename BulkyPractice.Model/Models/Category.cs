using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyPractice.Model.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [MinLength(4,ErrorMessage ="Name must be atleast 4 digits long.")]
        [DisplayName("Category Name")]
        [MaxLength(30, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Display Order is required.")]
        [DisplayName("Display Order")]
        [Range(0, 100, ErrorMessage = "Display Order must be between 0 and 100.")]
        public int DisplayOrder { get; set; }

    }
}
