using API_FerroLaminas;
using API_FerroLaminas.Data;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using API_FerroLaminas.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


// Configure host configuration
//esto es para ejecutar la cadena de conexion que esta en produccion
//builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
//esto ejecuta la cadena de conexion que esta en desarollo en el entorno local
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext to the services container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar MaterialService y MaterialRepository
builder.Services.AddScoped<IMaterialService, MaterialService>();

builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();

builder.Services.AddScoped<IServicioService, ServicioService>();

builder.Services.AddScoped<IServicioRepository, ServicioRepository>();

builder.Services.AddScoped<ITipoCorteService, TipoCorteService>();

builder.Services.AddScoped<ITipoCorteRepository, TipoCorteRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IRolRepository, RolRepository>();

builder.Services.AddScoped<ICotizacionService, CotizacionService>();

builder.Services.AddScoped<ICotizacionRepository, CotizacionRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<LoggingMiddleware>(); // Agregar el middleware personalizado aquí
app.UseRouting();
app.UseAuthorization();

app.UseCors(options =>
{
    options.WithOrigins("https://localhost:7219", "https://laminasequipo1.azurewebsites.net") // Reemplaza esto con tu origen permitido
           .AllowAnyHeader()
           .AllowAnyMethod();
});



//este es el archivo de las rutas
app.MapControllers();

app.Run();
