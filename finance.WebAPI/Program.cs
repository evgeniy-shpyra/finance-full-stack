
using finance.bll.Services;
using finance.BLL.Common.Mappings;
using finance.BLL.Services.Interfaces;
using finance.DLL;
using finance.DLL.Models;
using finance.DLL.Repository;
using finance.DLL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace finance.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(FinanceMapping));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<BaseRepository<int, History>, HistoryRepository>();
            builder.Services.AddScoped<BaseRepository<int, FinancialCategory>, FinancialCategoryRepository>();
            builder.Services.AddScoped<BaseRepository<int, Wallet>, WalletsRepository>();
            builder.Services.AddScoped<BaseRepository<int, Transaction>, TransactionsRepository>();
            builder.Services.AddScoped<BaseRepository<int, TransactionType>, TransactionTypeRepository>();

            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IWalletService, WalletService>();
            builder.Services.AddScoped<IFinancialCategoryService, FinancialCategoryService>();
            builder.Services.AddScoped<IHistoryService, HistoryService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader());
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<FinanceContext>(options =>
                options.UseSqlServer(connectionString))
                .AddLogging(configure => configure.SetMinimumLevel(LogLevel.None));

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();

     


            app.MapControllers();

            app.Run();
        }
    }
}