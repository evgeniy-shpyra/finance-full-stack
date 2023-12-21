using finance.bll.Services;
using finance.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.BLL.ModelsDTO;

namespace finance.MVC.Controllers
{
    public class TransactionController : Controller
    {

        private readonly IWalletService walletService;
        private readonly ITransactionService transactionService;
        private readonly IFinancialCategoryService financialCategoryService;

        public TransactionController(IWalletService walletService, ITransactionService transactionService, IFinancialCategoryService financialCategoryService)
        {
            this.walletService = walletService;
            this.transactionService = transactionService;
            this.financialCategoryService = financialCategoryService;
        }

      
        [HttpGet]
        public ActionResult CreateTransaction()
        {
            var transactionTypeViews = Mapper.TransactionTypeToTransactionTypeViewsMapper.Map<List<TransactionTypeDTO>, List<TransactionTypeViews>>(transactionService.GetAllTypes());
            var financialCategoryViews = Mapper.CategoryDTOCategoryViewsMapper.Map<List<FinancialCategoryDTO>, List<FinancialCategoryViews>>(financialCategoryService.GetAll());
            var walletsViews = Mapper.WalletToWalletViewsMapper.Map<List<WalletDTO>, List<WalletViews>>(walletService.GetAll());

            var TransactionViewModel = new TransactionViewModel { TransactionTypeViews = transactionTypeViews, FinancialCategoryViews = financialCategoryViews, WalletsViews = walletsViews };

            if (TempData.ContainsKey("Error"))
            {
                ViewBag.Error = TempData["Error"];
            }
            if (TempData.ContainsKey("Success"))
            {
                ViewBag.Success = TempData["Success"];
            }

            return View(TransactionViewModel);
        }
        [HttpPost]
        public ActionResult CreateTransaction(int transactionType, int sendingWallet, int receivingWallet, int financialCategory, decimal price)
        {

            try
            {
                CreateTransactionDTO transaction = new CreateTransactionDTO { Price = price };


                if (sendingWallet >= 0)
                {
                    transaction.SendingWalletId = sendingWallet;
                }
                if (receivingWallet >= 0)
                {
                    transaction.ReceivingWalletId = receivingWallet;
                }
                if (financialCategory >= 0)
                {
                    transaction.FinancialCategoryId = financialCategory;
                }

                transactionService.Add(transaction);

                TempData["Success"] = "Transaction added successfully";
                return RedirectToAction("CreateTransaction");
            }catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("CreateTransaction");
            }

        }
    }

}