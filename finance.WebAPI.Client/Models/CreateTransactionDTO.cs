namespace finance.WebAPI.Client.Models
{
    public class CreateTransactionDTO
    {
        public decimal Price { get; set; }

        public int? SendingWalletId { get; set; } = null;
        public int? ReceivingWalletId { get; set; } = null;

        public int? FinancialCategoryId{ get; set; } = null;
    }
}
