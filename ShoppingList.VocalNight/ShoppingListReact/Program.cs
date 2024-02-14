using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoppingListReact.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
const string corsPolicy = "any origin";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy, policy =>
        policy.WithOrigins("http://localhost:3000")
        .WithMethods("PUT", "POST", "GET", "DELETE", "OPTIONS")
          .WithHeaders("Content-Type", "Authorization"));

});
builder.Services.AddDbContext<ShoppingListContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();
app.UseCors(corsPolicy);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = scope.ServiceProvider.GetRequiredService<ShoppingListContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

app.MapPost("/shoppingItems", async ( Item ShoppingItem, ShoppingListContext db ) =>
{

    if (ShoppingItem.Name.IsNullOrEmpty() )
    {
        return Results.NoContent();
    }

    db.ShoppingListItems.Add(ShoppingItem);
    await db.SaveChangesAsync();

    return (Results.Created($"/ShoppingList/{ShoppingItem.Id}", ShoppingItem));
});

app.MapGet("/shoppingItems", async ( ShoppingListContext db ) =>
    await db.ShoppingListItems.ToListAsync());

app.MapDelete("/shoppingItems/{id}", async ( int id, ShoppingListContext db ) =>
{
    if (await db.ShoppingListItems.FindAsync(id) is Item item)
    {
        db.ShoppingListItems.Remove(item);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.MapPut("/shoppingItems/{id}", async ( int id, Item shoppingItem, ShoppingListContext db ) =>
{
    var item = await db.ShoppingListItems.FindAsync(id);

    Console.Write(item.IsPickedUp);
    Console.Write(shoppingItem.IsPickedUp);

    item.Name = shoppingItem.Name;
    item.IsPickedUp = shoppingItem.IsPickedUp;

    await db.SaveChangesAsync();
    return Results.NoContent();
});


app.UseHttpsRedirection();

app.Run();

