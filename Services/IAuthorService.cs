using PreNet_3.Models;

namespace PreNet_3.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author> CreateAuthorAsync(Author author);
        Task UpdateAuthorAsync(int id, Author updatedAuthor);
        Task DeleteAuthorAsync(int id);
        Task<IEnumerable<object>> GetAuthorsWithBookCountAsync();
        Task<IEnumerable<Author>> FindAuthorsByNameAsync(string name);
    }
}