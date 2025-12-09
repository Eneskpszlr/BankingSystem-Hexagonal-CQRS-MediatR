using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using BankingHexagonal.Application.DependencyResolvers;
using BankingHexagonal.Persistence.DependencyResolvers;
using BankingHexagonal.WebApi;
using BankingHexagonal.Persistence.EFData;
using Microsoft.EntityFrameworkCore;
namespace BankingHexagonal.ApiHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommand).Assembly);
            });
            builder.Services.AddRepositoryService();
            builder.Services.AddDbContextServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddDbContext<MyContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

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
