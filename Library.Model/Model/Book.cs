using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("AuthorID")]
        public string AuthorID { get; set; } = "";

        [Required]
        [DisplayName("Author")]
        public string AuthorName { get; set; } = "";

        [Required]
        [DisplayName("Book Name")]
        public string Name { get; set; } = "";

        [Required]
        [DisplayName("The number of Book")]
        public int Count { get; set; }

        [Required]
        [DisplayName("책 버전")]
        public int Version { get; set; }

        [Required]
        [DisplayName("현재 대여중인 권수")]
        public int RentalCount { get; set; }

        [Required]
        [DisplayName("역대 총 대여 횟수")]
        public int RentalCountHistory { get; set; }
    }

    public class BookSelectResponse
    {
        public List<Book> Books { get; set; }
        public int NumOfBook { get; set; }
    }

}
