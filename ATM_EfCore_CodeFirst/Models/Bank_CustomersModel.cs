using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ATM_EfCore_CodeFirst.Models
{
    public class Bank_CustomersModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public long MobileNumber { get; set; }
        [Required]
        public int PinNumber { get; set; }
        [Required]
        public double Balance { get; set; }

     
    }
    
}
