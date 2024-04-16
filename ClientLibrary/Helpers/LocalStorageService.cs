using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace ClientLibrary.Helpers
{
    public class LocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        public const string StorageKey = "authentication-token";
        public LocalStorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<string> GetToken() => await _localStorageService.GetItemAsStringAsync(StorageKey);
        public async Task SetToken(string item) => await _localStorageService.SetItemAsStringAsync(StorageKey, item);
        public async Task RemoveToken() => await _localStorageService.RemoveItemAsync(StorageKey);
    }
}