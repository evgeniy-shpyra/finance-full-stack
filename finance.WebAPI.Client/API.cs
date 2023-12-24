using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using finance.WebAPI.Client.Models;
using Newtonsoft.Json;

namespace finance.WebAPI.Client
{
    public class API
    {
        private string baseUrl;

        public API(string baseUrl) {
            this.baseUrl = baseUrl;
        }
 
        public List<Wallet> FetchWallets()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{baseUrl}/Wallet").Result;
                var result = response.Content.ReadAsAsync<List<Wallet>>().Result;
                return result;
            }
        }

        public async Task CreateWallet(CreateWalletDTO wallet)
        {
            using (var client = new HttpClient())
            {
                var jsonWallet = JsonConvert.SerializeObject(wallet);
                var content = new StringContent(jsonWallet, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{baseUrl}/Wallet", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Wallet was created success");
                }
                else
                {
                    Console.WriteLine("An error occur");
                }
               
            }
        }

        public async Task DeleteWallet(int walletId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{baseUrl}/Wallet/{walletId}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Wallet was deleted successfully");
                }
                else
                {
                    Console.WriteLine("An error occurred while deleting the wallet");
                }
            }
        }


        public List<FinancialCategory> FetchCategories()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{baseUrl}/FinancialCategory").Result;
                var result = response.Content.ReadAsAsync<List<FinancialCategory>>().Result;
                return result;
            }
        }

        public async Task CreateCategory(CreateCategoryDTO category)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(category);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{baseUrl}/FinancialCategory", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Financial category was created success");
                }
                else
                {
                    Console.WriteLine("An error occur");
                }

            }
        }
        
        public async Task DeleteCategory(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{baseUrl}/FinancialCategory/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Financial category was deleted successfully");
                }
                else
                {
                    Console.WriteLine("An error occurred while deleting the category");
                }
            }
        }

        
        public List<History> FetchHistory(FetchHistoryParams queyParams)
        {
            string queryUrl = "";
            if (queyParams.categoryId != null)
            {
                queryUrl = $"/ByCategory/{queyParams.categoryId}";
            }
            else if (queyParams.transactionTypeId != null)
            {
                queryUrl = $"/ByTransactionType/{queyParams.transactionTypeId}";
            }

            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{baseUrl}/History{queryUrl}").Result;
                var result = response.Content.ReadAsAsync<List<History>>().Result;
                return result;
            }
        }


        public List<TransactionType> FetchTransactionTypes()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{baseUrl}/Transaction/type").Result;
                var result = response.Content.ReadAsAsync<List<TransactionType>>().Result;
                return result;
            }
        }


        public async Task CreateTransaction(CreateTransactionDTO transaction)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(transaction);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{baseUrl}/Transaction", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Transaction was created success");
                }
                else
                {
                    Console.WriteLine("An error occur");
                }

            }
        }

    }
}
