using Demo_ASP_FirstTry.App.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<SQLConnectionFactory>();
builder.Services.AddScoped<ContactRepository>();

var app = builder.Build();


app.Use(async (context, next) =>
{
    Stopwatch sw = Stopwatch.StartNew();


    await next();


    string? url = context.Request.Path;


    sw.Stop();

    Console.WriteLine($"Request: {url,50} - Temps {sw.ElapsedMilliseconds} ms");
});


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
    name: "Blog",
    pattern: "/blog/{slug}",
    defaults: new {
        controller = "Blog",
        action = "Article",
    });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
