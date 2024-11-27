using LibraryManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.DBContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Book>()
        //                .HasOne(b => b.Author)  // Book has one Author
        //                .WithMany()  // Author can have many books
        //                .HasForeignKey(b => b.AuthorId)
        //                .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Book>()
        //                .HasOne(b => b.Category)  // Book has one Category
        //                .WithMany()  // Category can have many books
        //                .HasForeignKey(b => b.CategoryId)
        //                .OnDelete(DeleteBehavior.Restrict);
        //}
    }
}
