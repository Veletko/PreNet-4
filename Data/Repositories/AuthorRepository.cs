using PreNet_3.Models;

namespace PreNet_3.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly InMemoryDataStore _dataStore;

        public AuthorRepository(InMemoryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await Task.FromResult(_dataStore.Authors);
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_dataStore.Authors.Find(x => x.Id == id));
        }

        public async Task AddAsync(Author author)
        {
            author.Id = _dataStore.Authors.Any() ? _dataStore.Authors.Max(a => a.Id) + 1 : 1;
            _dataStore.Authors.Add(author);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Author updatedAuthor)
        {
            var author = _dataStore.Authors.Find(a => a.Id == updatedAuthor.Id);
            if (author != null)
            {
                author.Name = updatedAuthor.Name;
                author.DateOfBirth = updatedAuthor.DateOfBirth;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var author = _dataStore.Authors.Find(a => a.Id == id);
            if (author != null)
            {
                _dataStore.Authors.Remove(author);
            }
            await Task.CompletedTask;
        }
    }
}
