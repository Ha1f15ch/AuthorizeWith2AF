using DatabaseEngine;
using DatabaseEngine.InitializeData;
using DatabaseEngine.Repository;
using DatabaseEngine.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Подключаем файл конфигурации
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container. 
// Подключение к базе данных
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(connectionStrings);
});

// Остальные контейнеры
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Настройки для роутинга
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	try
	{
		var context = services.GetRequiredService<AppDbContext>();
		await RoleDataInitialize.Initialize(context);
	}
	catch (Exception ex)
	{
		Console.WriteLine($"При выполнении записи данных по умолчанию возникла ошибка - {ex.Message}");
	}
}

	app.Run();
