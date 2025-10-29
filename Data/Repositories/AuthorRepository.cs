using Microsoft.EntityFrameworkCore;
using PreNet_3.Models;

namespace PreNet_3.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _context;

        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author updatedAuthor)
        {
            _context.Authors.Update(updatedAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<object>> GetAuthorsWithBookCountAsync()
        {
            return await _context.Authors
                .Select(a => new
                {
                    AuthorId = a.Id,
                    AuthorName = a.Name,
                    DateOfBirth = a.DateOfBirth,
                    BookCount = a.Books.Count
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> FindAuthorsByNameAsync(string name)
        {
            return await _context.Authors
                .Where(a => a.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsWithMoreThanNBooksAsync(int bookCount)
        {
            return await _context.Authors
                .Where(a => a.Books.Count > bookCount)
                .ToListAsync();
        }
    }
}