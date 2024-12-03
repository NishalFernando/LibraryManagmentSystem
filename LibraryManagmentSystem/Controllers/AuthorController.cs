using LibraryManagmentSystem.DBContext;
using LibraryManagmentSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LibraryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> InsertAuthor(Author author)
        {
            try
            {
                var newAuthor = new Author
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    CreatedAt = DateTime.Now
                };

                await _dbContext.Authors.AddAsync(newAuthor);
                await _dbContext.SaveChangesAsync();

                return Ok("Author added Succesfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            var existingAuthor = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (existingAuthor == null) 
            {
                return NotFound("Author not exist");
            }

            try
            {
                existingAuthor.FirstName = author.FirstName;
                existingAuthor.LastName = author.LastName;
                existingAuthor.ModifiedAt = DateTime.Now;

                await _dbContext.SaveChangesAsync();
                return Ok("Author Updated Succesfully");

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var existingAuthor = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if(existingAuthor == null)
            {
                return NotFound("Author not Exist");
            }
            try
            {
                _dbContext.Authors.Remove(existingAuthor);
                await _dbContext.SaveChangesAsync();

                return Ok("Author Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return Conflict("Cannot Delete. Because Author is referenced to elsewhere");
            }
        }

    }
}
