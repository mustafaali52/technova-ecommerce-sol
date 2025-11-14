using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Models.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Column("customer_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Column("first_name")]
        [Display (Name = "Name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Column("email")]
        [Display(Name = "Email")]
        //[Validation(Regex = "")]
        public string Email { get; set; }

        [Column("password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Column("address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Column("phone_number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
