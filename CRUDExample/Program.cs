using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using static System.Net.Mime.MediaTypeNames;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//add services into IoC container
builder.Services.AddSingleton<ICountriesService, CountriesService>();
builder.Services.AddSingleton<IPersonsService, PersonsService>();

builder.Services.AddDbContext<PersonsDbContext>
(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = PersonsDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate=False; Application Intent = ReadWrite; Multi Subnet Failover=False
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();