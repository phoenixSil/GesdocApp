namespace Gesd.Features.Contrats.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> Get();
        public Task<T> Get(Guid id);
        public Task<T> Add(T entite);
        public Task<T> Update(T entite);
        public Task<bool> Delete(T entite);
        public Task<bool> Exists(Guid id);
    }
}
