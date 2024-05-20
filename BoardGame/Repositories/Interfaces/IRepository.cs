using MongoDB.Bson;

namespace BoardGame.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(ObjectId id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<ObjectId> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(ObjectId id);
    }
}
