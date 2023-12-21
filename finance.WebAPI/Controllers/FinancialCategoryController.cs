using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace finance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialCategoryController : ControllerBase
    {
        private readonly IFinancialCategoryService financialCategoryService;

        public FinancialCategoryController(IFinancialCategoryService financialCategoryService)
        {
            this.financialCategoryService = financialCategoryService;
        }

        [HttpGet]
        public ActionResult<List<FinancialCategoryDTO>> GetAllFinancialCategories()
        {
          
            var financialCategories = financialCategoryService.GetAll();

          
            return Ok(financialCategories);
        }

        [HttpGet("{id}")]
        public ActionResult<FinancialCategoryDTO> GetFinancialCategory(int id)
        {
            var financialCategory = financialCategoryService.GetById(id);

            if (financialCategory == null)
            {
                return NotFound();
            }

            return Ok(financialCategory);
        }

        [HttpPost]
        public IActionResult CreateFinancialCategory(CreateFinancialCategoryDTO financialCategory)
        {
            financialCategoryService.Add(financialCategory);
            return Ok(financialCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFinancialCategory(int id)
        {
            var financialCategory = financialCategoryService.GetById(id);

            if (financialCategory == null)
            {
                return NotFound();
            }

            financialCategoryService.Remove(financialCategory);
            return NoContent();
        }
    }
}
