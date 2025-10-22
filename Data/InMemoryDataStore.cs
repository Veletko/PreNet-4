using PreNet_3.Models;

namespace PreNet_3.Data
{
    public class InMemoryDataStore
    {
        private readonly List<Author> _authors = new();
        private readonly List<Book> _books = new();

        public List<Author> Authors => _authors;
        public List<Book> Books => _books;

        public InMemoryDataStore()
        {
            _authors.Add(new Author { Id = 1, Name = "Витя Коготь", DateOfBirth = new DateTime(1939, 1, 1) });
            _books.Add(new Book { Id = 1, Title = "Законы бытия в Сибири", PublishedYear = 1980, AuthorId = 1 });
        }
    }
}
