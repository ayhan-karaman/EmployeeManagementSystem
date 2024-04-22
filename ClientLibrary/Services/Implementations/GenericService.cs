using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations
{
    public class GenericService<T> : IGenericService<T>
    {
        private readonly GetHttpClient _getHttpClient;

        public GenericService(GetHttpClient getHttpClient)
        {
            _getHttpClient = getHttpClient;
        }

        public async Task<GeneralResponse> DeleteAsync(int id, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.DeleteAsync($"{baseUrl}/delete/{id}");
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        public async Task<ICollection<T>> GetAllAsync(string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results =  await httpClient.GetFromJsonAsync<ICollection<T>>($"{baseUrl}/all");
            return results!;
        }

        public async Task<T> GetByIdAsync(int id, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<T>($"{baseUrl}/single/{id}");
            return result!;
        }

        public async Task<GeneralResponse> InsertAsync(T item, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/add", item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        public async Task<GeneralResponse> UpdateAsync(T item, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($@"{baseUrl}/update", item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
    }
}