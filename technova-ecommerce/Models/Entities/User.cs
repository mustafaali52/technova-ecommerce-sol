using System.ComponentModel.DataAnnotations.Schema;

namespace technova_ecommerce.Models.Entities
{
    [Table("User")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("role")]
        public string? Role { get; set; }
        [Column("hashed_password")]
        public string HashedPassword { get; set; }
    }
}
