using LibraryManagmentSystem.DBContext;
using LibraryManagmentSystem.DTOs;
using LibraryManagmentSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> InsertCategory(Category category)
        {
            var newCategory = new Category
            {
                Name = category.Name,
                CreatedAt = DateTime.Now
            };

            try
            {
                await _dbContext.Categories.AddAsync(newCategory);

                await _dbContext.SaveChangesAsync();

                return Ok("Category Added Sucessfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
                           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,Category category)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x=> x.Id == id);
            if(existingCategory == null)
            {
                return NotFound("Category not Exist");
            }

            try
            {
                existingCategory.Name = category.Name;
                existingCategory.ModifiedAt = DateTime.Now;

                await _dbContext.SaveChangesAsync();
                return Ok("Category Updated Successfully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory == null)
            {
                return NotFound("Category not Exist");
            }

            try
            {
                 _dbContext.Categories.Remove(existingCategory);

                await _dbContext.SaveChangesAsync();

                return Ok("Category Deleted Successfully");
            }
            catch(Exception ex) {
                
                return Conflict("Cannot Delete. Because Category is referenced to elsewhere");
              
            }
        }
    }
}
