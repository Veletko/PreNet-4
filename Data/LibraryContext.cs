using Microsoft.EntityFrameworkCore;
using PreNet_3.Models;

namespace PreNet_3.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Витя Коготь", DateOfBirth = new DateTime(1939, 1, 1) }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Законы бытия в Сибири", PublishedYear = 1980, AuthorId = 1 }
            );
        }
    }
}