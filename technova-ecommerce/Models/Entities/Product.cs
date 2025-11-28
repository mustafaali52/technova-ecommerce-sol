using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace technova_ecommerce.Models.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("product_name")]
        public string ProductName { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("stock_price")]
        public decimal StockPrice { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        
        // Foreign Key
        [Column("category_id")]
        public int CategoryId { get; set; }
        
        // Navigation Property
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
