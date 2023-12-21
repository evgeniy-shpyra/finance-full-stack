using finance.BLL.ModelsDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace finance.BLL.Services.Interfaces
{
    public interface IHistoryService
    {
        public List<HistoryDTO> GetByCategory(string category);
        public List<HistoryDTO> GetByTransactionType(int typeId);
        public List<HistoryDTO> GetAll();
    }
}
