using Microsoft.EntityFrameworkCore;
using PreNet_3.Models;

namespace PreNet_3.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book updatedBook)
        {
            _context.Books.Update(updatedBook);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetBooksPublishedAfterYearAsync(int year)
        {
            return await _context.Books
                .Where(b => b.PublishedYear > year)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetBooksWithAuthorInfoAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Select(b => new
                {
                    BookId = b.Id,
                    BookTitle = b.Title,
                    PublishedYear = b.PublishedYear,
                    AuthorId = b.AuthorId,
                    AuthorName = b.Author.Name,
                    AuthorBirthDate = b.Author.DateOfBirth
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId)
        {
            return await _context.Books
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksOrderedByYearAsync(bool descending = false)
        {
            if (descending)
            {
                return await _context.Books
                    .OrderByDescending(b => b.PublishedYear)
                    .ToListAsync();
            }

            return await _context.Books
                .OrderBy(b => b.PublishedYear)
                .ToListAsync();
        }

        public async Task<object> GetBooksStatisticsAsync()
        {
            return await _context.Books
                .GroupBy(b => 1)
                .Select(g => new
                {
                    TotalBooks = g.Count(),
                    LatestYear = g.Max(b => b.PublishedYear),
                    EarliestYear = g.Min(b => b.PublishedYear),
                    AverageYear = g.Average(b => b.PublishedYear)
                })
                .FirstOrDefaultAsync();
        }
    }
}