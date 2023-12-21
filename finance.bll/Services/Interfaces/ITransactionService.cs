using finance.BLL.ModelsDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace finance.BLL.Services.Interfaces
{
    public interface ITransactionService
    {
        public List<TransactionDTO> GetAll();
        public List<TransactionTypeDTO> GetAllTypes();
        public void Add(CreateTransactionDTO transaction);
        public TransactionDTO GetById(int id);
    }
}
