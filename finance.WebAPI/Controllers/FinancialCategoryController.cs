using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.WebAPI.DTO;
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
        public ActionResult<List<FinancialCategoryViewsDTO>> GetAllFinancialCategories()
        {
            var financialCategories = Mapper.CategoryDTOCategoryViews.Map<List<FinancialCategoryDTO>, List<FinancialCategoryViewsDTO>>(financialCategoryService.GetAll());
          
            return Ok(financialCategories);
        }

        [HttpGet("{id}")]
        public ActionResult<FinancialCategoryViewsDTO> GetFinancialCategory(int id)
        {
         
            var financialCategory = Mapper.CategoryDTOCategoryViews.Map<FinancialCategoryDTO, FinancialCategoryViewsDTO>(financialCategoryService.GetById(id));

            if (financialCategory == null)
            {
                return NotFound();
            }

            return Ok(financialCategory);
        }

        [HttpPost]
        public IActionResult CreateFinancialCategory(CreateFinancialCategoryViewsDTO financialCategory)
        {
            var newCategory = Mapper.CreateCategoryViewsCreateCategory.Map<CreateFinancialCategoryViewsDTO, CreateFinancialCategoryDTO>(financialCategory);

            financialCategoryService.Add(newCategory);
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
