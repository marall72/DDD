using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customers.Data.Model
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Firstname { get; set; }

        [MaxLength(255)]
        [Required]
        public string Lastname { get; set; }

        [MaxLength(255)]
        [Required]
        public string Email { get; set; }
    }
}
