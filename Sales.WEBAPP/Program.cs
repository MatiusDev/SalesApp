using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sales.WEB.Repositories;
using Sales.WEB;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IRepository, Repository>();

//HttpClient
builder.Services.AddHttpClient<IRepository, Repository>(c => c.BaseAddress = new Uri("https://localhost:7069/"));
// JsonSerializerOptions
builder.Services.AddSingleton(sp => new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
//SweetAlert2
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
