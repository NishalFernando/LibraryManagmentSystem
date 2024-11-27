using LibraryManagmentSystem.DBContext;
using LibraryManagmentSystem.DTOs;
using LibraryManagmentSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace LibraryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BooksController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
          

            var books = await (from book in _dbContext.Books
                               join author in _dbContext.Authors on book.AuthorId equals author.Id
                               join category in _dbContext.Categories on book.CategoryId equals category.Id
                               select new BookDTO
                               {
                                   Id = book.Id,
                                   Title = book.Title,
                                   ISBN = book.ISBN,
                                   YearPublished = book.YearPublished,
                                   RackNo = book.RackNo,
                                   RowNo = book.RowNo,
                                   TotalCopies = book.TotalCopies,
                                   AvailableCopies = book.AvailableCopies,
                                   AuthorFullName = author.FirstName + " " + author.LastName,  // Assuming you want full name
                                   CategoryName = category.Name,
                                   CreatedAt = book.CreatedAt,
                                   ModifiedAt = book.ModifiedAt
                               }).ToListAsync();  // Now you can safely call ToListAsync()
            return Ok(books);
            

        }

        
        [HttpPost]
        public async Task<IActionResult> InsertBook(Book book)
        {
     

            // Create a new Book entity
            var newBook = new Book
            {
                Title = book.Title,
                ISBN = book.ISBN,
                YearPublished = book.YearPublished,
                RackNo = book.RackNo,
                RowNo = book.RowNo,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.TotalCopies, 
                AuthorId = book.AuthorId, 
                CategoryId = book.CategoryId, 
                CreatedAt = DateTime.Now,
                ModifiedAt= null
            };

            try
            {
              
                await _dbContext.Books.AddAsync(newBook);

                await _dbContext.SaveChangesAsync();

                return Ok(new { Message = "Book added successfully", BookId = newBook.Id });
            }
            catch (Exception ex)
            {
             
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBook == null)
            {
                return NotFound("Book not Exist");
            }

            try
            {
                existingBook.Title = book.Title;
                existingBook.ISBN = book.ISBN;
                existingBook.YearPublished = book.YearPublished;
                existingBook.RackNo = book.RackNo;
                existingBook.RowNo = book.RowNo;
                existingBook.TotalCopies = book.TotalCopies;
                existingBook.AvailableCopies = book.AvailableCopies;
                existingBook.AuthorId = book.AuthorId;
                existingBook.CategoryId = book.CategoryId;
                existingBook.ModifiedAt = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                return Ok("Book Updated Succesfully");
            }
            catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id==id);
            if (existingBook == null)
            {
                return NotFound("Book not Exist");
            }

            try
            {
                _dbContext.Books.Remove(existingBook);
                await _dbContext.SaveChangesAsync();

                return Ok("Book deleted Succesfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
