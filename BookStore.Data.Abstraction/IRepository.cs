

namespace BookStore.Data.Abstraction
{
    public interface IRepository<T>
    {
        Task<string> InsertAsync(T item, CancellationToken cancellationToken);
        Task<bool> UdpateAsync(T item, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> GetByIdAsync(string id, CancellationToken cancellationToken);
    }
}
