using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShoppingList.Data;
using ShoppingList.Repositories;
using ShoppingList.Services;
using ShoppingList.Utils;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicy = "corspolicy";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "Shopping List API",
            Description = "A Web API for for the Shopping List Web App",
            Contact = new OpenApiContact
            {
                Name = "Kim Fom",
                Email = "kimfom01@gmail.com",
                Url = new Uri("https://kimfom.space")
            },
            License = new OpenApiLicense
            {
                Name = "MIT Licence",
                Url = new Uri("https://opensource.org/license/mit/")
            }
        }
    );

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: corsPolicy,
        policy => policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod()
    );
});
builder.Services.AddDbContext<ShoppingListDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ShoppingListDb"));
    // options.UseInMemoryDatabase("ShoppingListDb");
});
builder.Services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
builder.Services.AddScoped<IShoppingListService, ShoppingListService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.MapControllers();

app.Setup(builder.Environment);

app.Run();