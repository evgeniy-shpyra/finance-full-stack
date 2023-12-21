using AutoMapper;
using finance.bll.Services;
using finance.BLL.Common.Mappings;
using finance.BLL.Services.Interfaces;
using finance.DLL;
using finance.DLL.Models;
using finance.DLL.Repository;
using finance.DLL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace financeProgram
{
    class Program
    {
        static void Main(string[] args)
        {

            IHost host = CreateHostBuilder(args).Build();
            var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                Console.Clear();

                services.GetRequiredService<App>().Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] strings)
        {
            var databaseName = "net7financedb";
            var connectionString = $"Server=(localdb)\\mssqllocaldb;Database={databaseName};Trusted_Connection=True;MultipleActiveResultSets=true";
            return Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddScoped<IUnitOfWork, UnitOfWork>();

                    services.AddScoped<BaseRepository<int, History>, HistoryRepository>();
                    services.AddScoped<BaseRepository<int, FinancialCategory>, FinancialCategoryRepository>();
                    services.AddScoped<BaseRepository<int, Wallet>, WalletsRepository>();
                    services.AddScoped<BaseRepository<int, Transaction>, TransactionsRepository>();
                    services.AddScoped<BaseRepository<int, TransactionType>, TransactionTypeRepository>();

                    services.AddScoped<ITransactionService, TransactionService>();
                    services.AddScoped<IWalletService, WalletService>();
                    services.AddScoped<IFinancialCategoryService, FinancialCategoryService>();
                    services.AddScoped<IHistoryService, HistoryService>();

                    
                    services.AddDbContext<FinanceContext>(options =>
                        options.UseSqlServer(connectionString))
                        .AddLogging(configure => configure.SetMinimumLevel(LogLevel.None));
                    services.AddScoped<App>();

                    var mapperConfig = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<FinanceMapping>();
                    });

                    IMapper mapper = mapperConfig.CreateMapper();
                    services.AddSingleton(mapper);

                });
        }
    }                                       
}
