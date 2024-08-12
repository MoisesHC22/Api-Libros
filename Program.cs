using gRPC.Libro.Serve;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservicio.Libro.Extensiones;
using Tienda.Microservicio.Libro.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomService(builder.Configuration);

builder.Services.AddGrpcClient<LibroImg.LibroImgClient>(o =>
{
    o.Address = new Uri("https://localhost:7209");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AlowSpecificOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
