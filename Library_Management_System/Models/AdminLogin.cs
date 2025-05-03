using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class AdminLogin
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
