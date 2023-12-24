
namespace finance.WebAPI.DTO
{
    public class CreateTransactionViewsDTO
    {
        public decimal Price { get; set; }

        public int? SendingWalletId { get; set; } = null;
        public int? ReceivingWalletId { get; set; } = null;

        public int? FinancialCategoryId{ get; set; } = null;
    }
}
