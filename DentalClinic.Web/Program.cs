using DentalClinic.Web.Configurations;
using DentalClinic.Web.Infrastructure.Http;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<JwtAuthorizationHandler>();

builder.Services.AddHttpClient("ApiClient", (serviceProvider, client) =>
{
    var settings = serviceProvider
        .GetRequiredService<IOptions<ApiSettings>>().Value;

    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthorizationHandler>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "Cookies";
})
.AddCookie("Cookies", options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
})
.AddJwtBearer("ApiBearer", options =>
{
    options.Authority = "http://localhost:7248"; // آدرس API
    options.Audience = "DentalClinicApi";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireDentistRole", policy =>
        policy.RequireRole("Dentist"));
});

builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
