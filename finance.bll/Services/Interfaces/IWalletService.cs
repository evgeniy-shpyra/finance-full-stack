using finance.BLL.ModelsDTO;
using finance.DLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace finance.BLL.Services.Interfaces
{
    public interface IWalletService
    {
        public List<WalletDTO> GetAll();
        public void Add(CreateWalletDTO wallet);
        public void Remove(int id);
    }
}
