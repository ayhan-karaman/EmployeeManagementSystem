using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IGenericService<T> 
    {
        Task<ICollection<T>> GetAllAsync(string baseUrl);
        Task<T> GetByIdAsync(int id, string baseUrl);
        Task<GeneralResponse> InsertAsync(T item, string baseUrl);
        Task<GeneralResponse> UpdateAsync(T item, string baseUrl);
        Task<GeneralResponse> DeleteAsync(int id, string baseUrl);
    }
}