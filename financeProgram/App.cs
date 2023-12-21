using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using System.Collections.Generic;
using System;
using finance.BLL.ModelsDTO;
using System.Linq;

namespace financeProgram
{
    public class App
    {
        private readonly IWalletService walletService;
        private readonly IFinancialCategoryService categoryService;
        private readonly IHistoryService historyService;
        private readonly ITransactionService transactionService;

        //public App(IWalletService walletService, IFinancialCategoryService categoryService, IHistoryService historyService, ITransactionService transactionService)
        public App(IWalletService walletService, IFinancialCategoryService categoryService, IHistoryService historyService, ITransactionService transactionService)
        {
            this.walletService = walletService;
            this.categoryService = categoryService;
            this.historyService = historyService;
            this.transactionService = transactionService;
        }

        public void Run()
        {
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

        private void PrintWallets()
        {
            IEnumerable<WalletDTO> walletsEnumerable = walletService.GetAll();
            List<WalletDTO> wallets = new List<WalletDTO>(walletsEnumerable);

            if (wallets.Count == 0)
            {
                Console.WriteLine("You don't have a wallet");
            }
            else
            {
                Console.WriteLine("Your wallets:");
                foreach (WalletDTO wallet in wallets)
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
                    CreateWalletDTO newWallet = new CreateWalletDTO();
                    newWallet.Name = walletName;

                    walletService.Add(newWallet);

                    Console.Clear();
                    Console.WriteLine("Wallet was created success");
                    Console.ReadKey();
                    return;
                }
            }


        }

        private void DeleteWallet()
        {

            IEnumerable<WalletDTO> walletsEnumerable = walletService.GetAll();
            List<WalletDTO> wallets = new List<WalletDTO>(walletsEnumerable);
            List<string> options = new List<string> { };

            if (wallets.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You don't hava a wallet");
                Console.ReadKey();
                return;
            }

            foreach (WalletDTO wallet in wallets)
            {
                options.Add($"name: {wallet.Name}, balance: {wallet.Balance}");
            }

            int action = CreateMenu(options, null, "Select a wallet for deleting");

            if (action == 0)
            {
                return;
            }

            walletService.Remove(wallets[--action].Id);

            Console.Clear();
            Console.WriteLine("Wallet was deleted success");
            Console.ReadKey();

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
                    CreateFinancialCategoryDTO newCategory = new CreateFinancialCategoryDTO();
                    newCategory.Name = name;

                    categoryService.Add(newCategory);

                    Console.Clear();
                    Console.WriteLine("Category was created success");
                    Console.ReadKey();
                    return;
                }
            }
        }

        private bool PrintCategory()
        {
            List<FinancialCategoryDTO> categories = categoryService.GetAll();

            if (categories.Count == 0)
            {
                Console.WriteLine("You don't have a categories\n");
                return false;
            }
            else
            {
                Console.WriteLine("Your categories:");
                foreach (FinancialCategoryDTO category in categories)
                {
                    Console.WriteLine($"id: {category.Id}, name: {category.Name}");
                }
                return true;
            }
        }

        private void DeleteCategory()
        {

            List<FinancialCategoryDTO> categores = categoryService.GetAll();
            List<string> options = new List<string> { };

            if (categores.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You don't hava a category");
                Console.ReadKey();
                return;
            }


            foreach (FinancialCategoryDTO category in categores)
            {
                options.Add($"name: {category.Name}");
            }

            int action = CreateMenu(options, null, "Select a category for deleting");

            if (action == 0)
            {
                return;
            }

            categoryService.Remove(categores[--action]);

            Console.Clear();
            Console.WriteLine("Category was deleted success");
            Console.ReadKey();

        }

        private void PrintHistory()
        {
            void printTransaction(List<HistoryDTO> historyes)
            {
              

                foreach (HistoryDTO his in historyes)
                {


                    if (his.WalletTo != null && his.WalletFrom != null)
                    {
                        Console.WriteLine($"Type: transaction between wallets\nSending wallet: {his.WalletFrom}\nReceiving wallet: {his.WalletTo}\nAmount: {his.Price}\nLeft balance: {his.LeftBalance}\nTime: {his.CreateAt}\n");
                    }
                    else if (his.WalletTo != null)
                    {
                        Console.WriteLine($"Type: balance replenishment\nReceiving wallet: {his.WalletFrom}\nAmount: {his.Price}\nLeft balance: {his.LeftBalance}\nTime: {his.CreateAt}\n");
                    }
                    else if (his.WalletFrom != null)
                    {
                        Console.WriteLine($"Type: withdrawal of funds\nSending wallet: {his.WalletTo}\nAmount: {his.Price}\nLeft balance: {his.LeftBalance}\nTime: {his.CreateAt}\n");
                    }
                }

            
            }

            Console.Clear();
            List<HistoryDTO> history = historyService.GetAll();

            if (history.Count == 0)
            {
                Console.WriteLine("Transaction history is empty\n");
                Console.ReadKey();
                return;
            }




            List<string> options = new List<string> { "print all history", "print history by financial category", "print history by transaction type" };

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

                        FinancialCategoryDTO sysyemCategory = categoryService.GetById(1);
                        var allCategories = categoryService.GetAll().ToList();

                        allCategories.Insert(0, sysyemCategory);

                        List<string> categoryOptions = new List<string> { };

                        foreach (FinancialCategoryDTO category in allCategories)
                        {
                            categoryOptions.Add(category.Name);
                        }

                        int selectedCategoryIndex = CreateMenu(categoryOptions, null, "Select a financial category:\n");

                        if (selectedCategoryIndex == 0)
                        {
                            break;
                        }

                        selectedCategoryIndex--;

                        List<HistoryDTO> historyByCategory = historyService.GetByCategory(allCategories[selectedCategoryIndex].Name);

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

                        var transactionTypes = transactionService.GetAllTypes().ToList();

                        List<string> typesOptions = new List<string> { };

                        foreach (TransactionTypeDTO type in transactionTypes)
                        {
                            typesOptions.Add(type.Name);
                        }

                        int selectedTypeIndex = CreateMenu(typesOptions, null, "Select a transaction type:\n");

                        if (selectedTypeIndex == 0)
                        {
                            break;
                        }

                        selectedTypeIndex--;

                        List<HistoryDTO> historyByType = historyService.GetByTransactionType(transactionTypes[selectedTypeIndex].Id);

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

        private void BudgetWithdrawal()
        {

            IEnumerable<WalletDTO> walletsEnumerable = walletService.GetAll();
            List<WalletDTO> wallets = new List<WalletDTO>(walletsEnumerable);
            List<string> walletOptions = new List<string> { };

            if (wallets.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You don't hava a wallet");
                Console.ReadKey();
                return;
            }

            foreach (WalletDTO wallet in wallets)
            {
                walletOptions.Add($"name: {wallet.Name}, balance: {wallet.Balance}");
            }

            int walletIndex = CreateMenu(walletOptions, null, "Select a wallet\n");

            if (walletIndex == 0) return;

            WalletDTO selectedWallet = wallets[walletIndex - 1];

            Console.Clear();

            List<FinancialCategoryDTO> categores = categoryService.GetAll();
            List<string> categoryOptions = new List<string> { };

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
                            categores = categoryService.GetAll();
                            isLoop = false;
                            break;
                        case ConsoleKey.Escape:
                            return;
                    }
                }
            }

            foreach (FinancialCategoryDTO category in categores)
            {
                categoryOptions.Add($"name: {category.Name}");
            }

            int action = CreateMenu(categoryOptions, null, "Select a category\n");

            if (action == 0)
            {
                return;
            }

            FinancialCategoryDTO selectedCategory = categores[--action];



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
                        CreateTransactionDTO transaction = new CreateTransactionDTO();
                        transaction.Price = money;
                      //  transaction.FinancialCategory = selectedCategory;
                      //  transaction.SendingWallet = selectedWallet;
                      //  transaction.ReceivingWallet = null;

                        transactionService.Add(transaction);

                        Console.WriteLine("The funds have been successfully debited");
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

        private void BetweenWallets()
        {
            IEnumerable<WalletDTO> walletsEnumerable = walletService.GetAll();
            List<WalletDTO> wallets = new List<WalletDTO>(walletsEnumerable);

            List<string> walletOptions = new List<string> { };

            if (wallets.Count < 2)
            {
                Console.Clear();
                Console.WriteLine("You don't hava 2 walletes");
                Console.ReadKey();
                return;
            }

            foreach (WalletDTO wallet in wallets)
            {
                walletOptions.Add($"name: {wallet.Name}, balance: {wallet.Balance}");
            }

            int sendingWalletIndex = CreateMenu(walletOptions, null, "Select sending wallet\n");

            if (sendingWalletIndex == 0) return;

            WalletDTO sendingWallet = wallets[sendingWalletIndex - 1];

            walletOptions.RemoveAt(sendingWalletIndex - 1);
            wallets.RemoveAt(sendingWalletIndex - 1);

            int receivingWalletIndex = CreateMenu(walletOptions, null, "Select receiving wallet\n");

            WalletDTO receivingWallet = wallets[receivingWalletIndex - 1];


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
                        CreateTransactionDTO transaction = new CreateTransactionDTO();
                        transaction.Price = money;

                       // transaction.SendingWallet = sendingWallet;
                        //transaction.ReceivingWallet = receivingWallet;

                        transactionService.Add(transaction);

                        Console.WriteLine($"The funds were successfully transferred from wallet {sendingWallet.Name} to wallet {receivingWallet.Name}");
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

        private void WalletReplenishment()
        {
            IEnumerable<WalletDTO> walletsEnumerable = walletService.GetAll();
            List<WalletDTO> wallets = new List<WalletDTO>(walletsEnumerable);
            List<string> walletOptions = new List<string> { };

            if (wallets.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You don't hava a wallet");
                Console.ReadKey();
                return;
            }

            foreach (WalletDTO wallet in wallets)
            {
                walletOptions.Add($"name: {wallet.Name}, balance: {wallet.Balance}");
            }

            int walletIndex = CreateMenu(walletOptions, null, "Select a wallet\n");

            if (walletIndex == 0) return;

            WalletDTO selectedWallet = wallets[walletIndex - 1];

            List<FinancialCategoryDTO> categores = categoryService.GetAll();
            List<string> categoryOptions = new List<string> { };

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
                            categores = categoryService.GetAll();
                            isLoop = false;
                            break;
                        case ConsoleKey.Escape:
                            return;
                    }
                }
            }
            foreach (FinancialCategoryDTO category in categores)
            {
                categoryOptions.Add($"name: {category.Name}");
            }

            int action = CreateMenu(categoryOptions, null, "Select a category\n");

            if (action == 0)
            {
                return;
            }

            FinancialCategoryDTO selectedCategory = categores[--action];


            while (true)
            {
                Console.Clear();


                Console.Write("Enter the amount of the deposit: ");
                string enter = Console.ReadLine();
                if (enter == "exit") return;

                if (decimal.TryParse(enter, out decimal money))
                {

                    CreateTransactionDTO transaction = new CreateTransactionDTO();
                   // transaction.FinancialCategory = selectedCategory;
                    transaction.ReceivingWalletId = selectedWallet.Id;
                   // transaction.SendingWalletId = null;
                    transaction.Price = money;

                    transactionService.Add(transaction);

                    Console.Clear();
                    Console.WriteLine($"The wallet {selectedWallet.Name} was successfully replenished in the amount of: {enter}\nFinancial category: {selectedCategory.Name}");
                    Console.ReadKey();
                    return;
                  
                }


            }
        }
    }
}
