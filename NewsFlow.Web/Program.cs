﻿using Microsoft.EntityFrameworkCore;
using NewsFlow.Data.NewsFlowDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Development.json");


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RssDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("RssDbConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

