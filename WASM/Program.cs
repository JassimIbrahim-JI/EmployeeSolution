using BaseLibrary.Class.Entities;
using Blazored.LocalStorage;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WASM;
using WASM.ApplicationStates;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7018/") });

builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7018/");
}).AddHttpMessageHandler<CustomHttpHandler>();


builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationState>();
builder.Services.AddScoped<IUserAcountService,UserAccountService>();

builder.Services.AddScoped<IGenaricService<GeneralDepartment>, GenaricService<GeneralDepartment>>();
builder.Services.AddScoped<IGenaricService<Department>, GenaricService<Department>>();
builder.Services.AddScoped<IGenaricService<Branch>, GenaricService<Branch>>();

builder.Services.AddScoped<IGenaricService<Country>, GenaricService<Country>>();
builder.Services.AddScoped<IGenaricService<City>, GenaricService<City>>();
builder.Services.AddScoped<IGenaricService<Town>, GenaricService<Town>>();

builder.Services.AddScoped<IGenaricService<Employee>, GenaricService<Employee>>();

builder.Services.AddScoped<IGenaricService<Doctor>, GenaricService<Doctor>>();

builder.Services.AddScoped<IGenaricService<OverTime>, GenaricService<OverTime>>();
builder.Services.AddScoped<IGenaricService<OverTimeType>, GenaricService<OverTimeType>>();
builder.Services.AddScoped<IGenaricService<Sanction>, GenaricService<Sanction>>();
builder.Services.AddScoped<IGenaricService<Vaction>, GenaricService<Vaction>>();
builder.Services.AddScoped<IGenaricService<VactionType>, GenaricService<VactionType>>();
builder.Services.AddScoped<IGenaricService<SanctionType>, GenaricService<SanctionType>>();

builder.Services.AddScoped<AllSectionState>();
builder.Services.AddScoped<UserProfileState>();

//builder.Services.AddSyncfusionBlazor();
//builder.Services.AddScoped<SfDialogService>();
builder.Services.AddSweetAlert2();
builder.Services.AddMudServices();
await builder.Build().RunAsync();
