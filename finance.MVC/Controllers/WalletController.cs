using finance.bll.Services;
using finance.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.BLL.ModelsDTO;

namespace finance.MVC.Controllers
{
    public class WalletController : Controller
    {

        private readonly IWalletService walletService;

        public WalletController(IWalletService walletService)
        {
            this.walletService = walletService;
        }


        [HttpGet]
        public ActionResult WalletList()
        {
            var walletsViews = Mapper.WalletToWalletViewsMapper.Map<List<WalletDTO>, List<WalletViews>>(walletService.GetAll());
            return View(walletsViews);
        }

      
        [HttpGet]
        public ActionResult CreateWallet() => View();
        [HttpPost]
        public ActionResult CreateWallet(string Name)
        {
            if (Name == null)
            {
                ViewBag.Error = "An error occurred";
                return View();
            }
            var wallet = new CreateWalletDTO { Name = Name };
            walletService.Add(wallet);

            return RedirectToAction("WalletList");
        }


        public ActionResult DeleteWallet(int walletId)
        {
            if (walletId == null)
            {
                ViewBag.Error = "An error occurred";
                return View();
            }

            var wallet = walletService.GetAll().FirstOrDefault(x => x.Id == walletId);
           
            if (wallet == null)
            {
                return RedirectToAction("WalletList");
            }

            walletService.Remove(wallet.Id);

            return RedirectToAction("WalletList");
        }
    }

}