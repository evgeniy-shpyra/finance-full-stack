using finance.bll.Services;
using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace finance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TransactionViewsDTO>> GetAllTransactions()
        {
            var transactions = Mapper.TransactionDTOTransactionViews.Map<List<TransactionDTO>, List<TransactionViewsDTO>>(transactionService.GetAll());
            return Ok(transactions);
        }

        [HttpGet("type")]
        public ActionResult<IEnumerable<TransactionTypeViewsDTO>> GetAllTransactionTypes()
        {
         
            var transaction = Mapper.TransactionTypeDTOTransactionTypeViews.Map<List<TransactionTypeDTO>, List<TransactionTypeViewsDTO>>(transactionService.GetAllTypes());

            return Ok(transaction);
        }

        [HttpGet("{id}")]
        public ActionResult<TransactionViewsDTO> GetTransaction(int id)
        {
            var transaction = Mapper.TransactionDTOTransactionViews.Map<TransactionDTO, TransactionViewsDTO>(transactionService.GetById(id));

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost]
        public IActionResult CreateTransaction(CreateTransactionViewsDTO transaction)
        {
            try
            {
                var newTransaction = Mapper.CreateTransactionViewsCreateTransaction.Map<CreateTransactionViewsDTO, CreateTransactionDTO>(transaction);
                transactionService.Add(newTransaction);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
