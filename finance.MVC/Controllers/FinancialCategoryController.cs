using Microsoft.AspNetCore.Mvc;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.bll.Services;
using finance.MVC.Models;
using finance.BLL.ModelsDTO;

namespace finance.MVC.Controllers
{
    public class FinancialCategoryController : Controller
    {
        private readonly IFinancialCategoryService financialCategoryService;

        public FinancialCategoryController(IFinancialCategoryService financialCategoryService)
        {
            this.financialCategoryService = financialCategoryService;
        }


        [HttpGet]
        public ActionResult FinancialCategoryList()
        {
            var FinancialCategoryViews = Mapper.CategoryDTOCategoryViewsMapper.Map<List<FinancialCategoryDTO>, List<FinancialCategoryViews>>(financialCategoryService.GetAll());
            return View(FinancialCategoryViews);
        }

        [HttpGet]
        public ActionResult CreateFinancialCategory() => View();
        [HttpPost]
        public ActionResult CreateFinancialCategory(string Name)
        {
            if (Name == null)
            {
                ViewBag.Error = "An error occurred";
                return View();
            }
            var financialCategory = new CreateFinancialCategoryDTO { Name = Name };
            financialCategoryService.Add(financialCategory);

            return RedirectToAction("FinancialCategoryList");
        }

        public ActionResult DeleteFinancialCategory(int categoryId)
        {
            var financialCategory = financialCategoryService.GetAll().FirstOrDefault(x => x.Id == categoryId);

            if (financialCategory == null)
            {
                return RedirectToAction("WalletList");
            }

            financialCategoryService.Remove(financialCategory);

            return RedirectToAction("FinancialCategoryList");
        }
     
    }
}