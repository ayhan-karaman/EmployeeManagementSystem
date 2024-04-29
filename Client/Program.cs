using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Blazored.LocalStorage;
using ClientLibrary.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor;
using Client.ApplicationStates;
using BaseLibrary.Entities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddHttpClient("SystemApiClient", client => 
{
    client.BaseAddress = new Uri("https://super-duper-space-train-vwwvgvrrw97fw647-7008.app.github.dev/");
}).AddHttpMessageHandler<CustomHttpHandler>();
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7008/") });
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();

// General Department / Department / Branch
builder.Services.AddScoped<IGenericService<GeneralDepartment>, GenericService<GeneralDepartment>>();
builder.Services.AddScoped<IGenericService<Department>, GenericService<Department>>();
builder.Services.AddScoped<IGenericService<Branch>, GenericService<Branch>>();

// Country / City / Town
builder.Services.AddScoped<IGenericService<Country>, GenericService<Country>>();
builder.Services.AddScoped<IGenericService<City>, GenericService<City>>();
builder.Services.AddScoped<IGenericService<Town>, GenericService<Town>>();

// Employee
builder.Services.AddScoped<IGenericService<Employee>, GenericService<Employee>>();



builder.Services.AddScoped<AllState>();

builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<SfDialogService>();

await builder.Build().RunAsync();
