using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace finance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IWalletService walletService;

        public TransactionController(ITransactionService transactionService, IWalletService walletService)
        {
            this.transactionService = transactionService;
            this.walletService = walletService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> GetAllTransactions()
        {
            var transactions = transactionService.GetAll();
            return Ok(transactions);
        }

        [HttpGet("type")]
        public ActionResult<TransactionDTO> GetAllTransactionTypes()
        {
            var transaction = transactionService.GetAllTypes();


            return Ok(transaction);
        }

        [HttpGet("{id}")]
        public ActionResult<TransactionDTO> GetTransaction(int id)
        {
            var transaction = transactionService.GetById(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost]
        public IActionResult CreateTransaction(CreateTransactionDTO transaction)
        {
            try
            {
                transactionService.Add(transaction);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
