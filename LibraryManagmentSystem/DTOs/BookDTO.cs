using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.DTOs
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDTO : ControllerBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int YearPublished { get; set; }
        public int RackNo { get; set; }
        public int RowNo { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public string AuthorFullName { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
