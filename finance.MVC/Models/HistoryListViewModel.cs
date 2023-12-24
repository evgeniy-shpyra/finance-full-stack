namespace finance.MVC.Models
{
    public class HistoryListViewModel
    {
        public List<HistoryViews> HistoryViews { get; set; }
        public List<TransactionTypeViews> TransactionTypeViews { get; set; }
        public List<FinancialCategoryViews> FinancialCategoryViews { get; set; }
    }
}
