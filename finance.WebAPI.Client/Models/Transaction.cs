namespace finance.WebAPI.Client.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public Wallet SendingWallet { get; set; }
        public Wallet ReceivingWallet { get; set; }

        public FinancialCategory FinancialCategory { get; set; } 
        public TransactionType TransactionType { get; set; }
    }
}
