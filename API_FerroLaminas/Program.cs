using API_FerroLaminas;
using API_FerroLaminas.Data;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using API_FerroLaminas.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


// Configure host configuration
//this is for excuting appsetings in production environment
//builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
//this is for executing appsetings in development,which runs in localhost
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

// Register MaterialService y MaterialRepository
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

builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IUbicacionesService, UbicacionesService>();

builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();

builder.Services.AddScoped<IProyectoService, ProyectoService>();

builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();

builder.Services.AddScoped<ICalibreService, CalibreService>();

builder.Services.AddScoped<ICalibreRepository, CalibreRepository>();

builder.Services.AddScoped<IOrdenDeTrabajoService, OrdenDeTrabajoService>();

builder.Services.AddScoped<IOrdenDeTrabajoRepository, OrdenDeTrabajoRepository>();

builder.Services.AddScoped<ISeguimientoService, SeguimientoService>();

builder.Services.AddScoped<ISeguimientoRepository, SeguimientoRepository>();

builder.Services.AddScoped<EstadoOrdenTrabajoService, EstadoOrdenTrabajoService>();

builder.Services.AddScoped<EstadoOrdenTrabajoRepository, EstadoOrdenTrabajoRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<LoggingMiddleware>(); // Add custom middleware here
app.UseRouting();
app.UseAuthorization();

app.UseCors(options =>
{
    options.WithOrigins("https://localhost:7219", "https://laminasequipo1.azurewebsites.net") // allowed origin in here
           .AllowAnyHeader()
           .AllowAnyMethod();
});



//callig controllers
app.MapControllers();

app.Run();
