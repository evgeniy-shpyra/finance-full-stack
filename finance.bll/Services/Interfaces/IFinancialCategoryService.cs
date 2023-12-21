using finance.BLL.ModelsDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace finance.BLL.Services.Interfaces
{
    public interface IFinancialCategoryService
    {
        public List<FinancialCategoryDTO> GetAll();
        public FinancialCategoryDTO GetById(int categoryId);
        public void Add(CreateFinancialCategoryDTO financialCategory);
        public void Remove(FinancialCategoryDTO financialCategory);
    }
}
