﻿using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace finance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService historyService;
        private readonly IFinancialCategoryService financialCategoryService;
        private readonly ITransactionService transactionService;

        public HistoryController(IHistoryService historyService, IFinancialCategoryService financialCategoryService, ITransactionService transactionTypeService)
        {
            this.historyService = historyService;
            this.financialCategoryService = financialCategoryService;
            transactionService = transactionTypeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HistoryDTO>> GetAllHistory()
        {
            var history = Mapper.HistoryDTOHistoryViews.Map<List<HistoryDTO>, List<HistoryViewsDTO>>(historyService.GetAll());
            return Ok(history);
        }

        [HttpGet("ByCategory/{categoryId}")]
        public ActionResult<IEnumerable<HistoryViewsDTO>> GetHistoryByCategory(int categoryId)
        {
            var category = financialCategoryService.GetById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            var history = Mapper.HistoryDTOHistoryViews.Map<List<HistoryDTO>, List<HistoryViewsDTO>>(historyService.GetByCategory(category.Name));

            return Ok(history);
        }

        [HttpGet("ByTransactionType/{typeId}")]
        public ActionResult<IEnumerable<HistoryDTO>> GetHistoryByTransactionType(int typeId)
        {
            var type = transactionService.GetAllTypes().FirstOrDefault(x => x.Id == typeId);

            if (type == null)
            {
                return NotFound();
            }

            var history = Mapper.HistoryDTOHistoryViews.Map<List<HistoryDTO>, List<HistoryViewsDTO>>(historyService.GetByTransactionType(type.Id));
           
            return Ok(history);
        }
    }
}
