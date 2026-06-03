using Microsoft.EntityFrameworkCore;
using ShoppingList.NathanJenner.Server.Data;
using ShoppingList.NathanJenner.Server.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!await db.ShoppingItems.AnyAsync())
    {
        db.ShoppingItems.AddRange(
            new ShoppingItem { Name = "Milk",   Quantity = 2, SortOrder = 0 },
            new ShoppingItem { Name = "Bread",  Quantity = 1, SortOrder = 1 },
            new ShoppingItem { Name = "Eggs",   Quantity = 12, SortOrder = 2 }
        );
        await db.SaveChangesAsync();
    }
}

app.UseDefaultFiles();
app.MapStaticAssets();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var items = app.MapGroup("/api/items");

items.MapGet("/", async (AppDbContext db) =>
    await db.ShoppingItems
        .OrderBy(i => i.SortOrder)
        .ToListAsync());

items.MapPost("/", async (CreateItemDto dto, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(dto.Name) || dto.Quantity < 0)
        return Results.BadRequest("Name is required and quantity must be non-negative.");

    var maxOrder = await db.ShoppingItems.MaxAsync(i => (int?)i.SortOrder);

    var item = new ShoppingItem
    {
        Name = dto.Name,
        Quantity = dto.Quantity,
        SortOrder = (maxOrder ?? -1) + 1,
    };

    db.ShoppingItems.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/items/{item.Id}", item);
});

items.MapPut("/{id:int}", async (int id, UpdateItemDto dto, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(dto.Name) || dto.Quantity < 0)
        return Results.BadRequest("Name is required and quantity must be non-negative.");

    var item = await db.ShoppingItems.FindAsync(id);
    if (item is null) return Results.NotFound();

    item.Name = dto.Name;
    item.Quantity = dto.Quantity;

    await db.SaveChangesAsync();
    return Results.Ok(item);
});

items.MapDelete("/{id:int}", async (int id, AppDbContext db) =>
{
    var item = await db.ShoppingItems.FindAsync(id);
    if (item is null) return Results.NotFound();

    db.ShoppingItems.Remove(item);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

items.MapPatch("/{id:int}/toggle", async (int id, AppDbContext db) =>
{
    var item = await db.ShoppingItems.FindAsync(id);
    if (item is null) return Results.NotFound();

    item.IsPurchased = !item.IsPurchased;
    await db.SaveChangesAsync();
    return Results.Ok(item);
});

items.MapPut("/reorder", async (List<ReorderItemDto> updated, AppDbContext db) =>
{
    var ids = updated.Select(u => u.Id).ToList();
    var itemMap = await db.ShoppingItems
        .Where(i => ids.Contains(i.Id))
        .ToDictionaryAsync(i => i.Id);

    foreach (var incoming in updated)
    {
        if (itemMap.TryGetValue(incoming.Id, out var item))
            item.SortOrder = incoming.SortOrder;
    }

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapFallbackToFile("/index.html");

app.Run();

