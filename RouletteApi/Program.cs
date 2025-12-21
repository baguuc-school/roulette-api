using Microsoft.Extensions.Hosting;
using RouletteApi.Context;
using RouletteApi.Models;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DatabaseContext>();
builder.Logging.AddSimpleConsole(c => c.SingleLine = true);

var app = builder.Build();
app.MapGet("/", (DatabaseContext database) =>
{
    var record = from roulette in database.Set<Roulette>()
                 join item in database.Set<Item>()
                 on roulette.Id equals item.RouletteId into grouping
                 select new { 
                     Id = roulette.Id, 
                     Name = roulette.Name, 
                     Items = from item in grouping 
                             select new { 
                                 Id = item.Id,
                                 Name = item.Name,
                                 Value = item.Value
                             } 
                 };

    return Results.Ok(record);
});
app.MapPost("/", (DatabaseContext database) =>
{
    database.Add(new Roulette
    {
        Id = 1,
        Name = "Podstawowa",
        Items = [
            new Item { Id = 1, Name = "Itemek 1", Value = 2 },
            new Item { Id = 2, Name = "Itemek 2", Value = 8 },
            new Item { Id = 3, Name = "Itemek 3", Value = 5 }
        ]
    });
    database.SaveChanges();

    return Results.Ok();
});
app.Run();
