using PreNet_3.Data.Repositories;
using PreNet_3.Models;

namespace PreNet_3.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Название книги обязательно.");

            var author = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
                throw new ArgumentException("Недопустимый AuthorId. Укажите существующий ID автора.");

            await _bookRepository.AddAsync(book);
            return book;
        }

        public async Task UpdateBookAsync(int id, Book updatedBook)
        {
            if (string.IsNullOrWhiteSpace(updatedBook.Title))
                throw new ArgumentException("Название книги обязательно.");

            var author = await _authorRepository.GetByIdAsync(updatedBook.AuthorId);
            if (author == null)
                throw new ArgumentException("Недопустимый AuthorId.");

            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
                throw new KeyNotFoundException("Книга не найдена.");

            updatedBook.Id = id;
            await _bookRepository.UpdateAsync(updatedBook);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException("Книга не найдена.");

            await _bookRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooksPublishedAfterYearAsync(int year)
        {
            if (year < 0 || year > DateTime.Now.Year + 5)
                throw new ArgumentException("Некорректный год.");

            return await _bookRepository.GetBooksPublishedAfterYearAsync(year);
        }

        public async Task<IEnumerable<object>> GetBooksWithAuthorInfoAsync()
        {
            return await _bookRepository.GetBooksWithAuthorInfoAsync();
        }

        public async Task<object> GetBooksStatisticsAsync()
        {
            return await _bookRepository.GetBooksStatisticsAsync();
        }
    }
}