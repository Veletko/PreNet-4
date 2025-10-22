using System.ComponentModel.DataAnnotations;

namespace PreNet_3.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название книги обязательно")]
        [StringLength(200, ErrorMessage = "Название книги не может превышать 200 символов")]
        public string Title { get; set; } = string.Empty;
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "AuthorId обязателен")]
        public int AuthorId { get; set; }
    }
}