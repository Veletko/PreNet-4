using Microsoft.EntityFrameworkCore;
using PreNet_3.Data;
using PreNet_3.Data.Repositories;
using PreNet_3.Models;
using PreNet_3.Services;

namespace PreNet_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BookService>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();

                try
                {
                    if (context.Database.CanConnect())
                    {
                        Console.WriteLine("Успешное подключение к базе данных!");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось подключиться к базе данных");
                        Console.WriteLine("Выполните команды миграции:");
                        Console.WriteLine("dotnet ef migrations add InitialCreate");
                        Console.WriteLine("dotnet ef database update");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка подключения к базе: {ex.Message}");
                    Console.WriteLine("Выполните команды миграции:");
                    Console.WriteLine("dotnet ef migrations add InitialCreate");
                    Console.WriteLine("dotnet ef database update");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}