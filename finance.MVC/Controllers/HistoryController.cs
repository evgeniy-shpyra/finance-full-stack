using finance.bll.Services;
using finance.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using System;
using finance.BLL.ModelsDTO;

namespace finance.MVC.Controllers
{
    public class HistoryController : Controller
    {

        private readonly IHistoryService historyService;
        private readonly ITransactionService transactionService;
        private readonly IFinancialCategoryService financialCategoryService;

        public HistoryController(IHistoryService historyService, ITransactionService transactionService, IFinancialCategoryService financialCategoryService)
        {
            this.historyService = historyService;
            this.transactionService = transactionService;
            this.financialCategoryService = financialCategoryService;
        }


        [HttpGet]
        public ActionResult HistoryList()
        {
           
            var historyViews = Mapper.HistoryToHistoryViewsMapper.Map<List<HistoryDTO>, List<HistoryViews>>(historyService.GetAll());
            var transactionTypeViews = Mapper.TransactionTypeToTransactionTypeViewsMapper.Map<List<TransactionTypeDTO>, List<TransactionTypeViews>>(transactionService.GetAllTypes());
            var financialCategoryViews = Mapper.CategoryDTOCategoryViewsMapper.Map<List<FinancialCategoryDTO>, List<FinancialCategoryViews>>(financialCategoryService.GetAll());

            var historyViewModel = new HistoryListViewModel { HistoryViews = historyViews, TransactionTypeViews = transactionTypeViews, FinancialCategoryViews = financialCategoryViews };

            return View(historyViewModel);
        }


        [HttpPost]
        public ActionResult HistoryListByTransactionType(int typeId)
        {
            if (typeId == -1)
            {
                return RedirectToAction("HistoryList");
            }
            var type = transactionService.GetAllTypes().FirstOrDefault(x => x.Id == typeId);

            var filteredHistoryViews = Mapper.HistoryToHistoryViewsMapper.Map<List<HistoryDTO>, List<HistoryViews>>(historyService.GetByTransactionType(type.Id));

            return View(filteredHistoryViews);
        }

        [HttpPost]
        public ActionResult HistoryListByCategory(int categoryId)
        {
            if (categoryId == -1)
            {
                return RedirectToAction("HistoryList");
            }

            var category = financialCategoryService.GetById(categoryId); 
            var filteredHistoryViews = Mapper.HistoryToHistoryViewsMapper.Map<List<HistoryDTO>, List<HistoryViews>>(historyService.GetByCategory(category.Name));

            return View(filteredHistoryViews);
        }

    }
}