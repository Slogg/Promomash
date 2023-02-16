using AutoMapper;
using FluentValidation;
using Promomash.Infrastructure;
using Promomash.Web;
using Promomash.Web.Middlewares;
using Promomash.Web.Models.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddControllersWithViews();
builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterModelValidator>();

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddCoreServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<AppDbContext>();
        await AppDbContextSeed.SeedAsync(catalogContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();