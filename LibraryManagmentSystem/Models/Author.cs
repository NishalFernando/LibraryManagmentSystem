using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? ModifiedAt { get; set; }

      
    }
}
