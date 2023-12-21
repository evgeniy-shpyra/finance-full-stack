namespace finance.MVC.Models
{
    public class TransactionViewModel
    {
        public List<WalletViews> WalletsViews { get; set; }
        public List<TransactionTypeViews> TransactionTypeViews { get; set; }
        public List<FinancialCategoryViews> FinancialCategoryViews { get; set; }

    }
}
