
using PreNet_3.Data;
using PreNet_3.Data.Repositories;
using PreNet_3.Services;

namespace PreNet_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<InMemoryDataStore>();
            builder.Services.AddSingleton<IAuthorRepository, AuthorRepository>();
            builder.Services.AddSingleton<IBookRepository, BookRepository>();
            builder.Services.AddSingleton<IAuthorService, AuthorService>();
            builder.Services.AddSingleton<IBookService, BookService>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.Run();
        }
    }
}
