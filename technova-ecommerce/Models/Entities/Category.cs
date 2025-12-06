using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace technova_ecommerce.Models.Entities
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("category_name")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Column("Description")]
        [Display(Name = "Category Description")]
        public string Description { get; set; }

        [Column("display_order")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
