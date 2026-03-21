using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.PowerShell.Blazor.Wasm.Demo;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

await builder.Build().RunAsync();
