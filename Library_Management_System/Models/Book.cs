using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class Book
    {
        [Key]
        public int Bid { get; set; }
        [Required(ErrorMessage = "Book Name is required.")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Language { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
