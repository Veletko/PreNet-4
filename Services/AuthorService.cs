using PreNet_3.Data.Repositories;
using PreNet_3.Models;

namespace PreNet_3.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository; 

        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException("Имя автора обязательно.");

            await _authorRepository.AddAsync(author);
            return author;
        }

        public async Task UpdateAuthorAsync(int id, Author updatedAuthor)
        {
            if (string.IsNullOrWhiteSpace(updatedAuthor.Name))
                throw new ArgumentException("Имя автора обязательно.");

            var existingAuthor = await _authorRepository.GetByIdAsync(id);
            if (existingAuthor == null)
                throw new KeyNotFoundException("Автор не найден.");

            updatedAuthor.Id = id;
            await _authorRepository.UpdateAsync(updatedAuthor);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                throw new KeyNotFoundException("Автор не найден.");

           
            var hasBooks = (await _bookRepository.GetAllAsync()).Any(b => b.AuthorId == id);
            if (hasBooks)
                throw new InvalidOperationException("Нельзя удалить автора, у которого есть книги.");

            await _authorRepository.DeleteAsync(id);
        }
    }
}