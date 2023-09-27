using Microsoft.EntityFrameworkCore;
using NewsFlow.Application.UseCases.AddFeeds;
using NewsFlow.Application.UseCases.DeleteFeeds;
using NewsFlow.Application.UseCases.LoadFeeds;
using NewsFlow.Data.Infrastructure;
using NewsFlow.Data.Repositories.FeedRepository;
using NewsFlow.Web.Mapping.FeedMapping;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Development.json");


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RssDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("RssDbConnectionString")));
builder.Services.AddScoped<IAsyncFeedRepository, AsyncFeedRepository>();
builder.Services.AddScoped<IGetFeeds, GetFeeds>();
builder.Services.AddScoped<IAddFeeds, AddFeeds>();
builder.Services.AddScoped<IDeleteFeeds, DeleteFeeds>(); 
builder.Services.AddScoped<IFeedMapper, FeedMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Feeds}/{action=ListFeeds}");
app.Run();

