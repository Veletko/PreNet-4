using PreNet_3.Models;

namespace PreNet_3.Data.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);

        Task<IEnumerable<Book>> GetBooksPublishedAfterYearAsync(int year);
        Task<IEnumerable<object>> GetBooksWithAuthorInfoAsync();
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId);
        Task<IEnumerable<Book>> GetBooksOrderedByYearAsync(bool descending = false);
        Task<object> GetBooksStatisticsAsync();
    }
}