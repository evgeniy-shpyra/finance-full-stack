namespace finance.WebAPI.Client.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        public decimal Balance { get; set; } = 0;
    }
}
