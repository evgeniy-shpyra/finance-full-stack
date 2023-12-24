using finance.WebAPI.Client.Models;

namespace finance.WebAPI.Client
{
    internal class App
    {

        private API Api { get; set; }

    

        public async void Start ()
        {
           
            this.Api = new API("https://localhost:7012/api");

            while (true)
            {
                Console.Clear();

                List<string> options = new List<string>
            {
                "1 - wallet replenishment",
                "2 - withdrawal of funds",
                "3 - transaction between wallets",
                "4 - create wallet",
                "5 - delete wallet",
                "6 - print financial category",
                "7 - create financial category",
                "8 - delete financial category",
                "9 - history",
            };

                int action = CreateMenu(options, PrintWallets);

                switch (action)
                {
                    case 1:
                        WalletReplenishment();
                        break;
                    case 2:
                        BudgetWithdrawal();
                        break;
                    case 3:
                        BetweenWallets();
                        break;
                    case 4:
                        CretaeWallet();
                        break;
                    case 5:
                        DeleteWallet();
                        break;
                    case 6:
                        Console.Clear();
                        PrintCategory();
                        Console.ReadKey();
                        break;
                    case 7:
                        CreateCategory();
                        break;
                    case 8:
                        DeleteCategory();
                        break;
                    case 9:
                        PrintHistory();
                        break;
                    case 0:
                        return;
                }
            }
        }

       


        private void PrintWallets()
        {
            List<Wallet> wallets = Api.FetchWallets();

            if (wallets.Count == 0)
            {
                Console.WriteLine("You don't have a wallet");
            }
            else
            {
                Console.WriteLine("Your wallets:");
                foreach (Wallet wallet in wallets)
                {
                    Console.WriteLine($"id: {wallet.Id}, name: {wallet.Name}, balance: {wallet.Balance}");
                }
            }
            Console.WriteLine("");
        }

        private void CretaeWallet()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Creating wallet");
                Console.Write("Enter 'exit' for exit\nEnter wallet's name: ");
                string walletName = Console.ReadLine();

                if (walletName == "exit")
                {
                    return;
                }

                if (walletName.Length > 0)
                {
                    var newWallet = new CreateWalletDTO { Name = walletName };

                    Console.Clear();
                    Task.Run(() => Api.CreateWallet(newWallet));
                    Console.ReadKey();
                    return;
                }
            }
        }

        private void DeleteWallet()
        {

            List<Wallet> wallets = Api.FetchWallets();

            List<string> options = new List<string> { };

            if (wallets.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You don't hava a wallet");
                Console.ReadKey();
                return;
            }

            foreach (Wallet wallet in wallets)
            {
                options.Add($"name: {wallet.Name}, balance: {wallet.Balance}");
            }

            int action = CreateMenu(options, null, "Select a wallet for deleting");

            if (action == 0)
            {
                return;
            }


            Console.Clear();
            Task.Run(() => Api.DeleteWallet(wallets[--action].Id));

            Console.ReadKey();

        }


        private bool PrintCategory()
        {
            List<FinancialCategory> categories = Api.FetchCategories();

            if (categories.Count == 0)
            {
                Console.WriteLine("You don't have a categories\n");
                return false;
            }
            else
            {
                Console.WriteLine("Your categories:");
                foreach (FinancialCategory category in categories)
                {
                    Console.WriteLine($"id: {category.Id}, name: {category.Name}");
                }
                return true;
            }
        }

        private void CreateCategory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Creating category\n");
                Console.Write("Enter 'exit' for exit\nEnter categore's name: ");
                string name = Console.ReadLine();

                if (name == "exit")
                {
                    return;
                }

                if (name.Length > 0)
                {
                    var newCategory = new CreateCategoryDTO();
                    newCategory.Name = name;

                    Console.Clear();
                    Task.Run(() => Api.CreateCategory(newCategory));
                    Console.ReadKey();
                    return;
                }
            }
        }

        private void DeleteCategory()
        {

            List<FinancialCategory> categores = Api.FetchCategories();
            List<string> options = new List<string> { };

            if (categores.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You don't hava a category");
                Console.ReadKey();
                return;
            }


            foreach (FinancialCategory category in categores)
            {
                options.Add($"name: {category.Name}");
            }

            int action = CreateMenu(options, null, "Select a category for deleting");

            if (action == 0)
            {
                return;
            }

            Console.Clear();
            Task.Run(() => Api.DeleteCategory(categores[--action].Id));

            Console.ReadKey();

        }


        private void PrintHistory()
        {
            void printTransaction(List<History> historyes)
            {
                foreach (History his in historyes)
                {
                    if (his.WalletTo.Length != 0 && his.WalletFrom.Length != 0)
                    {
                        Console.WriteLine($"Type: transaction between wallets\nSending wallet: {his.WalletFrom}\nReceiving wallet: {his.WalletTo}\nFinancial category: {his.Category}\nAmount: {his.Price}\nLeft balance: {his.LeftBalance}\nTime: {his.CreateAt}\n");
                    }
                    else if (his.WalletTo.Length != 0)
                    {

                        Console.WriteLine($"Type: balance replenishment\nReceiving wallet: {his.WalletTo}\nFinancial category: {his.Category}\nAmount: {his.Price}\nLeft balance: {his.LeftBalance}\nTime: {his.CreateAt}\n");
                    }
                    else if (his.WalletFrom.Length != 0)
                    {
                        Console.WriteLine($"Type: withdrawal of funds\nSending wallet: {his.WalletFrom}\nFinancial category: {his.Category}\nAmount: {his.Price}\nLeft balance: {his.LeftBalance}\nTime: {his.CreateAt}\n");
                    }
                }
            }

            Console.Clear();

         
            List<History> history = Api.FetchHistory(new FetchHistoryParams());

            if (history.Count == 0)
            {
                Console.WriteLine("Transaction history is empty\n");
                Console.ReadKey();
                return;
            }




            List<string> options = new List<string> { "print all history", "print history by financial category", "print history by transaction type" };
            var allCategories = Api.FetchCategories();
            var transactionTypes = Api.FetchTransactionTypes();


            while (true)
            {
                int action = CreateMenu(options, null, "Select action:\n");
               

                switch (action)
                {
                    case 0: return;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Transaction history:\n");
                        printTransaction(history);
                        Console.ReadKey();

                        break;

                    case 2:

                        List<string> categoryOptions = new List<string> { };

                        foreach (FinancialCategory category in allCategories)
                        {
                            categoryOptions.Add(category.Name);
                        }

                        int selectedCategoryIndex = CreateMenu(categoryOptions, null, "Select a financial category:\n");
                        if (selectedCategoryIndex == 0)
                        {
                            break;
                        }
                        selectedCategoryIndex--;

                        List<History> historyByCategory = Api.FetchHistory(new FetchHistoryParams { categoryId = allCategories[selectedCategoryIndex].Id });

                        if (historyByCategory.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine($"Transaction history by '{allCategories[selectedCategoryIndex].Name}' category is empty");
                            Console.ReadKey();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine($"Transaction history by '{allCategories[selectedCategoryIndex].Name}' category:\n");
                        printTransaction(historyByCategory);

                        Console.ReadKey();
                        break;

                    case 3:

                        List<string> typesOptions = new List<string> { };

                        foreach (TransactionType type in transactionTypes)
                        {
                            typesOptions.Add(type.Name);
                        }

                        int selectedTypeIndex = CreateMenu(typesOptions, null, "Select a transaction type:\n");
                        if (selectedTypeIndex == 0)
                        {
                            break;
                        }
                        selectedTypeIndex--;

                        List<History> historyByType = Api.FetchHistory(new FetchHistoryParams { transactionTypeId = transactionTypes[selectedTypeIndex].Id });

                        if (historyByType.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine($"Transaction history by '{transactionTypes[selectedTypeIndex].Name}' type is empty");
                            Console.ReadKey();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine($"Transaction history by '{transactionTypes[selectedTypeIndex].Name}' type:\n");
                        printTransaction(historyByType);

                        Console.ReadKey();
                        break;
                }



            }

        }


        private void WalletReplenishment()
        {
            var wallets = Api.FetchWallets();
            var walletOptions = new List<string> { };

            if (wallets.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You don't hava a wallet");
                Console.ReadKey();
                return;
            }

            foreach (Wallet wallet in wallets)
            {
                walletOptions.Add($"name: {wallet.Name}, balance: {wallet.Balance}");
            }

            int walletIndex = CreateMenu(walletOptions, null, "Select a wallet\n");
            if (walletIndex == 0) return;

            Wallet selectedWallet = wallets[walletIndex - 1];

            var categores = Api.FetchCategories();
            var categoryOptions = new List<string> { };

            if (categores.Count == 0)
            {
                bool isLoop = true;
                while (isLoop)
                {

                    Console.WriteLine("You don't have categry\n\nDo you want to create one?");

                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Enter:
                            CreateCategory();
                            categores = Api.FetchCategories();
                            isLoop = false;
                            break;
                        case ConsoleKey.Escape:
                            return;
                    }
                }
            }
            foreach (FinancialCategory category in categores)
            {
                categoryOptions.Add($"name: {category.Name}");
            }

            int action = CreateMenu(categoryOptions, null, "Select a category\n");

            if (action == 0)
            {
                return;
            }

            FinancialCategory selectedCategory = categores[--action];


            while (true)
            {
                Console.Clear();


                Console.Write("Enter the amount of the deposit: ");
                string enter = Console.ReadLine();
                if (enter == "exit") return;

                if (decimal.TryParse(enter, out decimal money))
                {

                    var transaction = new CreateTransactionDTO { Price = money, FinancialCategoryId = selectedCategory.Id, ReceivingWalletId = selectedWallet.Id };

                    Console.Clear();
                    Task.Run(() => Api.CreateTransaction(transaction));
                    Console.ReadKey();

                    return;
                }
                
            }
        }

        private void BetweenWallets()
        {

            var wallets = Api.FetchWallets();

            List<string> walletOptions = new List<string> { };

            if (wallets.Count < 2)
            {
                Console.Clear();
                Console.WriteLine("You don't hava 2 walletes");
                Console.ReadKey();
                return;
            }

            foreach (Wallet wallet in wallets)
            {
                walletOptions.Add($"name: {wallet.Name}, balance: {wallet.Balance}");
            }

            int sendingWalletIndex = CreateMenu(walletOptions, null, "Select sending wallet\n");

            if (sendingWalletIndex == 0) return;

            Wallet sendingWallet = wallets[sendingWalletIndex - 1];

            walletOptions.RemoveAt(sendingWalletIndex - 1);
            wallets.RemoveAt(sendingWalletIndex - 1);

            int receivingWalletIndex = CreateMenu(walletOptions, null, "Select receiving wallet\n");

            Wallet receivingWallet = wallets[receivingWalletIndex - 1];


            while (true)
            {
                Console.Clear();

                Console.WriteLine($"Sending wallet: {sendingWallet.Name}, balance: {sendingWallet.Balance}");
                Console.WriteLine($"Receiving: wallet: {receivingWallet.Name}, balance: {receivingWallet.Balance}\n");


                Console.Write("Enter exut to exit\nEnter the amount to transfer: ");
                string enter = Console.ReadLine();
                if (enter == "exit") return;

                if (decimal.TryParse(enter, out decimal money))
                {
                    Console.Clear();
                    if (sendingWallet.Balance >= money)
                    {
                        var transaction = new CreateTransactionDTO { Price = money, SendingWalletId = sendingWallet.Id, ReceivingWalletId = receivingWallet.Id };

                        Console.Clear();
                        Task.Run(() => Api.CreateTransaction(transaction));
                        
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough funds");
                    }
                    Console.ReadKey();
                    return;
                }
            }

        }

        private void BudgetWithdrawal()
        {

            var wallets = Api.FetchWallets();
            List<string> walletOptions = new List<string> { };

            if (wallets.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You don't hava a wallet");
                Console.ReadKey();
                return;
            }

            foreach (Wallet wallet in wallets)
            {
                walletOptions.Add($"name: {wallet.Name}, balance: {wallet.Balance}");
            }

            int walletIndex = CreateMenu(walletOptions, null, "Select a wallet\n");

            if (walletIndex == 0) return;

            Wallet selectedWallet = wallets[walletIndex - 1];

            Console.Clear();

            var categores = Api.FetchCategories();
            var categoryOptions = new List<string> { };

            if (categores.Count == 0)
            {
                bool isLoop = true;
                while (isLoop)
                {

                    Console.WriteLine("You don't have categry\n\nDo you want to create one?");

                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Enter:
                            CreateCategory();
                            categores = Api.FetchCategories();
                            isLoop = false;
                            break;
                        case ConsoleKey.Escape:
                            return;
                    }
                }
            }

            foreach (FinancialCategory category in categores)
            {
                categoryOptions.Add($"name: {category.Name}");
            }

            int action = CreateMenu(categoryOptions, null, "Select a category\n");

            if (action == 0)
            {
                return;
            }

            var selectedCategory = categores[--action];



            while (true)
            {
                Console.Clear();

                Console.WriteLine($"Wallet: name: {selectedWallet.Name}, balance: {selectedWallet.Balance}");
                Console.WriteLine($"Category: name: {selectedCategory.Name}\n");


                Console.Write("Enter exut to exit\nEnter the amount to transfer: ");
                string enter = Console.ReadLine();
                if (enter == "exit") return;

                if (decimal.TryParse(enter, out decimal money))
                {
                    Console.Clear();
                    if (selectedWallet.Balance >= money)
                    {
                        var transaction = new CreateTransactionDTO { Price = money, FinancialCategoryId = selectedCategory.Id, SendingWalletId = selectedWallet.Id };

                        Console.Clear();
                        Task.Run(() => Api.CreateTransaction(transaction));
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough funds");
                    }
                    Console.ReadKey();
                    return;
                }
            }


        }


        private int CreateMenu(List<string> options, Action printFunction = null, string title = null)
        {
            int optionInFocus = 0;
            while (true)
            {
                Console.Clear();

                if (printFunction != null)
                {
                    printFunction();
                }

                if (title != null)
                {
                    Console.WriteLine(title);
                }

                foreach (string option in options)
                {
                    int index = options.IndexOf(option);
                    string prefix = index == optionInFocus ? "->" : "  ";
                    Console.WriteLine($"{prefix} {option}");
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (optionInFocus > 0)
                        {
                            optionInFocus--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (optionInFocus < options.Count - 1)
                        {
                            optionInFocus++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        return optionInFocus + 1;
                    case ConsoleKey.Escape:
                        return 0;

                }


            }
        }
    }


}
