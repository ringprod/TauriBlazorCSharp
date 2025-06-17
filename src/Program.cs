using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using src;
using src.Helpers; // Add this using

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Remove the old helper and add the new bridge
// builder.Services.AddScoped<ItauriHelper,TauriHelper>();
builder.Services.AddSingleton<TauriBridge>(); // Add this line

await builder.Build().RunAsync();
