using InternetShopAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем DbContext до Build()
builder.Services.AddDbContext<InternetShopContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.MapGet("/product", () =>
// {
//     return new
//     {
//         Id = 1,
//         Name = "Test Product",
//         Price = 9.99
//     };
// });

// app.UseHttpsRedirection();
// app.UseAuthorization();
app.Run();