using Microsoft.EntityFrameworkCore;
using ShoppingList.StevieTV.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ShoppingListContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingListContext") ?? throw new InvalidOperationException("Connection String not found")));

builder.Services.AddScoped<ShoppingListContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Services.CreateScope().ServiceProvider.GetService<ShoppingListContext>().Database.EnsureCreated();
}

app.UseCors(options => 
    options.WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();