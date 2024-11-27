using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string ISBN { get; set; }
        public int YearPublished { get; set; }
        public int RackNo { get; set; }
        public int RowNo { get; set;}
        public int TotalCopies { get; set; }
        public int AvailableCopies {  get; set; }
        public DateTime CreatedAt { get; set;}
        public DateTime? ModifiedAt { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
