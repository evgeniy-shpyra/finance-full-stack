namespace finance.WebAPI.Client.Models
{
    public class History
    {
        public int Id { get; set; }

        public decimal LeftBalance { get; set; }
        public string Price { get; set; }
        public string WalletFrom { get; set; }
        public string WalletTo { get; set; }
        public string Category { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
