using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RouletteApi.Context;
using RouletteApi.Models;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DatabaseContext>();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddSimpleConsole(c => c.SingleLine = true);

var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();