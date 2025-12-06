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
        public string CategoryName { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("display_order")]
        public int DisplayOrder { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
