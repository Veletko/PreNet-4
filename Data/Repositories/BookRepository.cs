using PreNet_3.Models;

namespace PreNet_3.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly InMemoryDataStore _dataStore;

        public BookRepository(InMemoryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await Task.FromResult(_dataStore.Books);
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_dataStore.Books.Find(x => x.Id == id));
        }

        public async Task AddAsync(Book book)
        {
            book.Id = _dataStore.Books.Any() ? _dataStore.Books.Max(b => b.Id) + 1 : 1;
            _dataStore.Books.Add(book);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Book updatedBook)
        {
            var book = _dataStore.Books.Find(b => b.Id == updatedBook.Id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.PublishedYear = updatedBook.PublishedYear;
                book.AuthorId = updatedBook.AuthorId;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var book = _dataStore.Books.Find(b => b.Id == id);
            if (book != null)
            {
                _dataStore.Books.Remove(book);
            }
            await Task.CompletedTask;
        }
    }
}
