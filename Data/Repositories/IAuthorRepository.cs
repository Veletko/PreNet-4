using PreNet_3.Models;

namespace PreNet_3.Data.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(int id);
        Task<IEnumerable<object>> GetAuthorsWithBookCountAsync();
        Task<IEnumerable<Author>> FindAuthorsByNameAsync(string name);
        Task<IEnumerable<Author>> GetAuthorsWithMoreThanNBooksAsync(int bookCount);
    }
}