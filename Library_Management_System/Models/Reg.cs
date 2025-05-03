using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class Reg
    {
        [Key]
        public int rid { get; set; }
        [Required(ErrorMessage = " Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
