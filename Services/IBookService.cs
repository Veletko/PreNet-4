using PreNet_3.Models;

namespace PreNet_3.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book book);
        Task UpdateBookAsync(int id, Book updatedBook);
        Task DeleteBookAsync(int id);
    }
}
