
using BaseLibrary.Class.Response;

namespace ClientLibrary.Services.Contracts
{
    public interface IGenaricService<T>
    {
        Task<List<T>> GetAll(string baseUrl);
        Task<T> GetById (int id, string baseUrl);
        Task<GeneralResponse>Insert (T item, string baseUrl);
        Task<GeneralResponse>Update (T item, string baseUrl);
        Task<GeneralResponse>Delete (int id, string baseUrl);
    }
}
