using finance.bll.Services;
using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.WebAPI.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace finance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService walletService;

        public WalletController(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WalletDTO>> GetAllWallets()
        {
            var wallets = Mapper.WalletDTOWalletViews.Map<List<WalletDTO>, List<WalletViewsDTO>>(walletService.GetAll());

            return Ok(wallets);
        }

        [HttpGet("{id}")]
        public ActionResult<Wallet> GetWallet(int id)
        {
            var wallet = Mapper.WalletDTOWalletViews.Map<WalletDTO, WalletViewsDTO>(walletService.GetAll().FirstOrDefault(x => x.Id == id));

            if (wallet == null)
            {
                return NotFound();
            }

            return Ok(wallet);
        }

        [HttpPost]
        public IActionResult CreateWallet(CreateWalletViewsDTO wallet)
        {
            var newWallet = Mapper.CreateWalletViewsCreateWallet.Map<CreateWalletViewsDTO, CreateWalletDTO>(wallet);

            walletService.Add(newWallet);
            return Ok(wallet);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWallet(int id)
        {
            var wallet = walletService.GetAll().FirstOrDefault(x => x.Id == id);

            if (wallet == null)
            {
                return NotFound();
            }

            walletService.Remove(id);
            return NoContent();
        }
    }
}
