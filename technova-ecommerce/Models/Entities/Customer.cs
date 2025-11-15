using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace technova_ecommerce.Models.Entities
{
    [Table("Customer")]
    public class Customer
    {
        [Column("customer_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Column("first_name")]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Column("email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
