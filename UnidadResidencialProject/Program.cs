using UnidadResidencialProject.Components;
using UnidadResidencialProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7051/")
});
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IngresoService>();
builder.Services.AddScoped<ResidenteService>();
builder.Services.AddScoped<ParqueaderoService>(); // Re-add if it was here, I see we have AppDtos.cs with Parqueadero
builder.Services.AddScoped<MantenimientoService>();
builder.Services.AddScoped<MensajeriaService>();
builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<DashboardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
