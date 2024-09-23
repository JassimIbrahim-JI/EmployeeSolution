
using BaseLibrary.Class.Response;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IGenaricRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);

        Task<GeneralResponse> Insert(T item);
        Task<GeneralResponse> Update(T item);
        Task<GeneralResponse> DeleteById(int id);
    }
}
