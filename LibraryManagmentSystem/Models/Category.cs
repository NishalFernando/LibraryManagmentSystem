using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
