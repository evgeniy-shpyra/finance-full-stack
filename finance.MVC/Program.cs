using finance.BLL.Services.Interfaces;
using finance.bll.Services;
using finance.DLL.Repository.Interfaces;
using finance.DLL.Repository;
using finance.DLL.UnitOfWork;
using finance.DLL;
using Microsoft.EntityFrameworkCore;
using finance.DLL.Models;
using finance.BLL.Common.Mappings;

namespace finance.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


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

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<FinanceContext>(options =>
                options.UseSqlServer(connectionString))
                .AddLogging(configure => configure.SetMinimumLevel(LogLevel.None));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Wallet}/{action=WalletList}/{id?}");

            app.Run();
        }
    }
}